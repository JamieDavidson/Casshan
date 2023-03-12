using System.Net;
using Casshan.Logging;
using Casshan.RiotApi.Bindings;
using Casshan.RiotApi.Domain;
using Casshan.RiotApi.Exceptions;
using Newtonsoft.Json;

namespace Casshan.RiotApi.Repositories
{
    public sealed class MatchRepository : IMatchRepository
    {
        private static readonly int[] BotGameIds = { 31, 32, 33, 800, 810, 820, 830, 840, 850 };

        private static readonly int[] AramGameIds = { 65, 67, 100, 450 };

        public MatchRepository(Func<HttpClient> createClient,
                               ILog log)
        {
            m_CreateClient = createClient
                ?? throw new ArgumentNullException(nameof(createClient));

            m_Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public LeagueMatch GetMatchForPlayer(string accountId, string gameId)
        {
            var binding = GetJson<SingleMatchJsonBinding>($"match/v4/matches/{gameId}");

            return ToDomain(accountId, binding);
        }

        public int GetGameCount(string accountId)
        {
            try
            {
                var binding = GetJson<GameCountBinding>
                    ($"match/v4/matchlists/by-account/{accountId}?beginIndex=13371337&endindex=13371337");

                return binding.TotalGames;
            }
            catch (NonSuccessResponseException e)
            when (e.StatusCode == HttpStatusCode.NotFound)
            {
                m_Log.Log($"Querying total game count returned 404 for account {accountId}", LogLevel.Error);
                m_Log.Log(e.Message, LogLevel.Error);

                return 0;
            }
        }

        public int GetAramGameCount(string accountId)
        {
            try
            {
                var binding = GetJson<GameCountBinding>
                    ($"match/v4/matchlists/by-account/{accountId}?beginIndex=13371337&endIndex=13371337&queue={string.Join("&queue=", AramGameIds.Select(i => i.ToString()))}");

                return binding.TotalGames;
            }
            catch (NonSuccessResponseException e)
                when (e.StatusCode == HttpStatusCode.NotFound)
            {
                m_Log.Log($"Querying ARAM game count returned 404 for account {accountId}", LogLevel.Error);
                m_Log.Log($"{e.Message}", LogLevel.Error);
                return 0;
            }
        }

        public int GetBotGameCount(string accountId)
        {
            try
            {
                var binding = GetJson<GameCountBinding>
                    ($"match/v4/matchlists/by-account/{accountId}?beginIndex=13371337&endIndex=13371337&queue={string.Join("&queue=", BotGameIds.Select(i => i.ToString()))}");

                return binding.TotalGames;
            }
            catch (NonSuccessResponseException e)
            when (e.StatusCode == HttpStatusCode.NotFound)
            {
                m_Log.Log($"Querying bot game count returned 404 for account {accountId}", LogLevel.Error);
                m_Log.Log($"{e.Message}", LogLevel.Error);
                return 0;
            }
        }

        public IEnumerable<string> GetMatchIds(string accountId, int begin = 0, int end = 50)
        {
            try
            {
                var binding = GetJson<MatchesJsonBinding>
                    ($"match/v4/matchlists/by-account/{accountId}?queue={string.Join("&queue=", BotGameIds.Select(i => i.ToString()))}&endIndex={end}&beginIndex={begin}");

                return binding.Matches.Select(m => m.GameId);
            }
            catch (NonSuccessResponseException e)
            when (e.StatusCode == HttpStatusCode.NotFound)
            {
                m_Log.Log($"Querying for match IDs returned 404 for account {accountId}", LogLevel.Error);
                m_Log.Log($"{e.Message}", LogLevel.Error);
                return new string[] {};
            }
        }

        private static LeagueMatch ToDomain(string targetAccountId, SingleMatchJsonBinding binding)
        {
            var humanPlayers = binding.ParticipantIdentities
                .Where(p => p.Player.SummonerId != null);

            var participantId = binding.ParticipantIdentities.SingleOrDefault(p => p.Player.AccountId == targetAccountId)?.ParticipantId;

            if (participantId == null)
            {
                return null;
            }

            var participantData = binding.Participants.Single(p => p.ParticipantId == participantId);

            var spells = new SummonerSpellPair(participantData.Spell1Id, participantData.Spell2Id);

            var stats = participantData.Stats;

            var runes = new[]
            {
                stats.Perk0,
                stats.Perk1,
                stats.Perk2,
                stats.Perk3,
                stats.Perk4,
                stats.Perk5
            };

            var visionScore = new VisionStats(stats.VisionScore,
                                              stats.VisionWardsBoughtInGame,
                                              stats.WardsKilled,
                                              stats.WardsPlaced);

            return new LeagueMatch(humanPlayers.Select(s => new Account(s.Player.SummonerName, s.Player.AccountId)),
                             runes,
                             spells,
                             visionScore,
                             stats.TotalMinionsKilled);
        }

        private T GetJson<T>(string path) where T : class
        {
            using (var client = m_CreateClient())
            {
                var response = client.GetAsync(path).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new NonSuccessResponseException($"Request failed, response {responseContent}", response.StatusCode);
                }

                return JsonConvert.DeserializeObject<T>(responseContent);
            }
        }

        private readonly Func<HttpClient> m_CreateClient;

        private readonly ILog m_Log;
    }
}
