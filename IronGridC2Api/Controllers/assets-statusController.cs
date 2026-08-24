using IronGridC2Api.Controllers;
using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using IronGridC2Api.Servise;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IronGridC2Api.Controllers;

[ApiController]
[Route("api/assets-status")]
public class assets_statusController : ControllerBase
{
    private readonly IDatabase _redis;
    private readonly IAssetsStatusRepository _assetsStatusRepository;
    public assets_statusController(IAssetsStatusRepository assetsStatusRepository, IConnectionMultiplexer redis)
    {
        _assetsStatusRepository = assetsStatusRepository;
        _redis = redis.GetDatabase();
    }

    [HttpGet]
    public async Task<ActionResult<List<AssetWithAssetLiveStatusDto>>> GetAll()
    {
        var AssetWithAssetLive = await _assetsStatusRepository.GetAllAssetLiveStatusAsync();

        return Ok(AssetWithAssetLive);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetWithAssetLiveStatusDto>> GetAssetWithAssetLiveStatusByIdAsync(int id)
    {
        string key = $"asset-status:{id}";

        var json = await _redis.StringGetAsync(key);

        if (json.HasValue)
        {
            var result = JsonSerializer.Deserialize<AssetWithAssetLiveStatusDto>(json.ToString());
            Console.WriteLine("from redis");
            return Ok(result);
        }
        var AssetWithStatus = await _assetsStatusRepository.GetAssetWithAssetLiveStatusByIdAsync(id);

        if (AssetWithStatus == null)
        {
            return NotFound();
        }
        var asset = JsonSerializer.Serialize(AssetWithStatus);
        await _redis.StringSetAsync(key, asset, TimeSpan.FromMinutes(5));
        Console.WriteLine("from database");
        return Ok(AssetWithStatus);
    }

    [HttpGet("status")]
    public async Task<ActionResult<List<AssetWithAssetLiveStatusDto>>> GetAllByStatus([FromQuery] string status)
    {
        var AssetWithAssetLiveByStatus = await _assetsStatusRepository.GetAssetWithAssetLiveStatusByStatusAsync(status);

        return Ok(AssetWithAssetLiveByStatus);
    }

}       