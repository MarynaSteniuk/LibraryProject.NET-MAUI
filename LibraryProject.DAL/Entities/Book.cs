using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.DAL.Entities;

public class Book
{
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Isbn { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public string? ImageUrl { get; set; }
}
