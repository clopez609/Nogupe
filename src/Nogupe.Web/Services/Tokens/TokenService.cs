using Nogupe.Web.Data;
using Nogupe.Web.Entities.Courses;
using Nogupe.Web.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Tokens
{
    public class TokenService : Repository<Token>, ITokenService
    {
        private readonly DataContext _context;

        public TokenService(DataContext context) : base(context)
        {
        }
    }
}
