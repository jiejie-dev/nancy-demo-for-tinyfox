
/***********************************************************
 * 作用：演示开发一个 MS OWIN Middleware 中间件的基本方法
 * **********************************************************/


#region <USINGs>

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

#endregion



namespace MsOin.Demo2
{

    /// <summary>
    /// 我的OWIN "中间件"
    /// </summary>
    public class MyMiddleware : OwinMiddleware
    {

        /// <summary>
        /// 下一个“中间件”对象
        /// </summary>
        OwinMiddleware _next;

        /// <summary>
        /// 构造函数，第一个参数必须为 OwinMiddleware对象
        /// </summary>
        /// <param name="next">下一个中间件</param>
        public MyMiddleware(OwinMiddleware next)
            : base(next)
        {
            _next = next;
            //第一个参数是固定的，后边还可以添加自定义的其它参数
        }


        /// <summary>
        /// 处理用户请求的具体方法，该方法是必须的
        /// </summary>
        /// <param name="c">OwinContext对象</param>
        /// <returns></returns>
        public override Task Invoke(IOwinContext c)
        {

            if (_next != null && c.Request.Path.Value != "/test") { return _next.Invoke(c); }

            // var urlPath = c.Request.Path;
            // switch (urlPath) { 
            //    ..........
            //    ..........
            //    可以根据不同的URL PATH调用不同的处理方法
            //    从而构成一个完整的应用。
            // }

            const string outString = "<html><head><title>Jexus Owin Web Server</title></head><body>Jexus Owin Server!<br /><h2>Jexus Owin Server，放飞您灵感的翅膀...</h2>\r\n</body></html>";
            var outBytes = Encoding.UTF8.GetBytes(outString);

            c.Response.ContentType = "text/html; charset=utf-8";
            c.Response.Write(outBytes, 0, outBytes.Length);
            
            return Task.FromResult<int>(0);
        }

        

    } //end call mymiddleware




    /// <summary>
    /// 这个类是为AppBuilder添加一个名叫UseMyApp的扩展方法，目的是方便用户调用
    /// </summary>
    public static class MyExtension
    {
        public static IAppBuilder UseMyApp(this IAppBuilder builder)
        {
            return builder.Use<MyMiddleware>(); 
            //USE可以带多个参数，对应中间件构造函数中的第2、3、....参数;
        }

    }


}
