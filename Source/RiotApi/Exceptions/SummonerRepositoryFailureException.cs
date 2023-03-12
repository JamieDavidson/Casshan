namespace Casshan.RiotApi.Exceptions
{
    public sealed class SummonerRepositoryFailureException : Exception
    {
        public SummonerRepositoryFailureException(string message)
            : base(message)
        { }
    }
}
