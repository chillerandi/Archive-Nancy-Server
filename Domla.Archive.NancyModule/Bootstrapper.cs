using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace Domla.Archive.Nancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            //###########################    hier wird das Archiv verbunden    ###########################
            Access.Service.AuthenticatedLogin.AddHandler((_, x) => LoginAuthenticated(x));
            Access.Service.ConnectionFailed.AddHandler((_, x) => ConnectionFailed(x));
            Access.Service.ConnectionInfo.AddHandler((_, x) => ConnectionInfos(x));
            Access.Service.SimpleLogin.AddHandler((_, x) => LoginSimple(x));

            Handle = Access.Service.openConnection();

            if (!string.IsNullOrWhiteSpace(ConnectionError)) return;

            container.Register<Access.Archive>(Handle);
            //container.Register<FileSystemCache>(Cache);           
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            Action<NancyContext> action = (NancyContext ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                           .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT")
                           .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            };
            pipelines.AfterRequest.AddItemToEndOfPipeline(action);
        }



        void LoginAuthenticated(Access.LoginAuthenticatedArguments arguments)
        {
            arguments.User = Settings.Default.ArchiveUser;
            arguments.Password = Settings.Default.ArchivePassword;
        }

        void LoginSimple(Access.LoginSimpleArguments arguments)
        {
            arguments.User = Settings.Default.ArchiveUser;
        }

        void ConnectionInfos(Access.ConnectionArguments arguments)
        {
            arguments.Host = Settings.Default.ArchiveHost;
            arguments.Port = Settings.Default.ArchivePort;
        }

        void ConnectionFailed(Access.ConnectionFailedArguments arguments)
        {
            ConnectionError = arguments.Message;
        }

        public string ConnectionError
        {
            get;
            private set;
        }

        public Access.Archive Handle
        {
            get;
            private set;
        }

    }
}
