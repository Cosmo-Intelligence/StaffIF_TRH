
namespace Serv_UsersIFLinkage.Data.Import.Common
{
  /// <summary>
  /// SERVクエリクラス
  /// </summary>
  class SERV_QUERY
  {
    #region YOKOGAWA ユーザ管理

    /// <summary>
    /// ユーザ管理 Merge
    /// </summary>
    public const string YOKOGAWA_USERMANAGE_MERGE =
                  " merge into USERMANAGE"
                + " using("
                + "   select {0} as USERID, {1} as HOSPITALID from dual"
                + " ) dummy"
                + " on ("
                + "     USERMANAGE.USERID = dummy.USERID"
                + "   and"
                + "     USERMANAGE.HOSPITALID = dummy.HOSPITALID"
                + " )"
                + " {12}"
                + " when not matched then"
                + "   insert"
                + "     (USERID,"
                + "      HOSPITALID,"
                + "      PASSWORD,"
                + "      USERNAME,"
                + "      USERNAMEENG,"
                + "      PASSWORDEXPIRYDATE,"
                + "      PASSWORDWARNINGDATE,"
                + "      USERIDVALIDITYFLAG,"
                + "      BELONGINGDEPARTMENT,"
                + "      MAINGROUPID,"
                + "      SUBGROUPIDLIST,"
                + "      UPDATEDATETIME)"
                + "   values"
                + "     ({0},"
                + "      {1},"
                + "      {2},"
                + "      {3},"
                + "      {4},"
                + "      {5},"
                + "      {6},"
                + "      {7},"
                + "      {8},"
                + "      {9},"
                + "      {10},"
                + "      {11})";

    /// <summary>
    /// ユーザ管理 削除
    /// </summary>
    public const string YOKOGAWA_USERMANAGE_DELETE =
                  " update USERMANAGE"
                + " set"
                + "   USERIDVALIDITYFLAG = {2},"
                + "   UPDATEDATETIME = {3}"
                + " where"
                + "   USERID = {0}"
                + " and"
                + "   HOSPITALID = {1}";

    /// <summary>
    /// ユーザ管理 Merge
    /// </summary>
    public const string YOKOGAWA_USERMANAGECOMP_MERGE =
                      " merge into USERMANAGECOMP"
                    + " using("
                    + "   select {0} as USERID, {1} as HOSPITALID from dual"
                    + " ) dummy"
                    + " on ("
                    + "     USERMANAGECOMP.USERID = dummy.USERID"
                    + "   and"
                    + "     USERMANAGECOMP.HOSPITALID = dummy.HOSPITALID"
                    + " )"
                    + " {7}"
                    + " when not matched then"
                    + "   insert"
                    + "     (USERID,"
                    + "      HOSPITALID,"
                    + "      PASSWORD,"
                    + "      COMMISSION,"
                    + "      COMMISSION2,"
                    + "      VIEWRACCESSCTRLFLAG,"
                    + "      VIEWCACCESSCTRLFLAG)"
                    + "   values"
                    + "     ({0},"
                    + "      {1},"
                    + "      {2},"
                    + "      {3},"
                    + "      {4},"
                    + "      {5},"
                    + "      {6})";

    #endregion

    #region YOKOGAWA ユーザアプリケーション管理

    /// <summary>
    /// ユーザアプリケーション管理 Update
    /// </summary>
    public const string YOKOGAWA_USERAPPMANAGE_UPDATE =
                  " update USERAPPMANAGE"
                + " set"
                + "   LICENCETOUSE = {2},"
                + "   UPDATEDATETIME = sysdate"
                + " where"
                + "   USERID = {0}"
                + " and"
                + "   HOSPITALID = {1}";

    /// <summary>
    /// ユーザアプリケーション管理 Merge
    /// </summary>
    public const string YOKOGAWA_USERAPPMANAGE_MERGE =
                  " merge into USERAPPMANAGE"
                + " using("
                + "   select {0} as USERID, {1} as HOSPITALID, {2} as APPCODE from dual"
                + " ) dummy"
                + " on ("
                + "     USERAPPMANAGE.USERID = dummy.USERID"
                + "   and"
                + "     USERAPPMANAGE.HOSPITALID = dummy.HOSPITALID"
                + "   and"
                + "     USERAPPMANAGE.APPCODE = dummy.APPCODE"
                + " )"
                + " when not matched then"
                + "   insert"
                + "     (USERID,"
                + "      HOSPITALID,"
                + "      APPCODE,"
                + "      LICENCETOUSE,"
                + "      MYATTRID,"
                + "      UPDATEDATETIME)"
                + "   values"
                + "     ({0},"
                + "      {1},"
                + "      {2},"
                + "      {3},"
                + "      {4},"
                + "      {5})";

    #endregion

    #region YOKOGAWA 属性管理

    /// <summary>
    /// 属性管理 Merge
    /// </summary>
    public const string YOKOGAWA_ATTRMANAGE_MERGE =
                  " merge into ATTRMANAGE"
                + " using("
                + "   select {1} as ATTROWNERID, {2} as ATTRNAME from dual"
                + " ) dummy"
                + " on ("
                + "     ATTRMANAGE.ATTROWNERID = dummy.ATTROWNERID"
                + "   and"
                + "     ATTRMANAGE.ATTRNAME = dummy.ATTRNAME"
                + " )"
                + " when not matched then"
                + "   insert"
                + "     (ATTRID,"
                + "      ATTROWNERID,"
                + "      ATTRNAME,"
                + "      VALUETYPE,"
                + "      TEXTVALUE,"
                + "      BLOBVALUE,"
                + "      UPDATEDATETIME)"
                + "   values"
                + "     (GetNewAttrID(),"
                + "      {1},"
                + "      {2},"
                + "      {3},"
                + "      {4},"
                + "      {5},"
                + "      {6})";

    /// <summary>
    /// 属性管理デフォルトテキスト型属性値取得
    /// </summary>
    public const string YOKOGAWA_ATTRMANAGE_SELECT_TEXTVALUE =
                  " (select"
                + "   nvl(max(TEXTVALUE), {2})"
                + " from"
                + "   ATTRMANAGE"
                + " where"
                + "   ATTROWNERID = {0}"
                + " and"
                + "   ATTRNAME = {1})";

    #endregion

  }

}
