using IronGridC2Api.DTO;

namespace IronGridC2Api.Servise
{
    public interface IAssetsRepository
    {
        Task<AssetGetByIdDto?> GetByIdAsync(int id);
        
    }
}
