using System;
using System.Linq;

namespace Casshan.Service.Analyzers
{
    internal sealed class CasingPatternNameAnalyzer : INameAnalyzer
    {
        private readonly IReportLog m_ReportLog;

        public CasingPatternNameAnalyzer(IReportLog reportLog)
        {
            m_ReportLog = reportLog
                ?? throw new ArgumentNullException(nameof(reportLog));
        }

        public void AnalyzeName(string name)
        {
            var upper = name.Count(char.IsUpper);
            var lower = name.Count(char.IsLower);

            if (Math.Abs(upper - lower) <= 1)
            {
                m_ReportLog.AddReportItem("Name Pattern Analayis", 1, new []
                {
                    $"Name {name} matched casing pattern"
                });
                return;
            }

            m_ReportLog.AddReportItem("Name Pattern Analayis", 0, new []
            {
                $"Name {name} did not match casing pattern"
            });
        }
    }
}
