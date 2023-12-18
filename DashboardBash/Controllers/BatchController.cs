using DashboardBash.Models;
using DashboardBash.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashboardBash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : Controller
    {
        private readonly DBatch _dbatch;
        public BatchController(IConfiguration configuration)
        {
            _dbatch = new DBatch(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("all")]
        public async Task<List<Batch>> GetBatch()
        {
            return await _dbatch.ListarBatch();
        }

        [HttpGet("search")]
        public async Task<List<Batch>> GetSearhBatch([FromQuery] string name, [FromQuery] DateTime fecha)
        {
            return await _dbatch.SearchProgram(name,fecha);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<List<Batch>>> GetPrograms(int pageNumber, int pageSize)
        {
            var (programs, totalPages) = await _dbatch.GetProgramsAll(pageNumber, pageSize);

            return new JsonResult(new { programs, totalPages });
        }

        [HttpGet("today-list-batch")]
        public async Task<ActionResult<List<Batch>>> GetTodayPrograms()
        {
            return await _dbatch.TodayListBatch(); ;
        }

    }
}
