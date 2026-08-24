using Microsoft.AspNetCore.Mvc;
using IronGridC2Api.Servise;
using IronGridC2Api.DTO;


namespace IronGridC2Api.Controllers;

[ApiController]
[Route("[controller]")]
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

}

