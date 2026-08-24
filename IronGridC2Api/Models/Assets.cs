using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Api.Models
{
    public class Assets
    {
        [Required(ErrorMessage = "Id id required")]
        public int Id { get; set; }

        public int UnitId { get; set; }
        public Units units { get; set; } = null!;

        [Required(ErrorMessage = "AssetSerial id required")]
        public string AssetSerial { get; set; } = string.Empty;

        public string AssetType { get; set; } = "GenericAsset";

        public AssetLiveStatus assetLiveStatus { get; set; } = null!;
    }
}
