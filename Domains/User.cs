using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Repository;

namespace Domains;


[Table("user")]
public class User: IEntity<int>
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column(name: "name", TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    

    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }
}
