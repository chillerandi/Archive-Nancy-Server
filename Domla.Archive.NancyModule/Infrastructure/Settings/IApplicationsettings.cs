using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domla.Archive.Nancy.Infrastructure.Settings
{
    public interface IApplicationSettings
    {
        string FileUploadDirectory { get; }
    }
}
