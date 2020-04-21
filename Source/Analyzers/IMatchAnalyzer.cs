using System.Collections.Generic;
using Casshan.Domain;

namespace Casshan.Analyzers
{
    internal interface IMatchAnalyzer
    {
        void AnalyzeMatches(IEnumerable<Match> matches);
    }
}
