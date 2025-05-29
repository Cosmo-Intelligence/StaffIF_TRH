
namespace Serv_UsersIFLinkage.Util
{
  /// <summary>
  /// アプリケーションコンフィグ定義
  /// </summary>
  public sealed class AppConfigParameter
  {
    #region 共通

    /// <summary>
    /// USER接続文字列
    /// </summary>
    public static string USER_Conn = "USER_ConnectionString";

    /// <summary>
    /// YOKOGAWA接続文字列
    /// </summary>
    public static string YOKO_Conn = "YOKOGAWA_ConnectionString";

    /// <summary>
    /// ログフォルダ保持期間(日数)
    /// </summary>
    public static string LogKeepDays = "LogKeepDays";

    /// <summary>
    /// TOUSERSINFOレコード保持期間(日数)
    /// </summary>
    public static string QueueKeepDays = "QueueKeepDays";

    /// <summary>
    /// TOUSERSINFOレコード削除対象ステータス
    /// </summary>
    public static string QueueDeleteStatus = "QueueDeleteStatus";

    /// <summary>
    /// TOUSERSINFOレコード取得件数
    /// </summary>
    public static string GetQueueCount = "GetQueueCount";

    /// <summary>
    /// スレッド待機時間(ミリ秒)
    /// </summary>
    public static string ThreadInterval = "ThreadInterval";

    /// <summary>
    /// 外字変換対象外文字(Unicode)リストファイル名
    /// </summary>
    public static string SQ_UNICODE_LIST_FILE = "SQ_UNICODE_LIST_FILE";

    // Y_Higuchi -- add --
    /// <summary>
    /// 登録対象DB
    /// </summary>
    public static string TARGET_DB = "DB";
    /// <summary>
    /// ユーザ登録条件ファイル名
    /// </summary>
    public static string AUTHUSER_Serv = "AUTHUSER_Serv";
    public static string AUTHUSER_RIS = "AUTHUSER_RIS";
    public static string AUTHUSER_TheraRIS = "AUTHUSER_TheraRIS";
    public static string AUTHUSER_Report = "AUTHUSER_Report";
    // Y_Higuchi -- add --

    #endregion

    #region SERV設定

    /// <summary>
    /// YOKOGAWAパスワード変換
    /// </summary>
    public static string YOKOGAWA_CONVERT_MD5 = "YOKOGAWA_CONVERT_MD5";

    /// <summary>
    /// YOKOGAWA外字変換
    /// </summary>
    public static string YOKOGAWA_CONVERT_GAIJI = "YOKOGAWA_CONVERT_GAIJI";

    /// <summary>
    /// YOKOGAWA外字変換後置換文字列
    /// </summary>
    public static string YOKOGAWA_GAIJI_REPLACE = "YOKOGAWA_GAIJI_REPLACE";

    /// <summary>
    /// YOKOGAWAユーザアプリケーション管理アプリケーション使用許可フラグ設定値
    /// </summary>
    public static string YOKOGAWA_USERAPPMANAGE_LICENCETOUSE = "YOKOGAWA_USERAPPMANAGE_LICENCETOUSE";

    /// <summary>
    /// ATTRMANAGE.TEXTVALUEデフォルト値
    /// </summary>
    public static string YOKOGAWA_ATTRMANAGE_TEXTVALUE_DEFAULT = "YOKOGAWA_ATTRMANAGE_TEXTVALUE_DEFAULT";

    /// <summary>
    /// USERMANAGECOMPパスワード変換
    /// </summary>
    public static string YOKOGAWA_USERMANAGECOMP_CONVERT_MD5 = "YOKOGAWA_USERMANAGECOMP_CONVERT_MD5";

    /// <summary>
    /// USERMANAGECOMP.VIEWRACCESSCTRLFLAG
    /// </summary>
    public static string YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG = "YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG";

    /// <summary>
    /// USERMANAGECOMP.VIEWCACCESSCTRLFLAG
    /// </summary>
    public static string YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG = "YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG";

    #endregion

    #region 設定値

    /// <summary>
    /// パスワード変換 0：変換なし
    /// </summary>
    public static string CONVERT_MD5_0 = "0";

    /// <summary>
    /// パスワード変換 1：MD5変換
    /// </summary>
    public static string CONVERT_MD5_1 = "1";

    /// <summary>
    /// パスワード変換 2：TOUSERSINFO.USERID適用
    /// </summary>
    public static string CONVERT_MD5_2 = "2";

    /// <summary>
    /// 外字変換 0：変換なし
    /// </summary>
    public static string CONVERT_GAIJI_0 = "0";

    /// <summary>
    /// 外字変換 1：変換あり
    /// </summary>
    public static string CONVERT_GAIJI_1 = "1";

    #endregion

  }
}
