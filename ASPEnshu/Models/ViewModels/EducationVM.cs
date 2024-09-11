using System.ComponentModel.DataAnnotations.Schema;

namespace ASPEnshu.Models.ViewModels {

    public class EducationForEntry {
        public string? Classcode { get; set; } = null!;

        public string? Seitono { get; set; } = null!;

        public int? Kokugo { get; set; } = null;

        public int? Suugaku { get; set; } = null;

        public int? Rika { get; set; } = null;
    }

    //画面表示表ビューモデル

    public class EducationVM {
        //追加データエリア用
        public EducationForEntry educationForEntry { get; set; } = new EducationForEntry();
        //一覧編集エリア用
        public IList<Education> educations { get; set; } = new List<Education>();
    }
}
