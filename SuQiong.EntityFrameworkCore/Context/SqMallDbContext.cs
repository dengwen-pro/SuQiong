using Microsoft.EntityFrameworkCore;
using SuQiong.EntityFrameworkCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuQiong.EntityFrameworkCore.Context
{
    public class SqMallDbContext : DbContext
    {
        public SqMallDbContext(DbContextOptions<SqMallDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        //自定义DbContext实体属性名与数据库表对应名称（默认 表名与属性名对应是 User与Users）
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
