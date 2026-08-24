using System.ComponentModel.DataAnnotations;

namespace IronGridC2Api.DTO
{
    public class UpdateAssetsDto
    {
        public int UnitId { get; set; }

        [Required(ErrorMessage = "AssetSerial id required")]
        public string AssetSerial { get; set; } = string.Empty;

        public string AssetType { get; set; } = "GenericAsset";
    }
}
