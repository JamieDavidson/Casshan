using Casshan.RiotApi.Domain;

namespace Casshan.RiotApi.Repositories
{
    public interface IMatchRepository
    {
        LeagueMatch GetMatchForPlayer(string accountId, string gameId);

        int GetAramGameCount(string accountId);

        int GetBotGameCount(string accountId);
        
        int GetGameCount(string accountId);

        IEnumerable<string> GetMatchIds(string accountId, int begin = 0, int end = 50);
    }
}
