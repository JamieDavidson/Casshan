using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Casshan.Logging;
using Casshan.Logging.Extensions;
using Casshan.RiotApi.Domain;
using Casshan.RiotApi.Exceptions;
using Casshan.RiotApi.Repositories;
using Casshan.Service.Analyzers;
using Casshan.Service.Bindings.Static;

namespace Casshan.Service
{
    internal class Program
    {
        private static readonly List<Account> AccountsToAnalyze = new List<Account>();
        private static readonly List<Account> AnalyzedAccounts = new List<Account>();

        private static IMatchRepository m_MatchRepository;
        private static ISummonerRepository m_SummonerRepository;

        private static INameAnalyzer m_NameAnalyzer;
        private static IMatchAnalyzer m_MatchAnalyzer;
        private static IReportLog m_ReportLog;

        private static ILog m_Log;

        private static void Main(string[] args)
        {
            m_Log = new ConsoleLogger()
                .WithConsoleColours()
                .WithTimeStampLogging(DateTimeKind.Local)
                .WithLogLevelPrefixes();

            m_ReportLog = new FileReportLog(m_Log);

            m_MatchAnalyzer = CreateCompositeAnalyzer();

            m_NameAnalyzer = CreateNameAnalyzer();

            m_MatchRepository = new MatchRepository(CreateHttpClient, m_Log);
            m_SummonerRepository = new SummonerRepository(CreateHttpClient, m_Log);

            var patientZero = m_SummonerRepository.GetAccountBySummonerName("uEeTLydia");

            RunAnalysis(patientZero);

            while (true)
            {
                var toSearch = AccountsToAnalyze.ToArray();

                if (toSearch.Length == 0)
                {
                    m_Log.Log("We have no accounts to search, something's clearly wrong, exiting :D", LogLevel.Error);
                    Environment.Exit(0);
                }

                foreach (var n in toSearch)
                {
                    Console.WriteLine();

                    AccountsToAnalyze.RemoveAll(a => a.AccountId == n.AccountId);
                    try
                    {
                        if (File.Exists($@".\Reports\{n.SummonerName}.json"))
                        {
                            m_Log.Log($"Already analysed {n.SummonerName}, skipping", LogLevel.Info);
                            continue;
                        }
                        var next = m_SummonerRepository.GetAccountById(n.AccountId);
                        if (File.Exists($@".\Reports\{next.SummonerName}.json"))
                        {
                            m_Log.Log($"Already analysed {next.SummonerName}, skipping", LogLevel.Info);
                            continue;
                        }
                        RunAnalysis(next);
                    }
                    catch (SummonerRepositoryFailureException)
                    {
                        m_Log.Log($"{n.SummonerName} has played no bot games", LogLevel.Info);
                    }
                    finally
                    {
                        AnalyzedAccounts.Add(n);
                    }
                }
            }
        }

        private static IMatchAnalyzer CreateCompositeAnalyzer()
        {
            var runes = JsonUtil.LoadJsonFromFile<RuneCategoryJsonBinding[]>
                    ($@"{Environment.CurrentDirectory}\data\runesReforged.json")
                .SelectMany(s => s.Slots)
                .SelectMany(s => s.Runes)
                .ToDictionary(r => r.Id, r => r.Name);

            var items = JsonUtil.LoadJsonFromFile<ItemsJsonBinding>($@"{Environment.CurrentDirectory}\data\item.json")
                .Data.ToDictionary(i => i.Key, i => i.Value.Name);

            var summonerSpells = JsonUtil.LoadJsonFromFile<SummonersJsonBinding>
                    ($@"{Environment.CurrentDirectory}\data\summoner.json")
                .Data.ToDictionary(s => s.Value.Key, s => s.Value.Name);

            return new CompositeMatchAnalyzer(new IMatchAnalyzer[]
            {
                new RunesAnalyzer(m_ReportLog, runes),
                new MinionsKilledAnalyzer(m_ReportLog),
                new SummonerSpellAnalyzer(m_ReportLog, summonerSpells),
                new VisionAnalyzer(m_ReportLog) 
            });
        }

