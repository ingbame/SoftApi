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
        [HttpGet("Get")]
        public async Task<ActionResult> Get(long? id = null)
        {
            var searchResult = await BoFinance.Instance.GetIncomes(id);
            if (searchResult.Error)
                return BadRequest(searchResult.Message);
            return Ok(searchResult.Model);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Post(IncomeEntity request)
        {
            if (request == null)
                return NotFound("No se ha ingresado información");

            var validate = BoFinance.Instance.ValidateIncomeModel(request);
            if (validate.Error)
                return BadRequest(validate.Message);

            var incomeResult = await BoFinance.Instance.NewIncome(request);
            if (incomeResult.Error)
                return BadRequest(incomeResult.Message);
            return Ok(incomeResult.Model);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Put(IncomeEntity request)
        {
            if (request == null)
                return NotFound("No se ha ingresado información");

            var validate = BoFinance.Instance.ValidateIncomeModel(request, true);
            if (validate.Error)
                return BadRequest(validate.Message);

            var incomeResult = await BoFinance.Instance.EditIncome(request);
            if (incomeResult.Error)
                return BadRequest(incomeResult.Message);
            return Ok(incomeResult.Model);
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(IncomeEntity request)
        {
            if (request == null)
                return NotFound("No se ha ingresado información");

            if (!request.IncomeId.HasValue || request.IncomeId <= 0)
                return BadRequest("Debe de tener un Id la entrada que desea manipular.");

            var incomeResult = await BoFinance.Instance.DeleteIncome(request.IncomeId);
            if (incomeResult.Error)
                return BadRequest(incomeResult.Message);
            return Ok(incomeResult.Model);
        }
    }
}
