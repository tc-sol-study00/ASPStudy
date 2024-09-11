
namespace ASPEnshu.Models.Interfaces {
    public interface IService<T> {

        /// <summary>
        /// ビューモデルを初期化
        /// ビューモデルに、Educationの一覧をセット
        /// </summary>
        public Task<T> SetViewModel();

        /// <summary>
        /// Post後のPostデータ反映処理
        /// </summary>
        /// <param name="argVM">Contorollerから渡されるビューモデル（Post後)</param>
        /// <returns></returns>
        public Task<T> AfterPostExecution(T argVM);
    }
}
