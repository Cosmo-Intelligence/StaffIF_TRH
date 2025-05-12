﻿using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using Ris_UsersIFLinkage.Data.Export.Entity;
using Ris_UsersIFLinkage.Data.Import.Common;
using Ris_UsersIFLinkage.Data.Import.Entity;
using Ris_UsersIFLinkage.Util;

namespace Ris_UsersIFLinkage.Data.Import
{
    class RIS_RRIS_UserInfo_CA
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 設定ファイル：USERINFO_CA.ATTRIBUTE:ｸﾞﾙｰﾌﾟID（=GROUPMASTER.ID）デフォルト値
        /// </summary>
        private static string attribute =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_USERINFO_CA_ATTRIBUTE_DEFAULT);

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="tousersRow"></param>
        /// <param name="userinfoca"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref RIS_RRIS_UserInfo_CAEntity userinfoca, OracleDataBase db)
        {
            try
            {
                userinfoca.Id = "1";
                userinfoca.Loginid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                userinfoca.Staffid = tousersRow[ToUsersInfoEntity.F_STAFFID].ToString();
                if (string.IsNullOrEmpty(userinfoca.Staffid))
                {
                    userinfoca.Staffid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                }
                userinfoca.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                userinfoca.Syokuin_kbn = tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString();
                userinfoca.Attribute = CommonUtil.ConvertStrToInt(
                                            ConfigurationManager.AppSettings[
                                                AppConfigParameter.RRIS_USERINFO_CA_ATTRIBUTE
                                                + tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString()]);
                if (userinfoca.Attribute == null)
                {
                    // 取得できなかった場合はDEFAULT設定
                    userinfoca.Attribute = CommonUtil.ConvertStrToInt(attribute);
                }
                userinfoca.Showorder = "1";
                
                // データをログに出力
                //_log.Debug(userinfoca.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="userinfoca"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                RIS_RRIS_UserInfo_CAEntity userinfoca, DataRow tousersRow, OracleDataBase db)
        {
            string query = string.Empty;

            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() == 
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    // 登録
                    db.ExecuteQuery(
                        string.Format(
                            RIS_QUERY.RRIS_USERINFO_CA_MERGE,
                            userinfoca.Id,
                            OracleDataBase.SingleQuotes(userinfoca.Loginid),
                            OracleDataBase.SingleQuotes(userinfoca.Staffid),
                            OracleDataBase.SingleQuotes(userinfoca.Hospitalid),
                            OracleDataBase.SingleQuotes(userinfoca.Syokuin_kbn),
                            userinfoca.Attribute,
                            userinfoca.Showorder
                            )
                        );
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        #endregion
    }
}
