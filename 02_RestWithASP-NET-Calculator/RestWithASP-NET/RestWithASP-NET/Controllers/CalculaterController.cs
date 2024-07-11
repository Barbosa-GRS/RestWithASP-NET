using Microsoft.AspNetCore.Mvc;

namespace RestWithASP_NET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculaterController : ControllerBase
    {
        

        private readonly ILogger<CalculaterController> _logger;

        public CalculaterController(ILogger<CalculaterController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Get(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
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
        }
    }
}
