using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.BLL.DTOs;
public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
}
public class CreateAuthorDto
{
    public string Name { get; set; }
    public string Nationality { get; set; }
}
public class UpdateAuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Nationality { get; set; }
}
