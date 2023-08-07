using Api.Entities;
using Api.Interfaces.Services;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CnabController : ControllerBase
    {
        private readonly ICnabService _cnabService;

        public CnabController(ICnabService cnabService)
        {
            _cnabService = cnabService;
        }

        [HttpPost]
        public ActionResult<IEnumerable<CnabEntryModel>> Upload(IFormFile file)
        {
            try
            {
                var result = _cnabService.ImportFile(file).CnabEntries.Select(c => new CnabEntryModel(c));
                return Content(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<CnabFileModel>> List()
        {
            try
            {
                var result = _cnabService.List().Select(c => new CnabFileModel(c));
                return Content(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("file/{fileName}")]
        public ActionResult<IEnumerable<CnabFileModel>> Get(string fileName)
        {
            try
            {
                var result = _cnabService.Get(fileName).Select(c => new CnabFileModel(c));
                return Content(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
