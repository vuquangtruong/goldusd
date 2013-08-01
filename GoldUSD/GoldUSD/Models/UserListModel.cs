using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldUSD.Models
{
    public class UserListModel
    {
        public Guid? DeleteUserId { get; set; }

        public string SeachUsername { get; set; }
    }
}