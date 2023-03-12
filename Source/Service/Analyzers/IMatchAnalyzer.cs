using System.Collections.Generic;
using Casshan.Service.Domain;

namespace Casshan.Service.Analyzers
{
    internal interface IMatchAnalyzer
    {
        void AnalyzeMatches(IEnumerable<Match> matches);
    }
}
