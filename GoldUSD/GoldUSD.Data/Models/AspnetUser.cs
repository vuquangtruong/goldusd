using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GoldUSD.Data.Models
{
    [Table("aspnet_Users")]
    public class AspnetUser
    {
        public int ApplicationId { get; set; }

        [Key]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string LoweredUserName { get; set; }

        public string MobileAlias { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime LastActivityDate { get; set; }

        //Navigation properties
        public virtual List<User> Users { get; set; }
    }
}
