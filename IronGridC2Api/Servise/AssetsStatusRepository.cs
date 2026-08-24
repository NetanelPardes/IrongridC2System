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

    }
}
