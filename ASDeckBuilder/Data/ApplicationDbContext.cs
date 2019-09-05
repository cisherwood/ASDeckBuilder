using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASDeckBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardCategories> CardCategories { get; set; }
        public DbSet<CardTags> CardTags { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Decks> Decks { get; set; }



    }
}
