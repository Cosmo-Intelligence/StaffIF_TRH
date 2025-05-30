﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Serv_UsersIFLinkage.Data.Export.Entity;
using Serv_UsersIFLinkage.Data.Import;
using Serv_UsersIFLinkage.Data.Import.Entity;
using Serv_UsersIFLinkage.Util;

namespace Serv_UsersIFLinkage.Ctrl
{
  class SERV_YOKOGAWA_LinkageController
  {
    #region private

    /// <summary>
    /// ログ出力
    /// </summary>
    private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// DBクラス
    /// </summary>
    private OracleDataBase db = null;

    #endregion

    #region コンストラクタ

    public SERV_YOKOGAWA_LinkageController(OracleDataBase odbc)
    {
      db = odbc;
    }

    #endregion

    #region ファンクション、メソッド

    /// <summary>
    /// 連携実行
    /// </summary>
    /// <param name="tousersRow"></param>
    /// <returns>正常ならtrue、異常ならfalse</returns>
    public bool Execute(DataRow tousersRow)
    {
      string process = string.Empty;

      if (tousersRow[ToUsersInfoEntity.F_MESSAGEID1].ToString() == Util.CommonParameter.NODE_NAME_EC04)
      {
        // ① ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        process = SERV_YOKOGAWA_UserManageEntity.EntityName;

        SERV_YOKOGAWA_UserManageEntity manage = new SERV_YOKOGAWA_UserManageEntity();

        _log.InfoFormat("{0}マッピング処理を実行します。", process);

        // ユーザ管理マッピング処理
        if (!SERV_YOKOGAWA_UserManage.Mapping(tousersRow, ref manage, db))
        {
          _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
        }

        _log.InfoFormat("{0}更新処理を実行します。", process);

        // ユーザ管理更新処理
        if (!SERV_YOKOGAWA_UserManage.Merge(manage, tousersRow, db))
        {
          _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
        }
      }

      if (tousersRow[ToUsersInfoEntity.F_MESSAGEID1].ToString() == Util.CommonParameter.NODE_NAME_EC02)
      {
        // ② ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        process = SERV_YOKOGAWA_UserAppManageEntity.EntityName;

        List<SERV_YOKOGAWA_UserAppManageEntity> appmanageList = new List<SERV_YOKOGAWA_UserAppManageEntity>();

        _log.InfoFormat("{0}マッピング処理を実行します。", process);

        // ユーザアプリケーション管理マッピング処理
        if (!SERV_YOKOGAWA_UserAppManage.Mapping(tousersRow, ref appmanageList, db))
        {
          _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
        }

        _log.InfoFormat("{0}更新処理を実行します。", process);

        // ユーザアプリケーション管理更新処理
        if (!SERV_YOKOGAWA_UserAppManage.Merge(appmanageList, tousersRow, db))
        {
          _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
        }

        // ③ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        process = SERV_YOKOGAWA_AttrManageEntity.EntityName;

        List<SERV_YOKOGAWA_AttrManageEntity> attrmanageList = new List<SERV_YOKOGAWA_AttrManageEntity>();

        _log.InfoFormat("{0}マッピング処理を実行します。", process);

        // 属性管理マッピング処理
        if (!SERV_YOKOGAWA_AttrManage.Mapping(tousersRow, ref attrmanageList, db))
        {
          _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
        }

        _log.InfoFormat("{0}更新処理を実行します。", process);

        // 属性管理更新処理
        if (!SERV_YOKOGAWA_AttrManage.Merge(attrmanageList, tousersRow, db))
        {
          _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
        }
      }

      if (tousersRow[Data.Export.Entity.ToUsersInfoEntity.F_MESSAGEID1].ToString() == Util.CommonParameter.NODE_NAME_EC01)
      {
        // ④ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        process = SERV_YOKOGAWA_UserManageCompEntity.EntityName;

        SERV_YOKOGAWA_UserManageCompEntity manageComp = new SERV_YOKOGAWA_UserManageCompEntity();

        _log.InfoFormat("{0}マッピング処理を実行します。", process);

        // ユーザ管理マッピング処理
        if (!SERV_YOKOGAWA_UserManageComp.Mapping(tousersRow, ref manageComp, db))
        {
          _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
        }

        _log.InfoFormat("{0}更新処理を実行します。", process);

        // ユーザ管理更新処理
        if (!SERV_YOKOGAWA_UserManageComp.Merge(manageComp, tousersRow, db))
        {
          _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
          throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
        }
      }

      return true;
    }

    #endregion
  }
}
