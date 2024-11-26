using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Document_search_bot.Models;

namespace Document_search_bot.Data
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options) { }
        public DbSet<Document> Documents { get; set; }
    }
}
