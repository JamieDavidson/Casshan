using System;

namespace Casshan.Exceptions
{
    internal sealed class SummonerRepositoryFailureException : Exception
    {
        public SummonerRepositoryFailureException(string message)
            : base(message)
        { }
    }
}
