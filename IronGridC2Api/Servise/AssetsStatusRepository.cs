using IronGridC2Api.Data;
using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IronGridC2Api.Servise
{
    public class AssetsStatusRepository : IAssetsStatusRepository
    {
        private readonly IronGridC2DbContext _DbContext;
        public AssetsStatusRepository(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<List<AssetWithAssetLiveStatusDto>> GetAllAssetLiveStatusAsync()
        {
            List<AssetWithAssetLiveStatusDto> result = new List<AssetWithAssetLiveStatusDto>();
            var Assets = await _DbContext.Assets.ToListAsync();
            foreach (var Asset in Assets)
            {
                var exist = await _DbContext.AssetLiveStatus.FirstOrDefaultAsync(x => x.AssetId == Asset.Id);
                if (exist == null)
                {
                    var asset = new AssetWithAssetLiveStatusDto
                    {
                        Id = Asset.Id,
                        UnitId = Asset.UnitId,
                        AssetSerial = Asset.AssetSerial,
                        AssetType = Asset.AssetType
                    };
                    result.Add(asset);
                }
                else
                {
                    var asset = new AssetWithAssetLiveStatusDto
                    {
                        Id = Asset.Id,
                        UnitId = Asset.UnitId,
                        AssetSerial = Asset.AssetSerial,
                        AssetType = Asset.AssetType,
                        RawValue = exist.RawValue,
                        ProcessedStatus = exist.ProcessedStatus,
                        IsVerified = exist.IsVerified,
                        LastUpdate = exist.LastUpdate
                    };
                    result.Add(asset);
                }
                
            }
            return result;
        }

        public async Task<AssetWithAssetLiveStatusDto?> GetAssetWithAssetLiveStatusByIdAsync(int id)
        {
            var Asset = await _DbContext.Assets.FindAsync(id);
            if(Asset == null)
            {
                return null;
            }
            var exist = await _DbContext.AssetLiveStatus.FirstOrDefaultAsync(x => x.AssetId == Asset.Id);
            if (exist == null)
            {
                var asset = new AssetWithAssetLiveStatusDto
                {
                    Id = Asset.Id,
                    UnitId = Asset.UnitId,
                    AssetSerial = Asset.AssetSerial,
                    AssetType = Asset.AssetType
                };
                return asset;
            }
            else
            {
                var asset = new AssetWithAssetLiveStatusDto
                {
                    Id = Asset.Id,
                    UnitId = Asset.UnitId,
                    AssetSerial = Asset.AssetSerial,
                    AssetType = Asset.AssetType,
                    RawValue = exist.RawValue,
                    ProcessedStatus = exist.ProcessedStatus,
                    IsVerified = exist.IsVerified,
                    LastUpdate = exist.LastUpdate
                };
                return asset;
            }
        }

        public async Task<List<AssetWithAssetLiveStatusDto>> GetAssetWithAssetLiveStatusByStatusAsync(string status)
        {
            return await _DbContext.Assets.Include(x => x.assetLiveStatus).Select(x => new AssetWithAssetLiveStatusDto
            {
                Id = x.Id,
                UnitId = x.UnitId,
                AssetSerial = x.AssetSerial,
                AssetType = x.AssetType,
                RawValue = x.assetLiveStatus.RawValue,
                ProcessedStatus = x.assetLiveStatus.ProcessedStatus,
                IsVerified = x.assetLiveStatus.IsVerified,
                LastUpdate = x.assetLiveStatus.LastUpdate
            }
            ).ToListAsync();
            
            //List<AssetWithAssetLiveStatusDto> result = new List<AssetWithAssetLiveStatusDto>();
            //var Assets = await _DbContext.Assets.ToListAsync();
            //foreach (var Asset in Assets)
            //{
            //    var exist = await _DbContext.AssetLiveStatus.FirstOrDefaultAsync(x => x.AssetId == Asset.Id);
            //    if (exist == null)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        var asset = new AssetWithAssetLiveStatusDto
            //        {
            //            Id = Asset.Id,
            //            UnitId = Asset.UnitId,
            //            AssetSerial = Asset.AssetSerial,
            //            AssetType = Asset.AssetType,
            //            RawValue = exist.RawValue,
            //            ProcessedStatus = exist.ProcessedStatus,
            //            IsVerified = exist.IsVerified,
            //            LastUpdate = exist.LastUpdate
            //        };
            //        if(asset.ProcessedStatus == status)
            //        {
            //            result.Add(asset);
            //        }
                   
            //    }

            //}
            //return result;
        }
    }
}
