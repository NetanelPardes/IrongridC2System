using IronGridC2Api.DTO;
using IronGridC2Api.Models;
using IronGridC2Api.Servise;
using Microsoft.AspNetCore.Mvc;


namespace IronGridC2Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class reportsController : ControllerBase
{
    private readonly IReportsRepository _reportsRepository;
    public reportsController(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }


    [HttpGet("critical-assets")]
    public async Task<ActionResult<List<CriticalAssetsDto>>> GetAllCriticalAssets()
    {
        var criticalAssets = await _reportsRepository.GetAllCriticalAssetsAsync();

        return Ok(criticalAssets);
    }

    [HttpGet("unit/{unitId}/assets")]
    public async Task<ActionResult<List<AssetForUnitDto>>> GetAllAssetForUnitAsync(int unitId)
    {
        var AssetForUnit = await _reportsRepository.GetAllAssetForUnitAsync(unitId);

        return Ok(AssetForUnit);
    }

    [HttpGet("summary-by-unit")]
    public async Task<ActionResult<List<SummaryDto>>> GetSummaryAssets()
    {
        var ummary = await _reportsRepository.GetSummaryAsync();

        return Ok(ummary);
    }



}

