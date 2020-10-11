using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AFIExercise.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFIExercise.API.Controllers
{
    [Produces("application/json")]
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly Mapper _mapper;

        public RegistrationController(ICustomerRegistrationService customerRegistrationService, Mapper mapper)
        {
            _customerRegistrationService = customerRegistrationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Validates a customer registration request and creates the corresponding resource if successful.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/registration
        ///     {
        ///        "firstName": "Bob",
        ///        "surname": "Smith",
        ///        "policyNumber": "AB-123456",
        ///        "dateOfBirth": "2020-01-01",
        ///        "emailAddress": "robert@smith.co.uk"
        ///     }
        ///
        /// </remarks>
        /// <param name="customerRegistrationRequest">Registration request to validate and create.</param>
        /// <returns>Either the newly created registrations unique identifier or validation messages describing problems with the request.</returns>
        /// <response code="200">The new customer registrations unique identifier.</response>
        /// <response code="400">Validation messages describing problems with the request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Models.CustomerRegistrationCreated), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Models.ValidationMessage[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SubmitCustomerRegistration([FromBody] Models.CustomerRegistrationRequest customerRegistrationRequest)
        {
            var result = await _customerRegistrationService.Register(_mapper.Map<CustomerRegistrationRequest>(customerRegistrationRequest));

            if (result.IsSuccessful)
            {
                return Ok(new Models.CustomerRegistrationCreated
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    CustomerId = result.CustomerId.Value
                });
            }

            return BadRequest(result.ValidationMessages.Select(vm=>_mapper.Map<Models.ValidationMessage>(vm)).ToArray());
        }
    }
}
