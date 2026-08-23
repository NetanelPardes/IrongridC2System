using IronGridC2Consumer.Data;
using IronGridC2Consumer.Models;
using IronGridC2Produser.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IronGridC2Consumer.Servise
{
    public class KafkaConsumerServise
    {

        private readonly IronGridC2DbContext _DbContext;
        public KafkaConsumerServise(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<bool> ProcessAssetLiveStatusUAVEventAsync(string assetLiveStatus)
        {
            var result = JsonSerializer.Deserialize<RealTimeReports>(assetLiveStatus);
            if (result == null)
            {
                Console.WriteLine("The message is empty.");
                return false;
            }


            string processedStatus;
            bool isVerified;

            int number;
            if (!int.TryParse(result.RawValue, out number))
            {
                processedStatus = "Warning";
                isVerified = false;
            }
            else if (number < 0 || number > 100)
            {
                processedStatus = "Warning";
                isVerified = false;
            }
            else if (number < 20)
            {
                processedStatus = "Warning";
                isVerified = true;
            }
            else
            {
                processedStatus = "Stable";
                isVerified = true;
            }
            var newAssetLiveStatus = new AssetLiveStatus
            {
                AssetId = result.AssetId,
                AssetType = result.AssetType,
                RawValue = result.RawValue,
                ProcessedStatus = processedStatus,
                IsVerified = isVerified,
                LastUpdate = result.Timestamp
            };
            if (await ProcessEventAsync(newAssetLiveStatus))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> ProcessAssetLiveStatusPerimeterSensorEventAsync(string assetLiveStatus)
        {
            var result = JsonSerializer.Deserialize<RealTimeReports>(assetLiveStatus);
            if (result == null)
            {
                Console.WriteLine("The message is empty.");
                return false;
            }
            string rawValue;
            string processedStatus;
            bool isVerified;

            if (result.RawValue == "Good" || result.RawValue == "GOOD" || result.RawValue == "good" || result.RawValue == "gud")
            {
                rawValue = "Good";
                processedStatus = "Stable";
                isVerified = true;
            }
            else if (result.RawValue == "Bad" || result.RawValue == "BAD" || result.RawValue == "bad" || result.RawValue == "bed")
            {
                rawValue = "Bad";
                processedStatus = "Warning";
                isVerified = true;
            }
            else
            {
                rawValue = result.RawValue;
                processedStatus = "Warning";
                isVerified = false;
            }
            var newAssetLiveStatus = new AssetLiveStatus
            {
                AssetId = result.AssetId,
                AssetType = result.AssetType,
                RawValue = rawValue,
                ProcessedStatus = processedStatus,
                IsVerified = isVerified,
                LastUpdate = result.Timestamp
            };
            if (await ProcessEventAsync(newAssetLiveStatus))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> ProcessEventAsync(AssetLiveStatus newAssetLiveStatus)
        {
            var assetLiveStatusCHeck = await _DbContext.AssetLiveStatus.FirstOrDefaultAsync(x => x.AssetId == newAssetLiveStatus.AssetId);
            if (assetLiveStatusCHeck == null)
            {
                var assetsCHeck = await _DbContext.Assets.FirstOrDefaultAsync(x => x.Id == newAssetLiveStatus.AssetId);
                if (assetsCHeck == null)
                {
                    Console.WriteLine("AssetLiveStatus should contain a unit that exists");
                    return false;
                }
                else
                {
                    newAssetLiveStatus.AssetId = assetsCHeck.Id;
                    _DbContext.AssetLiveStatus.Add(newAssetLiveStatus);
                    await _DbContext.SaveChangesAsync();

                }
            }
            else
            {
                assetLiveStatusCHeck.AssetType = newAssetLiveStatus.AssetType;
                assetLiveStatusCHeck.RawValue = newAssetLiveStatus.RawValue;
                assetLiveStatusCHeck.ProcessedStatus = newAssetLiveStatus.ProcessedStatus;
                assetLiveStatusCHeck.IsVerified = newAssetLiveStatus.IsVerified;
                assetLiveStatusCHeck.LastUpdate = newAssetLiveStatus.LastUpdate;
                await _DbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
