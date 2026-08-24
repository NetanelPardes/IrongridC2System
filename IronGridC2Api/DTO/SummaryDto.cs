namespace IronGridC2Api.DTO
{
    public class SummaryDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = "Unknown Unit";
        public string Sector { get; set; } = "General";
        public int totalAssets { get; set; }
        public int stableAssets { get; set; }
        public int warningAssets { get; set; }
        public int unverifiedAssets { get; set; }
    }
}
