using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ASPEnshu.Models;

[Table("education")]
[PrimaryKey(nameof(Classcode), nameof(Seitono))]
public class Education
{

    [Column("classcode")]
    public string Classcode { get; set; } = null!;

    [Column("seitono")]
    public string Seitono { get; set; } = null!;

    [Column("kokugo")]

    public int? Kokugo { get; set; }

    [Column("suugaku")]
    public int? Suugaku { get; set; }

    [Column("rika")]
    public int? Rika { get; set; }

    [NotMapped]
    public bool DeleteFlg { get; set; } = false;
}
