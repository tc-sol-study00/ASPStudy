using ASPEnshu.Data;
using ASPEnshu.Models.Interfaces;
using ASPEnshu.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASPEnshu.Models.Services {
    public class EducationService : IService<EducationVM> {
        /*
         * メンバー
         */
        //DBコンテキスト
        private readonly ASPEnshuContext _context;

        //コントローラとやり取りするビューモデル
        public EducationVM EducationVM { get; set; }

        //コンストラクタ
        public EducationService(ASPEnshuContext context) {
            this._context = context;                //DBコンテクストのセット
            this.EducationVM = new EducationVM();   //ビューモデルの初期化
        }
        /*
         * 共通メソッド
         */
        /// <summary>
        /// エンティティ(education)の読み込み
        /// クラスコードと生徒Ｎｏでソート
        /// 利用する際には、ToList()などを行う事
        /// </summary>
        /// <returns>読み込み結果IQueryable<Education></returns>
        private IQueryable<Education> EducationQuery() {
            IQueryable<Education> educations = _context.Education.OrderBy(x => x.Classcode).ThenBy(x => x.Seitono);
            return educations;
        }

        /*
         * 初期表示用メソッド
         */

        /// <summary>
        /// ビューモデルを初期化
        /// ビューモデルに、Educationの一覧をセット
        /// </summary>
        /// <returns>Educationの一覧</returns>
        public async Task<EducationVM> SetViewModel() {
             this.EducationVM.educations = await EducationQuery().ToListAsync();
            return this.EducationVM;
        }

        /*
         * Post後処理
         */

        /// <summary>
        /// educationにPostデータのデータ閲覧・編集エラーの内容を上書き
        /// </summary>
        /// <param name="argEducations">Postされたeducationデータ</param>
        /// <returns>postデータで更新されたeducationデータ(savechangeに反映される)</returns>
        private async Task<IList<Education>> OverPostDataToEducation(IList<Education> argEducations) {
            //educationの一覧をビューモデルにセット
            EducationVM.educations = await EducationQuery().ToListAsync();

            //postデータを上乗せする
            foreach (var item in argEducations) {
                //Postデータのマッチする上乗せ対象を抽出
                Education? updatedEducation =
                    this.EducationVM.educations.Where(x => x.Classcode == item.Classcode && x.Seitono == item.Seitono).SingleOrDefault();

                if (updatedEducation != null) { 
                    if (item.DeleteFlg == true) {   //postデータの編集用一覧のデータに削除フラグがある場合
                        _context.Education.Remove(updatedEducation);    //削除
                    }
                    else {  //削除フラグが設定されていない場合は、postデータの内容を上乗せ
                        updatedEducation.Kokugo = item.Kokugo;
                        updatedEducation.Suugaku = item.Suugaku;
                        updatedEducation.Rika = item.Rika;
                    }
                }
                else {  //例外処理（発生想定：DBの主キーに不可視データがあるなど）
                    throw new Exception("Postデータ反映エラー");
                }
            }
            //上乗せ後の編集データを返却
            return (this.EducationVM.educations);
        }

        /// <summary>
        /// postされたデータ追加エリアのデータをエンティティeducationに追加
        /// </summary>
        /// <param name="argEducationForEntry">postされたデータ追加エリアのデータ</param>
        /// <returns>追加されたデータ追加エリアのデータ</returns>
        private Education AddPostDataToEducation(EducationForEntry argEducationForEntry) {
            //追加するeducationへのデータをセット
            Education addedEducation = new Education() {
                Classcode = argEducationForEntry.Classcode,
                Seitono = argEducationForEntry.Seitono,
                Kokugo = argEducationForEntry.Kokugo,
                Suugaku = argEducationForEntry.Suugaku,
                Rika = argEducationForEntry.Rika
            };

            //DBへ追加手続き
            _context.Education.Add(addedEducation);
            //追加手続き後のeducationデータの返却
            return (addedEducation);
        }

        /// <summary>
        /// Post後のPostデータ反映処理
        /// </summary>
        /// <param name="argEducationVM">Contorollerから渡されるビューモデル（Post後)</param>
        /// <returns></returns>
        public async Task<EducationVM> AfterPostExecution(EducationVM argEducationVM) {
            //educationにPostデータのデータ閲覧・編集エラーの内容を上書き
            argEducationVM.educations = await OverPostDataToEducation(argEducationVM.educations);

            //postされたデータ追加エリアのデータをエンティティeducationに追加
            if (!string.IsNullOrEmpty(argEducationVM.educationForEntry.Classcode)) {
                AddPostDataToEducation(argEducationVM.educationForEntry);
            }
            //DB反映
            await _context.SaveChangesAsync();

            //再描画用ビューモデルセット
            EducationVM = new EducationVM {
                educations = await EducationQuery().ToListAsync()
            };
            //セットされたビューモデルを返却
            return EducationVM;
        }

    }
}