        private static INameAnalyzer CreateNameAnalyzer()
        {
            var names = JsonUtil.LoadJsonFromFile<string[]>($@"{Environment.CurrentDirectory}\data\names.json");

            m_Log.Log($"Loaded {names.Length} predefined names", LogLevel.Debug);

            var variations = new List<string>();

            foreach(var name in names)
            {
                variations.Add(name.Substring(0, name.Length - 1));
                variations.Add(name + name[name.Length-1]);
            }

            m_Log.Log($"Created {variations.Count} name variations", LogLevel.Debug);

            return new PredefinedNameStrippingAnalyzer
                (m_ReportLog, names.Concat(variations), new CasingPatternNameAnalyzer(m_ReportLog));
        }

        private static void RunAnalysis(Account account)
        {
            m_ReportLog.StartReport(account);

            var allGames = m_MatchRepository.GetGameCount(account.AccountId);
            var botGames = m_MatchRepository.GetBotGameCount(account.AccountId);
            var aramGames = m_MatchRepository.GetAramGameCount(account.AccountId);

            var suspicion = 0;
            if (botGames > 50)
            {
                suspicion++;
            }

            if (allGames - botGames <= 50)
            {
                suspicion++;
            }

            m_ReportLog.AddReportItem("Game analysis", suspicion, new []
            {
                $"All games played: {allGames}",
                $"Bot games played: {botGames}"
            });

            m_Log.Log($"{account.SummonerName}, {account.AccountId}", LogLevel.Info);
            m_Log.Log($"Bot Games / All games: {botGames}/{allGames}", LogLevel.Info);

            // Unlikely we'll hit this for now, since we're analysing bot games and then
            // moving on to other players in the bot game. But for future purposes,
            // such as searching a user's current non-AI game, it'll be useful.
            if (botGames == 0)
            {
                m_Log.Log($"Target {account.SummonerName} has played no bot games, skipping.", LogLevel.Error);
                return;
            }

            m_NameAnalyzer.AnalyzeName(account.SummonerName);

            var gameIds = m_MatchRepository.GetMatchIds(account.AccountId).ToArray();
            var matches = new List<LeagueMatch>();

            m_Log.Log($"Retrieving bot games for {account.SummonerName}", LogLevel.Info);
            var previousNames = new List<string>();
            var participants = new List<Account>();
            for (var i = 0; i < gameIds.Length; i++)
            {
                UpdateTitle($"Retrieving bot game {i+1} of {gameIds.Length}");
                var match = m_MatchRepository.GetMatchForPlayer(account.AccountId, gameIds[i]);

                if (match == null)
                    continue;

                matches.Add(match);

                var humanParticipants = matches.SelectMany(m => m.HumanParticipants).ToArray();

                var oldName = humanParticipants.FirstOrDefault
                    (h => h.AccountId == account.AccountId && h.SummonerName != account.SummonerName)?.SummonerName;

                if (oldName != null)
                {
                    if (!previousNames.Contains(oldName))
                    {
                        previousNames.Add(oldName);
                        m_Log.Log($"{account.SummonerName} has had a name change, old name: {oldName}", LogLevel.Success);
                        m_NameAnalyzer.AnalyzeName(oldName);
                    }
                }

                participants.AddRange(humanParticipants);
            }

            if (AccountsToAnalyze.Count < 1000)
            {
                var uniqueParticipants = participants
                    .GroupBy(g => g.SummonerName)
                    .Where(g => g.Key != account.SummonerName && previousNames.All(p => p != g.Key))
                    .Select(g => g.First());

                AccountsToAnalyze.AddRange(uniqueParticipants.Where(m => AnalyzedAccounts.All(a => a.AccountId != m.AccountId)));
            }

            m_MatchAnalyzer.AnalyzeMatches(matches);

            m_ReportLog.FinishReport();
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new ExpectedFailureResponseHandler(new HttpClientHandler(), m_Log))
            {
                BaseAddress = new Uri("https://na1.api.riotgames.com/lol/")
            };
            
            client.DefaultRequestHeaders.Add("X-Riot-Token", "SET ME");

            return client;
        }

        private static void UpdateTitle(string suffix)
        {
            Console.Title = $"Casshan, bot hunter - Queue {AccountsToAnalyze.Count}"
                            + $" - Session - {AnalyzedAccounts.Count} - {suffix}";
        }
    }
}