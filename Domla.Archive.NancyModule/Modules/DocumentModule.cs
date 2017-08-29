using Access;
using Domla.Archive.Nancy.Requests;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy.Modules
{
    public class DocumentModule : NancyModule
    {

        readonly Access.Archive Handle;

        public DocumentModule(Access.Archive handle)
        {
            Handle = handle;

            Post["/document"] = parameters =>
            {
                var request = this.Bind<FileUploadRequest>();
                var header = new DocumentData("", "", request.File.Name, request.Title, "", "", "", DateTime.Now.Ticks, (int)request.File.Value.Length, "", "", "", (request.Directory + "/001"), false, 0);
                using (var Buffer = new MemoryStream())
                {
                    request.File.Value.CopyTo(Buffer);
                    Buffer.Capacity = (int)Buffer.Length;
                    Buffer.Position = 0;

                    Handle.PutInputData(header, Buffer.GetBuffer());
                    return Negotiate.WithStatusCode(HttpStatusCode.NoContent);
                }
            };

        }
    }
}
