using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        public string FullName { get; set; }
        public int UserCode { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string OfficialMailID { get; set; }
        public int Role { get; set; }
    }
}
