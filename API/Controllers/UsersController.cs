using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Features.Transactions.Commands;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, UserManager<AppUser> userManager, IMediator mediator)
        {
            _mediator = mediator;
            _userManager = userManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            userParams.CurrentEmail = User.GetEmail();
            var users = await _userRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpGet("agents")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAgents([FromQuery] UserParams userParams)
        {
            var agents = await _userManager.GetUsersInRoleAsync("Agent");
            _mapper.Map<IEnumerable<MemberDto>>(agents);
            return Ok(agents);

        }

        [HttpGet("{email}", Name = "GetUserByEmail")]
        public async Task<ActionResult<MemberDto>> GetUser(string email)
        {
            return await _userRepository.GetMemberAgentAsync(email);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(User.GetEmail());

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        [HttpPost("create-city")]
        public async Task<ActionResult<TransactionDto>> CreateUserCity(CityDto cityDto)
        {
            var city = await _mediator.Send(new CreateUserCity.Command(cityDto));

            if(city != null){
                return Ok(city);
            }
            return BadRequest("Failed to create transaction");

        }
    }
}