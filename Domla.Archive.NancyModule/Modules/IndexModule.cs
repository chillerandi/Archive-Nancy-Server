using Nancy;

namespace Domla.Archive.Nancy
{
    public class IndexModule : NancyModule
    {
        readonly Access.Archive Handle;

        public IndexModule(Access.Archive handle)
        {
            Handle = handle;
        }
    }
}

