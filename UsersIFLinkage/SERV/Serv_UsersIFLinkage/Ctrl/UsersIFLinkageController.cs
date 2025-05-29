using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Serv_UsersIFLinkage.Data.Export;
using Serv_UsersIFLinkage.Data.Export.Entity;
using Serv_UsersIFLinkage.Util;

namespace Serv_UsersIFLinkage.Ctrl
{
  class UsersIFLinkageController
  {
    #region private

    /// <summary>
    /// ログ出力
    /// </summary>
    private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// USER接続文字列を取得
    /// </summary>
    private static string userConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.USER_Conn);

    /// <summary>
    /// YOKOGAWA DB接続文字列を取得
    /// </summary>
    private static string yokoConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.YOKO_Conn);

    #endregion

    #region public

    /// <summary>
    /// ユーザ連携処理
    /// </summary>
    public static bool Linkage()
    {
      // TOUSERSINFO用 DBクラス
      OracleDataBase tousdb = null;

      // ユーザ情報連携I/F
      DataTable tousersDt = new DataTable();

      try
      {
        _log.Info("初期処理を実行します。");
        // 初期処理
        if (!Init())
        {
          throw new Exception("初期処理でエラーが発生しました。");
        }

        // TOUSERSINFO用 インスタンス生成
        tousdb = new OracleDataBase(userConn);

        _log.Info("ユーザ情報連携I/F処理済データ削除処理を実行します。");
        // ユーザ情報連携I/F処理済データ削除処理
        if (!ToUsersInfo.Delete(tousdb))
        {
          throw new Exception("ユーザ情報連携I/F処理済データ削除処理でエラーが発生しました。");
        }

        // STATUS='00'のものを拾って必要数増殖する
        _log.Info("ユーザ情報I/Fデータ複製&更新処理を実行します。");
        AuthorizeUser auth = new AuthorizeUser();
        int intret = auth.Copy_UsersInfo(tousdb);
        if (intret < 0)
        {
          throw new Exception("ユーザ情報I/Fデータ複製&更新処理でエラーが発生しました。");
        }
        else if (intret == 0)
        {
          _log.Info("処理対象となるレコードがありません。");
          return true;
        }

        _log.Info("ユーザ情報連携I/Fデータ取得処理を実行します。");
        // 2022.10.04 Add Cosmo＠Nishihara Start データ複製削除対応
        //1度NG更新処理を行った親レコードのRequestIDを格納するlist
        List<string> lstNGRequestID = new List<string>();
        // 2022.10.04 Add Cosmo＠Nishihara End データ複製削除対応
        while (true)
        {
          // ユーザ情報連携I/Fデータ取得処理
          if (!ToUsersInfo.Export(ref tousersDt, tousdb))
          {
            throw new Exception("ユーザ情報連携I/Fデータ取得でエラーが発生しました。");
          }

          // ユーザ情報連携I/Fデータが0件になったら終了
          if (tousersDt.Rows.Count == 0)
          {
            break;
          }

          // 取得した件数分処理を行う
          foreach (DataRow tousersRow in tousersDt.Rows)
          {
            // デフォルト設定
            tousersRow[ToUsersInfoEntity.F_TRANSFERSTATUS] = ToUsersInfoEntity.TRANSFERSTATUS_01;
            tousersRow[ToUsersInfoEntity.F_TRANSFERRESULT] = ToUsersInfoEntity.TRANSFERRESULT_OK;
            tousersRow[ToUsersInfoEntity.F_TRANSFERTEXT] = string.Empty;

            // 2022.10.04 Add Cosmo＠Nishihara Start データ複製削除対応
            //親レコードをNG更新するかの判定
            bool originalRecodeResult = true;
            // 2022.10.04 Add Cosmo＠Nishihara End データ複製削除対応
            try
            {
              // 連携処理実行
              Execute(tousersRow);
            }
            catch (Exception ex)
            {
              // 2022.10.04 Add Cosmo＠Nishihara Start データ複製削除対応
              _log.ErrorFormat("ユーザ情報連携I/Fデータ連携処理でエラーが発生しました。エラー内容：{0}", ex.Message);

              originalRecodeResult = false;
              // 2022.10.04 Add Cosmo＠Nishihara End データ複製削除対応
              // エラー発生
              tousersRow[ToUsersInfoEntity.F_TRANSFERSTATUS] = ToUsersInfoEntity.TRANSFERSTATUS_02;
              tousersRow[ToUsersInfoEntity.F_TRANSFERRESULT] = ToUsersInfoEntity.TRANSFERRESULT_NG;
              tousersRow[ToUsersInfoEntity.F_TRANSFERTEXT] = ex.Message;
            }
            finally
            {
              _log.Info("ユーザ情報連携I/Fデータ処理結果更新処理を実行します。");
              // ユーザ情報連携I/Fテーブル更新
              if (!ToUsersInfo.UpdateResult(tousersRow, tousdb))
              {
                _log.ErrorFormat("ユーザ情報連携I/Fデータ処理結果更新処理でエラーが発生しました。【送信要求番号】{0}", tousersRow[ToUsersInfoEntity.F_REQUESTID]);
              }

              // 2022.10.04 Add Cosmo＠Nishihara Start データ複製削除対応
              // ユーザ情報連携I/Fデータ処理取込元結果NG更新
              //連携処理失敗時、かつ、失敗した子レコードの親のレコードがNG更新されていない場合
              if (!originalRecodeResult && !lstNGRequestID.Contains(tousersRow[ToUsersInfoEntity.F_MESSAGEID2].ToString()))
              {
                  _log.Info("ユーザ情報連携I/Fデータ処理取込元結果NG更新処理を実行します。");
                  if (!ToUsersInfo.OriginalRecodeUpdateResult_NG(tousdb, ToUsersInfoEntity.TRANSFERSTATUS_02, ToUsersInfoEntity.TRANSFERRESULT_NG, tousersRow[ToUsersInfoEntity.F_MESSAGEID2].ToString()))
                  {
                      throw new Exception("ユーザ情報連携I/Fデータ処理取込元結果NG更新処理でエラーが発生しました。");
                  }

                  //NG更新した親レコードのREQUESTID(子レコードのMESSAGEID2に格納した値)をリストに追加
                  lstNGRequestID.Add(tousersRow[ToUsersInfoEntity.F_MESSAGEID2].ToString());

                 _log.InfoFormat("ユーザ情報連携I/Fデータ処理取込元結果NG更新処理が終了しました。【送信要求番号】{0}、【送信ステータス】{1}、【送信結果】{2}", tousersRow[ToUsersInfoEntity.F_MESSAGEID2], ToUsersInfoEntity.TRANSFERSTATUS_02, ToUsersInfoEntity.TRANSFERRESULT_NG);
              }
              // 2022.10.04 Add Cosmo＠Nishihara End データ複製削除対応
            }

            // 終了指示があるか判定
            if (ProcessMain.isStop)
            {
              // 終了を中断
              return true;
            }
          }

          // 初期化
          tousersDt.Clear();
        }
        // 2022.10.04 Add Cosmo＠Nishihara Start データ複製削除対応
        lstNGRequestID.Clear();

        _log.Info("ユーザ情報連携I/F処理済データ削除処理を実行します。");
        // ユーザ情報連携I/F処理済データ削除処理
        if (!ToUsersInfo.CopyDataDelete(tousdb))
        {
            throw new Exception("ユーザ情報連携I/F処理済データ削除処理でエラーが発生しました。");
        }

        // ユーザ情報連携I/Fデータ処理取込元結果更新
        _log.Info("ユーザ情報連携I/Fデータ処理取込元結果OK更新処理を実行します。");

        if (!ToUsersInfo.OriginalRecodeUpdateResult_OK(tousdb, ToUsersInfoEntity.TRANSFERSTATUS_01, ToUsersInfoEntity.TRANSFERRESULT_OK))
        {
            throw new Exception("ユーザ情報連携I/Fデータ処理取込元結果OK更新処理でエラーが発生しました。");
        }

        _log.InfoFormat("ユーザ情報連携I/Fデータ処理取込元結果OK更新処理の実行が完了しました。");
        // 2022.10.04 Add Cosmo＠Nishihara End データ複製削除対応
      }
      finally
      {
        // 破棄
        tousersDt.Clear();
        tousersDt = null;
        tousdb = null;
        GC.Collect();
      }

      return true;
    }

    #endregion

    #region メソッド、ファンクション

    /// <summary>
    /// 連携実行
    /// </summary>
    /// <returns>正常ならtrue、異常ならfalse</returns>
    private static bool Execute(DataRow tousersRow)
    {
      // YOKOGAWA DBクラス
      OracleDataBase yokodb = null;

      try
      {
        _log.InfoFormat("連携処理を開始します。【送信要求番号】{0}", tousersRow[ToUsersInfoEntity.F_REQUESTID]);

        // YOKOGAWADBインスタンス生成
        yokodb = new OracleDataBase(yokoConn);

        string dbname = string.Empty;

        dbname = "【SERV】YOKOGAWA";
        _log.InfoFormat("{0}連携処理を実行します。", dbname);

        // SERV YOKOGAWA接続
        yokodb.Open();

        // SERV YOKOGAWA連携処理
        SERV_YOKOGAWA_LinkageController yokoLink = new SERV_YOKOGAWA_LinkageController(yokodb);

        // SERV YOKOGAWA連携処理実行
        if (!yokoLink.Execute(tousersRow))
        {
          throw new Exception(string.Format("{0}連携処理でエラーが発生しました。", dbname));
        }

        yokodb.Commit();
      }
      catch (Exception ex)
      {
        yokodb.RollBack();
        throw ex;
      }
      finally
      {
        yokodb.Close();
        yokodb = null;
        _log.Info("連携処理を終了します。");
      }

      return true;
    }

    /// <summary>
    /// 初期処理
    /// </summary>
    /// <returns>正常ならtrue、異常ならfalse</returns>
    private static bool Init()
    {
      // ログフォルダ削除
      Logger.Delete();

      return true;
    }

    #endregion
  }
}
