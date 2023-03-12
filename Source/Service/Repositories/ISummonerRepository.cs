using Casshan.Service.Domain;

namespace Casshan.Service.Repositories
{
    internal interface ISummonerRepository
    {
        Account GetAccountBySummonerName(string summonerName);

        Account GetAccountById(string accountId);
    }
}
