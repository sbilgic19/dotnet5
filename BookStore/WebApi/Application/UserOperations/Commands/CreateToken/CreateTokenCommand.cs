using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateTokenCommand
{
    
    public class CreateTokenCommand {
        public CreateTokenModel Model { get; set; }
        private readonly IMapper _mapper;

        private readonly IBookStoreDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null){
                // Token yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);


                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();
                return token;
            }else{
                throw new InvalidOperationException("Kullanıcı adı veya şifre hatalı");
            }
        }

    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}