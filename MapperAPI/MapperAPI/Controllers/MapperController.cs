using MapperAPI.Common;
using MapperAPI.Models;
using MapperAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapperController : ControllerBase
    {
        private readonly IMapper _mapperRepo;
        private readonly ILogger _logger;
        public MapperController(IMapper mapperRepo, ILogger<MapperController> logger)
        {
            _mapperRepo = mapperRepo;
            _logger = logger;
        }

        [HttpGet("unmappedEntities")]
        public async Task<IActionResult> GetUnMappedEntities([FromQuery] string searchStr = "")
        {
            APIResponse <IEnumerable<MapperModel>> response = new APIResponse<IEnumerable<MapperModel>> ();
            try
            {
                response.Data = await _mapperRepo.GetUnMappedEntities(searchStr);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching unmapped entities with searchStr={SearchStr}", searchStr);
                response.Data = null;
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpGet("mappedEntites")]
        public async Task<IActionResult> GetMappedEntities([FromQuery] string searchStr = "")
        {
            APIResponse<IEnumerable<MapperModel>> response = new APIResponse<IEnumerable<MapperModel>>();
            try
            {
                response.Data = await _mapperRepo.GetMappedEntities(searchStr);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error fetching mapped entities with searchStr={SearchStr}", searchStr);
                response.Data= null;
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpPost("updateMappingStatus")]
        public async Task<IActionResult> UpdateMappingStatus([FromBody] string unmappedIds, string mappedIds)
        {
            APIResponse<int> response = new APIResponse<int> ();
            try
            {
                response.Data = await _mapperRepo.UpdateMappingStatus(unmappedIds, mappedIds, "hitesh");
                response.IsSuccess= true;
                return Ok (response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating mapping status. UnmappedIds={Unmapped}, MappedIds={Mapped}", unmappedIds, mappedIds);               
                response.ErrorMessage = "Failed to update mapping status.";
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
    }
}
