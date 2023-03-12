using System;
using System.Collections.Generic;
using System.Linq;
using Casshan.RiotApi.Domain;

namespace Casshan.Service.Analyzers
{
    internal sealed class VisionAnalyzer : IMatchAnalyzer
    {
        private readonly IReportLog m_ReportLog;

        public VisionAnalyzer(IReportLog reportLog)
        {
            m_ReportLog = reportLog ?? throw new ArgumentNullException(nameof(reportLog));
        }

        public void AnalyzeMatches(IEnumerable<LeagueMatch> matches)
        {
            var vision = matches.Select(m => m.TargetVisionStats).ToArray();

            var suspicion = 0;
            var reportLines = new List<string>();

            var wardsPlaced = vision.Sum(v => v.WardsPlaced);
            var averageWardsPlaced = vision.Average(v => v.WardsPlaced);

            var totalVisionScore = vision.Sum(v => v.VisionScore);
            var averageVisionScore = vision.Average(v => v.VisionScore);

            reportLines.Add($"Player placed {wardsPlaced} wards over {vision.Length} games");
            reportLines.Add($"Player placed an average of {averageWardsPlaced} wards per game");

            reportLines.Add($"Player had {totalVisionScore} vision score across {vision.Length} games");
            reportLines.Add($"Player had an average vision score of {averageVisionScore} per game");

            if (averageWardsPlaced < 1)
            {
                suspicion++;
            }

            if (averageVisionScore < 1)
            {
                suspicion++;
            }

            m_ReportLog.AddReportItem("Vision Analysis", suspicion, reportLines);
        }
    }
}
