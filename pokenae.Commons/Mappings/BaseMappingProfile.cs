using AutoMapper;

namespace pokenae.Commons.Mappings
{
    /// <summary>
    /// ベースマッピングプロファイルクラス。
    /// 共通のマッピング設定やカスタムコンバーターを定義します。
    /// </summary>
    public abstract class BaseMappingProfile : Profile
    {
        /// <summary>
        /// コンストラクタ。
        /// 共通のマッピング設定やカスタムコンバーターを初期化します。
        /// </summary>
        protected BaseMappingProfile()
        {
            // 共通のマッピング設定やカスタムコンバーターをここに記述
        }
    }
}
