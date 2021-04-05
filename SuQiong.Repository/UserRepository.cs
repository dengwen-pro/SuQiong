using SuQiong.Domain.Repository;
using SuQiong.EntityFrameworkCore.Context;
using SuQiong.EntityFrameworkCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuQiong.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SqMallDbContext context) : base(context)
        {

        }
    }
}
