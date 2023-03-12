namespace Casshan.RiotApi.Domain
{
    public sealed class VisionStats
    {
        public int VisionScore { get; }
        public int WardsBought { get; }
        public int? WardsKilled { get; }
        public int? WardsPlaced { get; }

        public VisionStats(int visionScore, int wardsBought, int? wardsKilled, int? wardsPlaced)
        {
            VisionScore = visionScore;
            WardsBought = wardsBought;
            WardsKilled = wardsKilled;
            WardsPlaced = wardsPlaced;
        }
    }
}
