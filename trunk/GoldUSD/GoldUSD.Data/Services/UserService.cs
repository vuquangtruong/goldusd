using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using GoldUSD.Data.Models;

namespace GoldUSD.Data.Services
{
    public class UserService : RepositoryBase<User>, IUserService
    {
        public UserService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
