using System.ComponentModel.DataAnnotations;

namespace IronGridC2Api.DTO
{
    public class criticalAssetsDto
    {
        public int AssetId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;
        public string AssetType { get; set; } = "GenericAsset";
        public string UnitName { get; set; } = "Unknown Unit";
        public string Sector { get; set; } = "General";
        public string ProcessedStatus { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
