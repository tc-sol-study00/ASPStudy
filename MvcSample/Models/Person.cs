using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSample.Models;

[Table("person")]
public partial class Person
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("name")]
    [Display(Name = "名前")]
    public string Name { get; set; } = null!;

    [Display(Name = "年齢")]
    [Column("age")]
    [DisplayFormat(DataFormatString = "{0}歳")]
    public int Age { get; set; }
}
