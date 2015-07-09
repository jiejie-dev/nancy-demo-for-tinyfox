/************************************************************
 * 这是 Microsoft.Owin.dll 框架的启动类，必须的。
 * **********************************************************/



#region <USINGs>

using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

#endregion



namespace MsOin.Demo
{

    /// <summary>
    /// 你的应用程序的“启动类”
    /// </summary>
    class Startup
    {

        /// <summary>
        /// 启动类的配置方法，其格式是 Microsoft.Owin.dll 约定的
        /// <para>必须是public，而且方法名和参数类型、参数数量都固定</para>
        /// </summary>
        /// <param name="builder">App生成器</param>
        public void Configuration(IAppBuilder builder)
        {
            // 当一个请求到来时，运用自己的处理方法去处理
            //////////////////////////////////////////////
            builder.Run(MyHandler);
            //RUN方法之后，请不要再添加其它处理方法或“中间件”

        }


        /// <summary>
        /// 具体处理用户请求方法
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        Task MyHandler(IOwinContext c)
        {

            //在ContentType属性中。指明charset，防止中文乱码
            c.Response.ContentType = "text/html; charset=utf-8";

            var htmlText = @"
<html>
<head>
    <title>OWIN....</title>
</head>
<body>
    <h2 style=""color:red"">您好，Jexus是全球首款直接支持MS OWIN标准的WEB服务器！</h2>
</body>
</html>
";

            // 发送数据 
            c.Response.Write(htmlText);


            // 创建并返回一个表示完成的服务
            var x = new TaskCompletionSource<bool>();
            x.SetResult(true); //调用SetResult后，这个服务即转为完成状态
            return x.Task;
        }



    }
}
