using AutoMapper;
using Grpc.Net.ClientFactory;
using Microsoft.AspNetCore.Mvc;
using ProfileService.GRPC;
using WebApiGateway.Models;
using static ProfileService.GRPC.ProfileService;

namespace WebApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly GrpcClientFactory _grpcClientFactory;
        private readonly IMapper _mapper;

        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, GrpcClientFactory grpcClientFactory, IMapper mapper)
        {
            _logger = logger;
            _grpcClientFactory = grpcClientFactory;
            _mapper = mapper;
        }


        // GET api/profile/"guid"
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileModel>> Get([FromRoute]string id)
        {
            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var request = new GetProfileDataByIdRequest()
            {
               ProfileByIdRequest = new ProfileByIdRequest()
               {
                   Id = id
               },
            };

            var result = await profileClient.GetProfileDataByIdAsync(request , cancellationToken: token);

            var response = _mapper.Map<UserProfile, ProfileModel>(result.UserProfile);

            return Ok(response);
        }


        // POST api/profile
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]ProfileModel profileModel)
        {
            if (profileModel == null)
            {
                return BadRequest();
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<ProfileModel, UserProfile>(profileModel);

            var request = new AddProfileDataRequest()
            {
                UserProfile = requestModel
            };

            var result = await profileClient.AddProfileDataAsync(request, cancellationToken: token);

            return Ok();
        }

        // PUT api/profile/
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]ProfileModel profileModel)
        {
            if (profileModel == null)
            {
                return BadRequest();
            }

            var profileClient = _grpcClientFactory.CreateClient<ProfileServiceClient>("ProfileGrpcClient");
            var token = HttpContext.RequestAborted;

            var requestModel = _mapper.Map<ProfileModel, UserProfile>(profileModel);

            var request = new UpdateProfileDataRequest()
            {
                UserProfile = requestModel
            };

            var result = await profileClient.UpdateProfileDataAsync(request, cancellationToken: token);

            return Ok();
        }
    }
}