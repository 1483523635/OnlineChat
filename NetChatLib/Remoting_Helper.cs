﻿using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;

namespace NetChatLib
{
    /// <summary>
    /// 用于Remoting的连接与释放
    /// </summary>
    public class Remoting_Helper
    {
        /// <summary>
        /// 服务端激活Remoting
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public bool Serivce<T>(int port)
        {
            //这里将通过反射获取T的类型的字符串  
            T type = (T)Activator.CreateInstance(typeof(T));
            //这里需要注意子类即使重写了GetType 也不行 
            //系统调用的仍然是Object的GetTtpe
            string str = type.GetType().ToString();
            str = str.Split('.')[str.Split('.').Length - 1];
            try
            {
                #region TCP协议实现
               
        //        TcpServerChannel chnl = new TcpServerChannel("talkChannel", port);
                
                #endregion
                #region HTTP协议实现
               HttpServerChannel chnl = new HttpServerChannel(port);
                #endregion
                ChannelServices.RegisterChannel(chnl, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(T), str, WellKnownObjectMode.SingleCall);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 客户端激活Remoting
        /// 采用的是Http协议进行传输
        /// Tcp协议传输不同电脑之间会登陆失败
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="url">类型所在宿主位置</param>
        /// <returns>该类的实例</returns>
        public T Client<T>(string url)
        {
            try
            {
                HttpClientChannel chnl = new HttpClientChannel();
                ChannelServices.RegisterChannel(chnl, true);
                T cls = (T)Activator.GetObject(typeof(T), url);
                return cls;

            }
            catch
            {
                return default(T);
            }
        }
    }
}
