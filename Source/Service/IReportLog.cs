using System.Collections.Generic;
using Casshan.Service.Domain;

namespace Casshan.Service
{
    internal interface IReportLog
    {
        void StartReport(Account account);
        void AddReportItem(string sectionName, int sectionSuspicion, IEnumerable<string> reportLines);
        void FinishReport();
    }
}
