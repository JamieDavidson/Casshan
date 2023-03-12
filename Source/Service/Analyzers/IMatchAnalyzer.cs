using System.Collections.Generic;
using Casshan.RiotApi.Domain;

namespace Casshan.Service.Analyzers
{
    internal interface IMatchAnalyzer
    {
        void AnalyzeMatches(IEnumerable<LeagueMatch> matches);
    }
}
