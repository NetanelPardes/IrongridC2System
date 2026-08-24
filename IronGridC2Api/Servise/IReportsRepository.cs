using IronGridC2Api.DTO;

namespace IronGridC2Api.Servise
{
    public interface IReportsRepository
    {
        Task<List<CriticalAssetsDto>> GetAllAsyncCriticalAssets();
    }
}
