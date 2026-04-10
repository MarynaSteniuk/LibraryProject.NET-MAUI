using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using LibraryProject.DAL.Entities;

namespace LibraryProject.DAL;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);
        modelBuilder.Entity<Author>().Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)       
            .WithMany(a => a.Books)      
            .HasForeignKey(b => b.AuthorId); 
    }
}
