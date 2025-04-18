﻿
using Microsoft.EntityFrameworkCore;
namespace UserService
{
    public class MyDbContext :DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
    }

}
