using System;
using System.Collections.Generic;
using System.Linq;

namespace Casshan.Service.Analyzers
{
    internal sealed class PredefinedNameStrippingAnalyzer : INameAnalyzer
    {
        private readonly IReportLog m_ReportLog;
        private readonly INameAnalyzer m_UnderlyingAnalyzer;
        private readonly string[] m_SuspiciousNames;

        public PredefinedNameStrippingAnalyzer(IReportLog reportLog,
                                               IEnumerable<string> suspiciousNames,
                                               INameAnalyzer underlyingAnalyzer)
        {
            m_ReportLog = reportLog ?? throw new ArgumentNullException(nameof(reportLog));

            m_UnderlyingAnalyzer = underlyingAnalyzer
                ?? throw new ArgumentNullException(nameof(underlyingAnalyzer));

            m_SuspiciousNames = suspiciousNames.OrderByDescending(x => x.Length).ToArray();
        }

        public void AnalyzeName(string name)
        {
            foreach (var n in m_SuspiciousNames)
            {
                if (name.StartsWith(n) || name.EndsWith(n))
                {
                    var stripped = name.Replace(n, "");
                    m_ReportLog.AddReportItem("Suspicious Name Analysis", 1, new []
                    {
                        $"Player name {name} contained suspicious name {n}"
                    });

                    m_UnderlyingAnalyzer.AnalyzeName(stripped);
                    return;
                }
            }

            m_ReportLog.AddReportItem("Suspicious Name Analysis", 0, new []
            {
                $"Player name {name} did not contain a suspicious name."
            });

            m_UnderlyingAnalyzer.AnalyzeName(name);
        }
    }
}
