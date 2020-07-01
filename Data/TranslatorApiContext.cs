using Microsoft.EntityFrameworkCore;
using Translator.Data.Models;

namespace Translator.Data
{
    public class TranslatorApiContext : DbContext
    {

        public TranslatorApiContext(DbContextOptions<TranslatorApiContext> opt) : base(opt)
        { 

        }
        public DbSet<Languages> Languages{get; set;}
        public DbSet<Tags> Tags{get; set;}
        public DbSet<Translations> Translations{get; set;}
    }
}
