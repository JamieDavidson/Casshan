namespace Casshan.RiotApi.Domain
{
    public sealed class Account
    {
        public string SummonerName { get; }

        public string AccountId { get; }


        public Account(string summonerName, string accountId)
        {
            SummonerName = summonerName ?? throw new ArgumentNullException(nameof(summonerName));
            AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
        }
    }
}
