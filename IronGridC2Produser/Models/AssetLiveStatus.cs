using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Produser.Models
{
    public class AssetLiveStatus
    {
        public int AssetId { get; set; }

        [RegularExpression("^UAV|PerimeterSensor$", ErrorMessage = "AssetType must be UAV or PerimeterSensor")]
        public string AssetType { get; set; } = string.Empty;

        [Required(ErrorMessage = "RawValue id required)]")]
        public string RawValue { get; set; } = string.Empty;

        [RegularExpression("^Stable|Warning$" , ErrorMessage = "ProcessedStatus must be Stable or Warning")]
        public string ProcessedStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "IsVerified id required)]")]
        public bool IsVerified { get; set; }

        [Required(ErrorMessage = "LastUpdate id required)]")]
        public DateTime LastUpdate { get; set; }
    }
}
