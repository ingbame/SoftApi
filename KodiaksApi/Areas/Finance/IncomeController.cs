using KodiaksApi.Core;
using KodiaksApi.Entity.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KodiaksApi.Areas.Finance
{
    [Area("Finance")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class IncomeController : ControllerBase
    {
        [HttpGet("GetAllIncomes")]
        public ActionResult GetIncomes()
        {
            var searchResult = BoFinance.Instance.GetIncomes();
            if (searchResult.Error)
                return BadRequest(searchResult.Message);
            return Ok(searchResult.Model);
        }
        [HttpPost("CreateNewIncome")]
        public ActionResult PostCreate(IncomeEntity request)
        {
            if (request == null)
                return NotFound("No se ha ingresado información");

            var validate = BoFinance.Instance.ValidateIncomeModel(request);
            if (validate.Error)
                return BadRequest(validate.Message);

            var incomeResult = BoFinance.Instance.NewIncome(request);
            if (incomeResult.Error)
                return BadRequest(incomeResult.Message);
            return Ok(incomeResult.Model);
        }
    }
}
