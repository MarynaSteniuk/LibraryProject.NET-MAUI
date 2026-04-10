using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic; 

namespace LibraryProject.DAL.Entities;

public class Author
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Nationality { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}
