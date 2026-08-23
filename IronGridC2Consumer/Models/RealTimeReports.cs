using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Produser.Models
{
    public class RealTimeReports
    {
        public int AssetId { get; set; }

        [RegularExpression("^UAV|PerimeterSensor$", ErrorMessage = "AssetType must be UAV or PerimeterSensor")]
        public string AssetType { get; set; } = string.Empty;

        public string RawValue { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastUpdate id required)]")]
        public DateTime Timestamp { get; set; }
    }
}
