using Owin;

namespace Nancy.Demo
{

    /// <summary>
    /// 支持NancyFx的OWIN启动类
    /// <para>MS OWIN标准的宿主都需要一个启动类</para>
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            //将 Nancy(中间件)添加到Microsoft.Owin处理环节中
            ////////////////////////////////////////////////////
            builder.UseNancy();
        }
    }




    /// <summary>
    /// 针对NancyFx的Module定义，这是NancyFx的工作对象
    /// </summary>
    public class MyNancyModule : NancyModule
    {
        public MyNancyModule()
        {
            Get["/"] = _ => "<h2>Hello. This is a NancyFx on TinyFox Server.</h2>";
            // ........

        }
    }




}
