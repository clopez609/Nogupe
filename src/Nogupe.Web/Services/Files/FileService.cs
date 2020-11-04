using Microsoft.AspNetCore.Hosting;
using Nogupe.Web.Data;
using Nogupe.Web.Entities;
using Nogupe.Web.Entities.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = Nogupe.Web.Entities.File;

namespace Nogupe.Web.Services.Files
{
    public class FileService : Repository<File>, IFileService
    {
        private readonly DataContext _context;
        public FileService(DataContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
