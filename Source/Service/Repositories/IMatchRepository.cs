using System.Collections.Generic;
using Casshan.Service.Domain;

namespace Casshan.Service.Repositories
{
    internal interface IMatchRepository
    {
        Match GetMatchForPlayer(string accountId, string gameId);

        int GetAramGameCount(string accountId);

        int GetBotGameCount(string accountId);
        
        int GetGameCount(string accountId);

        IEnumerable<string> GetMatchIds(string accountId, int begin = 0, int end = 50);
    }
}
