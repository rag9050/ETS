using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Notification
    {
      public int SNo {get; set;}
      public int PostedBy {get; set;}
      public string Title {get;set;}
      public string Description {get;set;}
      public int Type {get;set;}
      public int Control{get; set;}
      public int Status{get; set;}
      public DateTime Dated { get; set; }
    }
}
