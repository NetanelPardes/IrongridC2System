using IronGridC2Api.DTO;
using IronGridC2Api.Models;

namespace IronGridC2Api.Servise
{
    public interface IAssetsRepository
    {
        Task<AssetGetByIdDto?> GetByIdAsync(int id);
        Task<CreateAssetsDto> CreateAssetsAsync(CreateAssetsDto newAssets);
        Task<Assets?> UpdateAssetsAsync(int id, UpdateAssetsDto updateAssets);
        Task<bool> DeleteAssetsAsync(int id);
    }
}
