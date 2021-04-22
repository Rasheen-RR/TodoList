using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TodoList.Models;

namespace TodoList.Context
{
    public class RepositoryContext : DbContext
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=todo-list;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        public DbSet<TodoItem> TodoItem { get; set; }

    }
}
