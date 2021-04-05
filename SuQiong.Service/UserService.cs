using SuQiong.Domain.Repository;
using SuQiong.Domain.Service;
using SuQiong.EntityFrameworkCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuQiong.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IUserRepository currentRepository) : base(unitOfWork, currentRepository)
        {

        }
    }
}
