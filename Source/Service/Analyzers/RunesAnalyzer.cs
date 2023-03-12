using System;
using System.Collections.Generic;
using System.Linq;
using Casshan.Domain;

namespace Casshan.Analyzers
{
    internal sealed class RunesAnalyzer : IMatchAnalyzer
    {
        private readonly IReportLog m_ReportLog;
        private readonly IReadOnlyDictionary<int, string> m_RuneData;

        public RunesAnalyzer(IReportLog reportLog, IReadOnlyDictionary<int, string> runeData)
        {
            m_ReportLog = reportLog ?? throw new ArgumentNullException(nameof(reportLog));
            m_RuneData = runeData ?? throw new ArgumentNullException(nameof(runeData));
        }

        public void AnalyzeMatches(IEnumerable<Match> matches)
        {
            var matchArray = matches.ToArray();

            var runeOccurences = matchArray
                .SelectMany(m => m.RuneIds)
                .GroupBy(r => r)
                .OrderByDescending(g => g.Count())
                .ToDictionary(r => r.Key, r => r.Count());

            var suspicion = runeOccurences.Count(r => r.Value >= 40);

            var lines = new List<string>();

            foreach (var rune in runeOccurences)
            {
                var name = m_RuneData.ContainsKey(rune.Key) ? m_RuneData[rune.Key] : "unknown";
                lines.Add($"Used {name} {rune.Value} times.");
            }

            m_ReportLog.AddReportItem("Rune Analysis",
                suspicion,
                lines);
        }
    }
}
