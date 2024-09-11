using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcSample.Models {

    [Table("employee")]
    public class Employee {
        [Column("employee_no")]
        [DisplayName("従業員番号")]
        [MaxLength(5, ErrorMessage = "{0}は最大文字数は{1}文字までです")]
        [Required(ErrorMessage = "{0}は必須入力です")]
        [Key]
        public string EmployeeNo { get; set; }

        [Column("employee_name")]
        [DisplayName("氏名")]
        [MaxLength(20, ErrorMessage = "最大文字数は20文字までです")]
        [Required(ErrorMessage = "必須入力です")]
        [RegularExpression("^[A-Z]{3}[0-9]{4}$", ErrorMessage = "{0}は、最初の３文字は英字、続いて数字四桁を入力してください")]

        public string EmployeeName { get; set; }

        [Column("current_address")]
        [DisplayName("住所")]
        [MaxLength(50)]
        [Required]
        public string CurrentAddress { get; set; }

        [Column("birthday")]
        [DisplayName("誕生日")]
        [Required]
        [DataType(DataType.Date)]
        [ValidBirthDay]
        public DateOnly BirthDay { get; set; }

        [Column("age")]
        [DisplayName("年齢")]
        [Range(0, 80)]
        [Required]
        public int Age { get; set; }

        [Column("department")]
        [DisplayName("所属")]
        [MaxLength(20)]
        [Required]
        public string Department { get; set; }

    }
}
