using Microsoft.AspNetCore.Mvc;
using Toggler_Service.DTOs;
using Toggler_Service.Services;

namespace Toggler_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToggleController : ControllerBase
    {
        private readonly IToggleService _service;
        public ToggleController(IToggleService service)
        {
            _service = service;
        }

        // POST api/<ServiceController>
        [HttpPost]
        public ActionResult<ApiResponseDTO> Post([FromBody] ToggleDTO toggle)
        {
            var res = _service.Register(toggle);
            if (res.IsSuccess)
            {
                if (toggle.ToggleService != null)
                {
                    res = _service.AddServices(toggle.Name, toggle.ToggleService);
                    if (!res.IsSuccess)
                    {
                        var error = res.ErrorMessage;
                        res.ErrorMessage = "Toggle successfully created, but Service association encountered an error." + error;
                        return Ok(res);
                    }
                }
                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPost("Bulk")]
        public ActionResult<ApiResponseDTO> BulkPost([FromBody] ToggleBatchCreationDTO dto)
        {
            var res = _service.Bulk(dto);
            if (res.IsSuccess)
            {

                return Ok(res);
            }

            return BadRequest(res);
        }

        [HttpPatch("{name}")]
        public ActionResult<ApiResponseDTO> RegisterServices(string name, [FromBody] ToggleServiceDTO dto)
        {
            var res = _service.AddServices(name, dto);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }
    }
}
