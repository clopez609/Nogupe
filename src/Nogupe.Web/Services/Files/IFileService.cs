using Nogupe.Web.Entities.Repository;
using File = Nogupe.Web.Entities.File;

namespace Nogupe.Web.Services.Files
{
    public interface IFileService : IRepository<File>
    {
    }
}
