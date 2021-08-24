using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Application.UserOperations.Commands.CreateUser;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateTokenCommand;
using WebApi.TokenOperations.Models;
using WebApi.Application.UserOperations.Commands.RefreshToken;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDBContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration; // appsettingsten bilgileri çekmek için

        public UserController(IBookStoreDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser) 
        {
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context,_mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }

    }   



}
