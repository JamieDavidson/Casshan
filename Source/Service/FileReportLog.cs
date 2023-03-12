using System;
using System.Collections.Generic;
using System.Linq;

using Casshan.Bindings.Report;
using Casshan.Domain;
using Casshan.Logging;

namespace Casshan
{
    internal sealed class FileReportLog : IReportLog
    {
        private readonly ILog m_Log;
        private ReportJsonBinding m_CurrentLog;
        private string m_SummonerName;

        public FileReportLog(ILog log)
        {
            m_Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void StartReport(Account account)
        {
            m_CurrentLog = new ReportJsonBinding
            {
                SummonerName = account.SummonerName,
                AccountId = account.AccountId,
                ReportItems = new List<ReportItemJsonBinding>()
            };

            m_SummonerName = account.SummonerName;
            m_Log.Log($"Log for {m_SummonerName} started", LogLevel.Success);
        }

        public void AddReportItem(string sectionName, int sectionSuspicion, IEnumerable<string> reportLines)
        {
            m_CurrentLog.ReportItems.Add(new ReportItemJsonBinding
            {
                SectionSuspicion = sectionSuspicion,
                SectionName = sectionName,
                ReportLines = reportLines.ToArray()
            });
            m_Log.Log($"Added {sectionName} section to report", LogLevel.Success);
        }

        public void FinishReport()
        {
            m_CurrentLog.SuspicionRating = m_CurrentLog.ReportItems.Sum(i => i.SectionSuspicion);

            JsonUtil.SaveJsonToFile($@".\Reports\{m_SummonerName}.json", m_CurrentLog);
            m_Log.Log($"Log for {m_SummonerName} finalised", LogLevel.Success);
            if (m_CurrentLog.SuspicionRating >= 10)
            {
                m_Log.Log($"{m_SummonerName} likely a bot, suspicion rating {m_CurrentLog.SuspicionRating}", LogLevel.Success);
            }
            else if (m_CurrentLog.SuspicionRating >= 5)
            {
                m_Log.Log($"{m_SummonerName} possibly a bot, suspicion rating {m_CurrentLog.SuspicionRating}", LogLevel.Success);
            }
            else
            {
                m_Log.Log($"{m_SummonerName} unlikely a bot, suspicion rating {m_CurrentLog.SuspicionRating}", LogLevel.Success);
            }
        }
    }
}