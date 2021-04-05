using SuQiong.EntityFrameworkCore.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuQiong.Domain.Repository
{
    public interface IUnitOfWork
    {
        SqMallDbContext GetDbContext();

        Task<int> SaveChangesAsync();
    }
}
