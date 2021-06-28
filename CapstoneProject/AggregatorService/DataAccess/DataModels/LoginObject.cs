using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorController.DataAccess
{
    public class LoginObject
    {
        public Userdata User { get; set; }

        public List<Topic> Topics { get; set; }

        public LoginObject()
        {
            Topics = new List<Topic>();
        }
    }
}
