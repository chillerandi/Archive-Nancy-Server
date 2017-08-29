using Nancy;
using System.Collections.Generic;

namespace Domla.Archive.Nancy.Requests
{
    public class FileUploadRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Directory { get; set; }

        public IList<string> Tags { get; set; }

        public HttpFile File { get; set; }
    }
}