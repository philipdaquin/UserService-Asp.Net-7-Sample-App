using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Repository;

namespace Domains;


[Table("users")]
public class User: IEntity<int>
{
    [Key]
    public int Id { get; set; }

    [Column(name: "name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    

    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }
}
