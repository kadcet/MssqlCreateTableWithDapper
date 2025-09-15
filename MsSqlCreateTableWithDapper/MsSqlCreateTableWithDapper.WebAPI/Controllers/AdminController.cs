using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MsSqlCreateTableWithDapper.Bll.Absract;

namespace MsSqlCreateTableWithDapper.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet(Name = "MigrateDatabase")]
        public ActionResult MigrateDatabase()
        {


            var res = adminService.MigrateDatabase();

            if (res.Success)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }

    }
}
