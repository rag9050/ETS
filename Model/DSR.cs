using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DSR
    {
      public int  SNo { get; set; }
      public string Name { get; set; }
      public string OfficialMailID { get; set; }
      public String Title { get; set; }
      public int TaskID { get; set; }
      public string Description { get; set; }
      public int EffertsPerformed { get; set; }
      public int EffertsRemaining { get; set; }
      public int Progress { get; set; }
      public int Status { get; set; }
      public DateTime Dated { get; set; }       
    }
}
