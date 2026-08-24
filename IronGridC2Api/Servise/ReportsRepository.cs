using IronGridC2Api.Data;
using IronGridC2Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace IronGridC2Api.Servise
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IronGridC2DbContext _DbContext;
        public ReportsRepository(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<List<CriticalAssetsDto>> GetAllCriticalAssetsAsync()
        {
            return await _DbContext.Assets
                .Include(n => n.units)
                .Include(n => n.assetLiveStatus)
                .Select(s => new CriticalAssetsDto
                {
                    AssetId = s.Id,
                    AssetSerial = s.AssetSerial,
                    AssetType = s.AssetType,
                    UnitName = s.units.UnitName,
                    Sector = s.units.Sector,
                    ProcessedStatus = s.assetLiveStatus.ProcessedStatus,
                    IsVerified = s.assetLiveStatus.IsVerified,
                    LastUpdate = s.assetLiveStatus.LastUpdate
                })
                .ToListAsync();
        }
        public async Task<List<AssetForUnitDto>> GetAllAssetForUnitAsync(int unitID)
        {
            return await _DbContext.Assets
                .Include(n => n.units)
                .Include(n => n.assetLiveStatus)
                .Where(w => w.units.Id == unitID)
                .Select(s => new AssetForUnitDto
                {
                    AssetId = s.Id,
                    AssetSerial = s.AssetSerial,
                    AssetType = s.AssetType,
                    ProcessedStatus = s.assetLiveStatus.ProcessedStatus,
                    IsVerified = s.assetLiveStatus.IsVerified,
                    LastUpdate = s.assetLiveStatus.LastUpdate
                })
                .ToListAsync();
        }
        public async Task<List<SummaryDto>> GetSummaryAsync()
        {
            return await _DbContext.Units
                .Include(n => n.assets)
                .ThenInclude(n => n.assetLiveStatus)
                .Select(s => new SummaryDto
                {
                    UnitId = s.Id,
                    UnitName = s.UnitName,
                    Sector = s.Sector,
                    totalAssets = s.assets.Count(),
                    stableAssets = s.assets.Count(g => g.assetLiveStatus.ProcessedStatus == "Stable"),
                    warningAssets = s.assets.Count(g => g.assetLiveStatus.ProcessedStatus == "Warning"),
                    unverifiedAssets = s.assets.Count(g => g.assetLiveStatus.ProcessedStatus != "Warning" && g.assetLiveStatus.ProcessedStatus == "Stable")
                })
                .ToListAsync();
        }
    }
}
