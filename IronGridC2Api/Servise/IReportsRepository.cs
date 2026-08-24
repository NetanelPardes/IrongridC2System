using IronGridC2Api.DTO;

namespace IronGridC2Api.Servise
{
    public interface IReportsRepository
    {
        Task<List<CriticalAssetsDto>> GetAllCriticalAssetsAsync();
        Task<List<AssetForUnitDto>> GetAllAssetForUnitAsync(int unitID);
        Task<List<SummaryDto>> GetSummaryAsync();
    }
}
