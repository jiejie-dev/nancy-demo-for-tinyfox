/***************************************************************
 *        Jexus Owin Host Server 基本应用示例
 * =============================================================   
 * 本DEMO的目的意义：
 *   一，演示如何开发一个含有名叫“OwinMain”方法的“适配器”类
 *   二，演示如果利用“OwinMain”方法传入的参数，获取请求数据，
 *       然后对该请求数据进行某种处理后，把结果发送给浏览器。
 *   三，了解“OWIN字典”含义后，你可以按本演示的特点，开发出具
 *       有特定功能的，与其它框架无关的符合HTTP协议的WEB应用。
 *  
 *  使用方法：将编译得到的dll放到网站的bin文件夹中。
 * *************************************************************/


#region <USING>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

#endregion


namespace Base.Demo
{


    /// <summary>
    /// 用于JWS/TinyFox加载并初始化的“应用程序启动类”
    /// <para>JWS、TinyFox称之为"适配器"，类名无具体要求</para>
    /// <para>JWS/TinyFox加载一个OWIN应用，总是从含有OwinMain方法的类型开始的</para>
    /// </summary>
    public class Adapter
    {

        /// <summary>
        /// 配配器构造函数
        /// </summary>
        public Adapter()
        {

            /******************************************
             * JWS实例化此类型时会调用这个默认构造函数
             * 所以，你可以在这儿写一些初始化代码
             * 这个类只会被 Jexus 初始化一次
             * ****************************************/

            // ......... YOU CODE ........ //

        }


        /// <summary>
        /// *** JWS或TinyFox所需要的关键函数 ***
        /// <para>你的应用程序要与JWS/TinyFox服务器对接，必须提供这一个方法</para>
        /// <para>服务器会将每个请求按OWIN规范打包成字典从这个方法传送给你</para>
        /// </summary>
        /// <param name="env">服务器按OWIN规范包装的“环境字典”，具体参见www.owin.org</param>
        /// <returns>返回一个任务。当任务的状态翻转为完成、异常、取消等状态时，这个连接才会重新接受浏览器新的请求而进入下一轮会话</returns>
        public Task OwinMain(IDictionary<string, object> env)
        {
            return ProcessRequest(env);
        }



        /// <summary>
        /// 你自己对请求的处理函数
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        private Task ProcessRequest(IDictionary<string, object> env)
        {

            // 从字典中获取请求路径，不含QueryString
            //////////////////////////////////////////////////
            var reqPath = env["owin.RequestPath"] as string;


            // 返回null，表示这个路径不属OWIN处理，让JWS或TinyFox选择其它处理方式
            //////////////////////////////////////////////////////////////////////
            if (reqPath != "/test" && reqPath != "/test/") return null;


            // 从字典中获取向客户（浏览器）发送数据的“流”对象
            /////////////////////////////////////////////////////////
            var responseStream = env["owin.ResponseBody"] as Stream;

            // 你准备发送的数据
            const string outString = "<html><head><title>Jexus Owin Web Server</title></head><body>Jexus Owin Server!<br /><h2>Jexus Owin Server，放飞您灵感的翅膀...</h2>\r\n</body></html>";
            var outBytes = Encoding.UTF8.GetBytes(outString);

            // 从参数字典中获取Response HTTP头的字典对象
            var responseHeaders = env["owin.ResponseHeaders"] as IDictionary<string, string[]>;

            // 设置必要的http响应头
            ////////////////////////////////////////////////////////////////

            // 设置 Content-Length头 （可以不设）
            responseHeaders.Add("Content-Length", new[] { outBytes.Length.ToString() });
            // responseHeaders.Add("Connection", new[] { "close" }); // 'Keep-Alive' is default
            // 设置 Content-Type头
            responseHeaders.Add("Content-Type", new[] { "text/html; charset=utf-8" });


            // 把正文写入流中，发送给浏览器
            responseStream.Write(outBytes, 0, outBytes.Length);

            // 返回一个任务。注意：
            // 一，返回的任务可以是完成或未完成任务，但Jexus总会等到这个任务状态
            //     变成“完成”“取消”“异常”等状态之一后，服务器才会接受浏览器
            //     新一轮的请求或者与浏览器（客户端）断开连接。
            // 二，可以返回 null，表示当前这个请求不属于本应用应该处理的范围，
            //     前提条件是：返回null之前，绝不能向Response流写入任何数据。
            return Task.FromResult<int>(0);

        }


    }
}
