using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET.Business;
using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Hypermedia.Filters;
using RestWithASP_NET.Model;
using RestWithASP_NET.Repository;
using System.Net;
using System.Threading.Tasks;

namespace RestWithASP_NET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery]string name,
            string sortDirection,
            int pageSize,
            int page)
        {

            return Ok(_personBusiness.FindWithPagedSearch( name, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var person = _personBusiness.FindByName(firstName, lastName);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType((201), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            person = _personBusiness.Create(person);
            return StatusCode((int)HttpStatusCode.Created, person);
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = _personBusiness.Desable(id);

            return Ok(person);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }


        /* calculater
                [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
                public IActionResult Subtraction(string firstNumber, string secondNumber)
                {
                    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                    {
                        var sum = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                        return Ok(sum.ToString());
                    }
                    return BadRequest("Ivaladi Input");
                }

                [HttpGet("multiplicstion/{firstNumber}/{secondNumber}")]
                public IActionResult Multiplication(string firstNumber, string secondNumber)
                {
                    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                    {
                        var sum = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                        return Ok(sum.ToString());
                    }
                    return BadRequest("Ivaladi Input");
                }

                [HttpGet("division/{firstNumber}/{secondNumber}")]
                public IActionResult Division(string firstNumber, string secondNumber)
                {
                    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                    {
                        var sum = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                        return Ok(sum.ToString());
                    }
                    return BadRequest("Ivaladi Input");
                }

                [HttpGet("mean/{firstNumber}/{secondNumber}")]
                public IActionResult Mean(string firstNumber, string secondNumber)
                {
                    if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
                    {
                        var sum = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                        return Ok(sum.ToString());
                    }
                    return BadRequest("Ivaladi Input");
                }

                [HttpGet("squareroot/{firstNumber}")]
                public IActionResult SquareRoot(string firstNumber)
                {
                    if (IsNumeric(firstNumber) )
                    {
                        var squareRoot = Math.Sqrt((double)ConvertToDecimal(firstNumber));
                        return Ok(squareRoot.ToString());
                    }
                    return BadRequest("Ivaladi Input");
                }

                private decimal ConvertToDecimal(string strNumber)
                {
                    decimal decimalValue;
                    if (decimal.TryParse(strNumber, out decimalValue)) 
                    {
                        return decimalValue;
                    }

                    return 0 ;
                }

                private bool IsNumeric(string strNumber)
                {
                    double number;
                    bool isNumeric = double.TryParse(
                        strNumber,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.NumberFormatInfo.InvariantInfo,
                        out number);
                    return isNumeric;
                }*/
    }
}
