/************************************************************
 * 这是 Microsoft.Owin.dll 框架的启动类，必须的。
 * **********************************************************/



#region <USINGs>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

#endregion



namespace MsOin.Demo2
{
   

    /// <summary>
    /// 启始类
    /// </summary>
    class Startup
    {

        /// <summary>
        /// 启动类的配置方法，格式是 Microsoft.Owin 所要求的
        /// <para>必须是public，而且方法名和参数类型、参数数量都固定</para>
        /// </summary>
        /// <param name="builder">App生成器</param>
        public void Configuration(IAppBuilder builder)
        {

            // 添加自己开发的“中间件”。
            // UseMyApp是IAppBuilder的一个扩展方法，具体实现，在MyMiddleware.cs文件中
            ///////////////////////////////////////////////////////////////////////////
            builder.UseMyApp();   // OR   builder.Use<MyMiddleware>();

            // builder.UseXXXX ...
            // builder.UseYYYY ...
            // builder.UseZZZZ ...


            // 放在处理链中最后执行的方法（相当于前一个中间件的next对象的Invoke方法）
            // 如果前边的中件间能处理所有的请求，那么这部分的代码是没有用的。
            ////////////////////////////////////////////////////////////////////////////
            builder.Run((c) => {
                c.Response.Write(System.Text.Encoding.ASCII.GetBytes(string.Format("Can't found, Path:{0}",c.Request.Path)));
                return Task.FromResult<int>(0);
            });

        }


 







    }


}
