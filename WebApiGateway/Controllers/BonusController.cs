using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using ProfileService.GRPC;
using Swashbuckle.AspNetCore.Annotations;
using WebApiGateway.Models;
using static ProfileService.GRPC.Profiler;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusController : ControllerBase
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        private readonly ILogger<BonusController> _logger;

        public BonusController(ILogger<BonusController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }


        // GET api/bonus/"guid"
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Successfully get bonus(es)", typeof(List<DiscountModel>))]
        public async Task<ActionResult<List<DiscountModel>>> Get([FromRoute]string id)
        {
            var profileClient = _grpcClientFactory.CreateClient<ProfilerClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var request = new GetDiscountsRequest()
            {
               Profilebyidrequest = new ProfileByIdRequest()
               {
                   Id = id
               },
            };

            var result = await profileClient.GetDiscountsAsync(request , cancellationToken: token);

            List<DiscountModel> response = _mapper.Map<IEnumerable<Discount>, List<DiscountModel>>(result.Discounts);

            return Ok(response);
        }


        // POST api/bonus
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]DiscountModel discountModel)
        {
            if (discountModel == null)
            {
                return BadRequest();
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfilerClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<DiscountModel, Discount>(discountModel);

            var request = new AddDiscountRequest()
            {
                Discount = requestModel
            };

            var result = await profileClient.AddDiscountAsync(request, cancellationToken: token);

            return Ok();
        }

        // PUT api/bonus/
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] DiscountModel discountModel)
        {
            if (discountModel == null)
            {
                return BadRequest();
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfilerClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<DiscountModel, Discount>(discountModel);

            var request = new UpdateDiscountRequest()
            {
                Discount = requestModel
            };

            var result = await profileClient.UpdateDiscountAsync(request, cancellationToken: token);

            return Ok();
        }


    }

}
