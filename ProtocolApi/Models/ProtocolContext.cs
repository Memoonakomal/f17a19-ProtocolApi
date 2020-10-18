using Microsoft.EntityFrameworkCore;

namespace ProtocolApi.Models
{
    public class ProtocolContext : DbContext
    {
        public ProtocolContext(DbContextOptions<ProtocolContext> options)
            : base(options)
        {
        }

        public DbSet<Protocol> TodoItems { get; set; }
    }
}