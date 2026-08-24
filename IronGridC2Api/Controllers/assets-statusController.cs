using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using IronGridC2Api.Servise;
using Microsoft.AspNetCore.Mvc;


namespace IronGridC2Api.Controllers;

[ApiController]
[Route("api/assets-status")]
public class assets_statusController : ControllerBase
{
    private readonly IAssetsStatusRepository _assetsStatusRepository1;
    public assets_statusController(IAssetsStatusRepository assetsStatusRepository)
    {
        _assetsStatusRepository1 = assetsStatusRepository;
    }


    [HttpGet]
    public async Task<ActionResult<List<AssetWithAssetLiveStatusDto>>> GetAll()
    {
        var AssetWithAssetLive = await _assetsStatusRepository1.GetAllAssetLiveStatusAsync();

        return Ok(AssetWithAssetLive);
    }


}

