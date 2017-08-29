using Domla.Archive.Nancy.Infrastructure;
using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy.Modules
{
    public class TreeModule : NancyModule
    {
        readonly Access.Archive Handle;

        public TreeModule(Access.Archive handle, DefaultContractResolver contractResolver, Node root)
        {
            Handle = handle;

            Get["/tree"] = parameters =>
            {
                var tree = Handle.ReadTree();
                root.Text = String.Empty;
                foreach (var entry in tree) Node.AddOrIgnore(entry.Name, root);
                contractResolver.NamingStrategy = new CamelCaseNamingStrategy();                
                var payload = JsonConvert.SerializeObject(root.Nodes, new JsonSerializerSettings { ContractResolver = contractResolver });
                return new TextResponse(payload, "application/json", Encoding.UTF8);
            };
        }
    }
}
