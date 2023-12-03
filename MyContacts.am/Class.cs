using MyContacts.am.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace DataAccess
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}