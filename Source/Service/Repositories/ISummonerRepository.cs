using Casshan.Domain;

namespace Casshan.Repositories
{
    internal interface ISummonerRepository
    {
        Account GetAccountBySummonerName(string summonerName);

        Account GetAccountById(string accountId);
    }
}
