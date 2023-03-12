namespace Casshan.RiotApi.Domain
{
    public readonly struct SummonerSpellPair
    {
        public int First { get; }
        public int Second { get; }

        public SummonerSpellPair(int first, int second)
        {
            First = first;
            Second = second;
        }

        public bool Equals(SummonerSpellPair other)
        {
            return First == other.First && Second == other.Second;
        }

        public override bool Equals(object obj)
        {
            return obj is SummonerSpellPair other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (First * 397) ^ Second;
            }
        }
    }
}
