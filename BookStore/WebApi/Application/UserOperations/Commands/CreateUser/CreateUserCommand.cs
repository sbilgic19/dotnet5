using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    
    public class CreateUserCommand {
        public CreateUserModel Model { get; set; }
        private readonly IMapper _mapper;

        private readonly IBookStoreDBContext _dbContext;
        public CreateUserCommand(IBookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı zaten mevcut");
            }else{
                
                user = _mapper.Map<User>(Model);

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

        }
        }
        

    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}


