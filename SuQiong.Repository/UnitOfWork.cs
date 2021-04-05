using SuQiong.Domain.Repository;
using SuQiong.EntityFrameworkCore.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuQiong.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqMallDbContext myDbContext;

        public UnitOfWork(SqMallDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public SqMallDbContext GetDbContext()
        {
            return myDbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await myDbContext.SaveChangesAsync();
        }
    }
}
