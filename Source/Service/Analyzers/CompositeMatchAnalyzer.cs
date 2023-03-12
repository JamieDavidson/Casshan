using System.Collections.Generic;
using System.Linq;
using Casshan.RiotApi.Domain;

namespace Casshan.Service.Analyzers
{
    internal sealed class CompositeMatchAnalyzer : IMatchAnalyzer
    {
        private readonly IReadOnlyCollection<IMatchAnalyzer> m_MatchAnalyzers;

        public CompositeMatchAnalyzer(IEnumerable<IMatchAnalyzer> matchAnalyzers)
        {
            m_MatchAnalyzers = matchAnalyzers.ToArray();
        }

        public void AnalyzeMatches(IEnumerable<LeagueMatch> matches)
        {
            var matchesArray = matches.ToArray();

            foreach (var analyzer in m_MatchAnalyzers)
            {
                analyzer.AnalyzeMatches(matchesArray);
            }
        }
    }
}
