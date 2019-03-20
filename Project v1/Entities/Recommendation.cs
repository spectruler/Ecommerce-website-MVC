using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    public class Recommendation
    {
        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public int ClientId { get; set; }

        public int AccountId { get; set; }


    }
}