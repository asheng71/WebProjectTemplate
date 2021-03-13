using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumeration
{
    public enum TRoadStateDefinitions
    {
        /*
tr_tb_agreements	provider_active	Y/N
	client_active	Y: 啟用/N: 停用/D: 廢止

         */

        Undefined = 0,
        SecurityServerApplyInit = 1,
        SecurityServerApplyPass = 2,
        SecurityServerApplyReject = 3,
        SecurityServerApplyDistribute = 4,  // 發布
        
        /// <summary>
        /// 新增安控伺服器
        /// </summary>
        SecurityServerInitialize = 20,
        /// <summary>
        /// 安控伺服器尚未啟用狀態 (tr_tb_security_servers.active = "U")
        /// </summary>
        SecurityServerInactive = 21,
        /// <summary>
        /// 安控伺服器啟用狀態 (tr_tb_security_servers.active = "Y")
        /// </summary>
        SecurityServerActive = 22,
        /// <summary>
        /// 安控伺服器停用狀態 (tr_tb_security_servers.active = "N")
        /// </summary>
        SecurityServerDeactive = 23,


        /// <summary>
        /// API啟用狀態 (tr_tb_apis.active = "Y")
        /// </summary>
        ApiActive = 30,
        /// <summary>
        /// API停用狀態 (tr_tb_apis.active = "N")
        /// </summary>
        ApiDeactive = 31,
        /// <summary>
        /// API暫停介接狀態 (tr_tb_apis.active = "S") 
        /// </summary>
        ApiSuspend = 32,


        // Agreement 系列
        AgreementManagement = 40
    }
}
