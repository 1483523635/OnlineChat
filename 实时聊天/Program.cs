﻿using NetChatLib;
using System;
using System.Windows.Forms;

namespace 实时聊天
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenRemotingService(12346);
            Application.Run(new fm_server());
        }

        #region 开启remoting服务     
        /// <summary>
        /// 用于开启Remoting服务
        /// </summary>
        /// <param name="port">端口号</param>
        static  void OpenRemotingService(int port)
        {
            Remoting_Helper remoting = new Remoting_Helper();
            bool flag;
            do
            {
                flag = remoting.Serivce<FileHelper>(port);
            } while (!flag);

        }
        #endregion
    }
}
