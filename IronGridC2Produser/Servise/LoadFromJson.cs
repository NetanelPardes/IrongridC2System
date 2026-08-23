using IronGridC2Produser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IronGridC2Produser.Servise
{
    public class LoadFromJson
    {

        public List<AssetLiveStatus> LoadAssetLiveStatusData(string filePath)
        {
            string json = File.ReadAllText(filePath);
            List<AssetLiveStatus>? AssetLiveStatusList = JsonSerializer.Deserialize<List<AssetLiveStatus>>(json);
            return AssetLiveStatusList ?? new List<AssetLiveStatus>();
        }
    }
}
