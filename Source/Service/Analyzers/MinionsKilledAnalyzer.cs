using System.Collections.Generic;
using System.Linq;
using Casshan.RiotApi.Domain;

namespace Casshan.Service.Analyzers
{
    internal sealed class MinionsKilledAnalyzer : IMatchAnalyzer
    {
        private readonly IReportLog m_ReportLog;

        public MinionsKilledAnalyzer(IReportLog reportLog)
        {
            m_ReportLog = reportLog;
        }

        public void AnalyzeMatches(IEnumerable<LeagueMatch> matches)
        {
            var matchesArray = matches.ToArray();
            var noKills = matchesArray.Count(m => m.MinionsKilled == 0);

            m_ReportLog.AddReportItem("Minion Analysis", 0, new []
            {
                $"Player killed no minions in: {noKills} games.",
                $"Min killed: {matchesArray.Min(s => s.MinionsKilled)}",
                $"Max killed: {matchesArray.Max(s => s.MinionsKilled)}",
                $"Average killed: {matchesArray.Average(s => s.MinionsKilled)}"
            });
        }
    }
}
