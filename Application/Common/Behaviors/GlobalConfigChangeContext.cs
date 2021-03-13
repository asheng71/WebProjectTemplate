using Application.Common.Interfaces;
using Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.GlobalConfig
{
    public class GlobalConfigChangeContext
    {
        private IApplicationDbContext _dbContext;

        public ChangeIntent Intent { get; private set; }
        public ChangedResult Result { get; private set; }

        public GlobalConfigChangeContext(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// 設定組態異動描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="refId"></param>
        public void SetChangeIntent(ConfigCategory type, string description, string refId)
        {
            var intent = new ChangeIntent
            {
                ConfigCategory = type,
                ChangeDescription = description,
                ReferenceId = refId
            };

            this.Intent = intent;
        }

        /// <summary>
        /// 設定組態異動成功執行的結果, 後續產生異動紀錄。若組態異動失敗，不需呼叫此方法。
        /// if create, then pass empty string/null to oldValue
        /// </summary>
        /// <param name="oldValue">序列化之舊資料。若為新增資料, 傳空值或空字串。</param>
        /// <param name="newValue">序列化之新資料</param>
        /// <param name="userId"></param>
        /// <param name="oid"></param>
        /// <param name="ssid"></param>
        /// <param name="state">特殊狀態(方便查詢)</param>
        public void SetChangedResult(string oldValue, string newValue, string userId, string userName, string oid, string ssid = null, TRoadStateDefinitions state = TRoadStateDefinitions.Undefined, string sercurityServerName = null)
        {
            var result = new ChangedResult();
            result.OldValue = oldValue;
            result.NewValue = newValue;
            result.UserId = userId;
            result.UserName = userName;
            result.OrgUnitOid = oid;
            result.SecurityServerId = ssid;
            result.SecurityServerName = sercurityServerName;
            result.IsNewValue = string.IsNullOrWhiteSpace(oldValue);
            result.State = state;

            this.Result = result;
        }
    }

    public class ChangeIntent
    {
        /// <summary>
        /// 異動類型
        /// </summary>
        public ConfigCategory ConfigCategory { get; set; }

        /// <summary>
        /// 異動項目說明
        /// </summary>
        public string ChangeDescription { get; set; }

        /// <summary>
        /// 異動項目 PK
        /// </summary>
        public string ReferenceId { get; set; }
    }

    public class ChangedResult
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrgUnitOid { get; set; }
        public string SecurityServerId { get; set; }

        /// <summary>
        /// GlobalConfigChangeBehavior uses securityServerId to find SecurityServerName. SS Name is required when calling OM.
        /// If you cannot be sure that SecurityServerId exists in the table SecurityServer, provide SS Name here.
        /// </summary>
        public string SecurityServerName { get; set; }

        public bool IsNewValue { get; set; }

        public TRoadStateDefinitions State { get; set; }

    }
}
