using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronGridC2Produser.Models
{
    public class Assets
    {
        [Required(ErrorMessage = "Id id required")]
        public int Id { get; set; }

        public int UnitId { get; set; }

        [Required(ErrorMessage = "AssetSerial id required")]
        public string AssetSerial { get; set; } = string.Empty;

        public string AssetType { get; set; } = "GenericAsset";
    }
}
