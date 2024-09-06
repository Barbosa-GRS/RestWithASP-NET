using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET.Business;
using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Hypermedia.Filters;
using RestWithASP_NET.Model;
using RestWithASP_NET.Repository;

namespace RestWithASP_NET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
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

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
           
            return Ok(_personBusiness.FindAll());
        }
        
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
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
