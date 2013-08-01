using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GoldUSD.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public DateTime ExpireDate { get; set; }

        //Navigation properties
        [ForeignKey("Id")]
        public virtual AspnetUser AspnetUser { get; set; }
    }
}
