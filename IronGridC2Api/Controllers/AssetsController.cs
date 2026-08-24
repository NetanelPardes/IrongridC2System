using Microsoft.AspNetCore.Mvc;
using IronGridC2Api.Servise;
using IronGridC2Api.DTO;


namespace IronGridC2Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IAssetsRepository _assetsRepository;
    public AssetsController(IAssetsRepository assetsRepository)
    {
        _assetsRepository = assetsRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetGetByIdDto>> GetById(int id)
    {
        var asset = await _assetsRepository.GetByIdAsync(id);

        if (asset == null)
        {
            return NotFound();
        }

        return Ok(asset);
    }


//    {
//  "id": 102,
//  "unitId": 3,
//  "assetSerial": "UAV-COAST-071",
//  "assetType": "UAV"
//}
    [HttpPost("units")]
    public async Task<ActionResult<CreateAssetsDto>> Post(CreateAssetsDto newAssets)
    {
        var exist = await _assetsRepository.GetByIdAsync(newAssets.Id);
        if(exist != null)
        {

            return BadRequest($"This assets with id {newAssets.Id} already exists.");
        }
        var createdAsset = await _assetsRepository.CreateAssetsAsync(newAssets);

        return CreatedAtAction(nameof(GetById),new { id = createdAsset.Id }, createdAsset);
    }



}

