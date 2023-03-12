using System.Collections.Generic;
using Casshan.Domain;

namespace Casshan
{
    internal interface IReportLog
    {
        void StartReport(Account account);
        void AddReportItem(string sectionName, int sectionSuspicion, IEnumerable<string> reportLines);
        void FinishReport();
    }
}
