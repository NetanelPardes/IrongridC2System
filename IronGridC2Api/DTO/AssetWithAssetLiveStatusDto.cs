using System.ComponentModel.DataAnnotations;

namespace IronGridC2Api.DTO
{
    public class AssetWithAssetLiveStatusDto
    {
        [Required(ErrorMessage = "Id id required")]
        public int Id { get; set; }

        public int UnitId { get; set; }

        [Required(ErrorMessage = "AssetSerial id required")]
        public string AssetSerial { get; set; } = string.Empty;

        [RegularExpression("^UAV|PerimeterSensor$", ErrorMessage = "AssetType must be UAV or PerimeterSensor")]
        public string AssetType { get; set; } = string.Empty;

        [Required(ErrorMessage = "RawValue id required)]")]
        public string? RawValue { get; set; } = null;

        [RegularExpression("^Stable|Warning$", ErrorMessage = "ProcessedStatus must be Stable or Warning")]
        public string? ProcessedStatus { get; set; } = null;

        [Required(ErrorMessage = "IsVerified id required)]")]
        public bool? IsVerified { get; set; }

        [Required(ErrorMessage = "LastUpdate id required)]")]
        public DateTime? LastUpdate { get; set; }
    }
}
