using IronGridC2Api.DTO;

namespace IronGridC2Api.Servise
{
    public interface IAssetsStatusRepository
    {
        Task<List<AssetWithAssetLiveStatusDto>> GetAllAssetLiveStatusAsync();
    }
}
