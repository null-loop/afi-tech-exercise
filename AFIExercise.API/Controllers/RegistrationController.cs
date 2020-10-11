using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AFIExercise.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AFIExercise.API.Controllers
{
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
