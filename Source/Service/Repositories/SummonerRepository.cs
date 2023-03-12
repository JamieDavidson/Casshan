using System;
using System.Net.Http;
using Casshan.Logging;
using Casshan.Service.Bindings.Dynamic;
using Casshan.Service.Domain;
using Casshan.Service.Exceptions;
using Newtonsoft.Json;

namespace Casshan.Service.Repositories
{
    internal sealed class SummonerRepository : ISummonerRepository
    {
        public SummonerRepository(Func<HttpClient> createClient, ILog log)
        {
            m_CreateClient = createClient
                             ?? throw new ArgumentNullException(nameof(createClient));

            m_Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public Account GetAccountBySummonerName(string summonerName)
        {
            m_Log.Log($"Retrieving details for {summonerName}", LogLevel.Info);
            using (var client = m_CreateClient())
            {
                var response = client.GetAsync($"summoner/v4/summoners/by-name/{summonerName}").Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new SummonerRepositoryFailureException($"Server responded with non success status code {response.StatusCode}, {responseContent}");
                }

                var accountBinding = JsonConvert.DeserializeObject<AccountJsonBinding>(responseContent);

                return new Account(summonerName, accountBinding.AccountId);
            }
        }

        public Account GetAccountById(string accountId)
        {
            using (var client = m_CreateClient())
            {
                var response = client.GetAsync($"summoner/v4/summoners/by-account/{accountId}").Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new SummonerRepositoryFailureException($"Server responded with non success status code {response.StatusCode}, {responseContent}");
                }

                var accountBinding = JsonConvert.DeserializeObject<AccountJsonBinding>(responseContent);

                return new Account(accountBinding.Name, accountBinding.AccountId);
            }
        }

        private readonly Func<HttpClient> m_CreateClient;
        private readonly ILog m_Log;
    }
}
