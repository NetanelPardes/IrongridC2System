using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using IronGridC2Api.Servise;
using Microsoft.AspNetCore.Mvc;


namespace IronGridC2Api.Controllers;

[ApiController]
[Route("api/assets-status")]
public class assets_statusController : ControllerBase
{
    private readonly IAssetsStatusRepository _assetsStatusRepository;
    public assets_statusController(IAssetsStatusRepository assetsStatusRepository)
    {
        _assetsStatusRepository = assetsStatusRepository;
    }


    [HttpGet]
    public async Task<ActionResult<List<AssetWithAssetLiveStatusDto>>> GetAll()
    {
        var AssetWithAssetLive = await _assetsStatusRepository.GetAllAssetLiveStatusAsync();

        return Ok(AssetWithAssetLive);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetWithAssetLiveStatusDto>> GetById(int id)
    {
        var AssetWithStatus = await _assetsStatusRepository.GetAssetWithAssetLiveStatusByIdAsync(id);

        if (AssetWithStatus == null)
        {
            return NotFound();
        }

        return Ok(AssetWithStatus);
    }



}

