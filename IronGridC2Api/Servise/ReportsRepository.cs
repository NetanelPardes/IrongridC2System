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
        public async Task<List<CriticalAssetsDto>> GetAllAsyncCriticalAssets()
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
    }
}
