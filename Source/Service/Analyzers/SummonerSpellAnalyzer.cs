using System;
using System.Collections.Generic;
using System.Linq;
using Casshan.Service.Domain;

namespace Casshan.Service.Analyzers
{
    internal sealed class SummonerSpellAnalyzer : IMatchAnalyzer
    {
        private readonly IReportLog m_ReportLog;
        private readonly IReadOnlyDictionary<int, string> m_SummonerData;

        public SummonerSpellAnalyzer(IReportLog reportLog, IReadOnlyDictionary<int, string> summonerData)
        {
            m_ReportLog = reportLog ?? throw new ArgumentNullException(nameof(reportLog));

            m_SummonerData = summonerData
                ?? throw new ArgumentNullException(nameof(summonerData));
        }

        public void AnalyzeMatches(IEnumerable<Match> matches)
        {
            var matchesArray = matches.ToArray();

            var spellTracking = new Dictionary<SummonerSpellPair, int>();

            foreach (var match in matchesArray)
            {
                if (spellTracking.ContainsKey(match.SpellIds))
                {
                    spellTracking[match.SpellIds]++;
                }
                else
                {
                    spellTracking.Add(match.SpellIds, 1);
                }
            }

            var logItems = new List<string>
            {
                $"Player used {spellTracking.Count} pairs of summoner spells over {matchesArray.Length} games"
            };

            logItems.AddRange(spellTracking.Select(m =>
                $"Used {m_SummonerData[m.Key.First]} and {m_SummonerData[m.Key.Second]} {m.Value} times."));

            var diff = 4 - spellTracking.Count;
            var suspicion = diff > 0 ? diff : 0;

            m_ReportLog.AddReportItem("Summoner Analysis", suspicion, logItems);
        }
    }
}
