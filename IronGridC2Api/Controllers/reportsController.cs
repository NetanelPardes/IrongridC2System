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
        var criticalAssets = await _reportsRepository.GetAllAsyncCriticalAssets();

        return Ok(criticalAssets);
    }



}

