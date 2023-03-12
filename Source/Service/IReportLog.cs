using System.Collections.Generic;
using Casshan.RiotApi.Domain;

namespace Casshan.Service
{
    internal interface IReportLog
    {
        void StartReport(Account account);
        void AddReportItem(string sectionName, int sectionSuspicion, IEnumerable<string> reportLines);
        void FinishReport();
    }
}
