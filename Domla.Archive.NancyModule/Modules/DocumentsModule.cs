using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy.Modules
{
    public class DocumentsModule : NancyModule
    {

        readonly Access.Archive Handle;

        public DocumentsModule(Access.Archive handle)
        {
            Handle = handle;


            Get["/documents"] = parameters =>
            {
                var documents = Handle.ListDocuments("/");
                var result = "[" + string.Join(",", documents.Select(doc => "{\"Name\" : \"" + doc.Name + "\"}")) + "]";
                var data = Encoding.UTF8.GetBytes(result);
                return new Response
                {
                    ContentType = "application/json",
                    Contents = x => x.Write(data, 0, data.Length)
                };
            };

        }
    }
}
