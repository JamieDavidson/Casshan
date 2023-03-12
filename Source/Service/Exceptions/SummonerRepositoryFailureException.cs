using System;

namespace Casshan.Service.Exceptions
{
    internal sealed class SummonerRepositoryFailureException : Exception
    {
        public SummonerRepositoryFailureException(string message)
            : base(message)
        { }
    }
}
