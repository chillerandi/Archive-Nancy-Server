using System;
using System.Linq;
using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace Domla.Archive.Nancy.Requests.Binding
{
    /// <summary>
    /// Do not pollute the Module. Use a custom Model Binder to extract the binding part.
    /// </summary>
    public class FileUploadRequestBinder : IModelBinder
    {
        /// <summary>
        /// Binds the request to the specified context, modeltype, and instancs with the specified configuration (can restrict overwrite)
        /// </summary>
        /// <param name="context">class NancyContext: (CultureInfo, UserIdentity, Request, Route, Response)</param>
        /// <param name="modelType">class System.Type: Represents type declarations: class types, interface types, 
        /// array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</param>
        /// <param name="instance">class System.Object: Supports all classes in the .NET Framework class hierarchy and provides 
        /// low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</param>
        /// <param name="configuration">class Nancy.ModelBinding.BindingConfig: Default/NoOverWrite</param>
        /// <param name="blackList">string-array</param>
        /// <returns>object fileUploadRequest</returns>
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            var fileUploadRequest = (instance as FileUploadRequest) ?? new FileUploadRequest();
            var form = context.Request.Form;            
            fileUploadRequest.Title = form["title"];
            fileUploadRequest.Description = form["description"] ;           
            fileUploadRequest.File = GetFileByKey(context, "file");
        // TODO: Alle Parameter müssen übergeben werden! Das muss validiert werden! Darf sonst nicht ans Archiv weitergegeben werden!
            return fileUploadRequest;
        }

        /// <summary>
        /// Gets the tags that are published with the document. Splits the tags by commas to seperate them.
        /// tags are used to associate the document with specific topics. 
        /// </summary>
        /// <param name="field"></param>
        /// <returns>genric list of strings</returns>
        private IList<string> GetTags(dynamic field)
        {
            try
            {
                var tags = (string)field;
                return tags.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Gets a file by a specified key-value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns>HttpFile</returns>
        private HttpFile GetFileByKey(NancyContext context, string key)
        {
            IEnumerable<HttpFile> files = context.Request.Files;
            if (files != null)
            {
                return files.FirstOrDefault(x => x.Key == key);
            }
            return null;
        }

        /// <summary>
        /// checks, if the modelType can bind to all neccesary params of Bind().        /// 
        /// </summary>
        /// <param name="modelType">System.Type: Represents type declarations</param>
        /// <returns>true if modelType is of type FileUploadRequest, else false</returns>
        public bool CanBind(Type modelType)
        {
            return modelType == typeof(FileUploadRequest);
        }
    }
}