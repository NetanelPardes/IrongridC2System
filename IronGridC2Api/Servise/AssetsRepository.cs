using IronGridC2Api.Data;
using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGridC2Api.Servise
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly IronGridC2DbContext _DbContext;
        public AssetsRepository(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }



        public async Task<AssetGetByIdDto?> GetByIdAsync(int id)
        {
            return await _DbContext.Assets.Select(x => new AssetGetByIdDto 
            {
                Id = x.Id ,
                UnitId = x.UnitId,
                AssetSerial = x.AssetSerial,
                AssetType = x.AssetType
            })
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<CreateAssetsDto> CreateAssetsAsync(CreateAssetsDto newAssets)
        {
            var CreatedAssets = new Assets
            {
                Id = newAssets.Id,
                UnitId = newAssets.UnitId,
                AssetSerial = newAssets.AssetSerial,
                AssetType = newAssets.AssetType
            };
            _DbContext.Assets.Add(CreatedAssets);

            await _DbContext.SaveChangesAsync();

            return newAssets;
        }


        public async Task<Assets?> UpdateAssetsAsync( int id, UpdateAssetsDto updateAssets)
        {
            var existingAsset = await _DbContext.Assets.FindAsync(id);

            if (existingAsset == null)
            {
                return null;
            }

            existingAsset.UnitId = updateAssets.UnitId;
            existingAsset.AssetSerial = updateAssets.AssetSerial;
            existingAsset.AssetType = updateAssets.AssetType;

            await _DbContext.SaveChangesAsync();

            return existingAsset;
        }



    }
}
