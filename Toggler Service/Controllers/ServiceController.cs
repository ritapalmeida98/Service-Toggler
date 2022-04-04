using Microsoft.AspNetCore.Mvc;
using Toggler_Service.DTOs;
using Toggler_Service.Services;

namespace Toggler_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        { 
            _service = service; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // GET api/<ServiceController>/5
        [HttpGet("{identifier}/{version}")]
        public IActionResult Get(string identifier, string version)
        {
            var service = _service.Get(identifier, version);
            if(service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        // GET api/<ServiceController>/5
        [HttpGet("Toggles/{identifier}/{version}")]
        public IActionResult GetToggles(string identifier, string version)
        {
            var toggles = _service.GetToggles(identifier, version);
            if (toggles == null || toggles.Count == 0)
            {
                return NotFound();
            }
            return Ok(toggles);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public ActionResult<ApiResponseDTO> Post([FromBody] ServiceDTO serviceInput)
        {
            var res = _service.Register(serviceInput);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

        // POST api/<ServiceController>
        [HttpPost("Bulk")]
        public ActionResult<ApiResponseDTO> BulkPost([FromBody] List<ServiceDTO> list)
        {
            var res = _service.RegisterMany(list);
            if (res.IsSuccess)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }
    }
}
