using Casshan.RiotApi.Domain;

namespace Casshan.RiotApi.Repositories
{
    public interface ISummonerRepository
    {
        Account GetAccountBySummonerName(string summonerName);

        Account GetAccountById(string accountId);
    }
}
