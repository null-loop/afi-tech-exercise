using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AFIExercise.API.Models;
using AFIExercise.Services;
using Microsoft.AspNetCore.Mvc;

namespace AFIExercise.API.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly ICustomerRegistrationService _customerRegistrationService;

        public RegistrationController(ICustomerRegistrationService customerRegistrationService)
        {
            _customerRegistrationService = customerRegistrationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerRegistrationCreated), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationMessage[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SubmitCustomerRegistration([FromBody] CustomerRegistrationRequest customerRegistrationRequest)
        {
            var result = await _customerRegistrationService.Register(customerRegistrationRequest);

            if (result.IsSuccessful)
            {
                return Ok(new CustomerRegistrationCreated
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    CustomerId = result.CustomerId.Value
                });
            }

            return BadRequest(result.ValidationMessages);
        }

    }
}
