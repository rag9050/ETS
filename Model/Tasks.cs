using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Task
    {
        public int TaskId { get; set; }
        public int CreatedByCode { get; set; }
        public string CreatedByName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimatedEfforts { get; set; }
        public int Type { get; set; }
        public int TaskStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MapID { get; set; }
        public int AssignedToCode { get; set; }
        public string AssignedName { get; set; }
        public DateTime AssignedDate { get; set; }
        public int EffortsPerformed { get; set; }
        public int TestingStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MapStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string strXMLData { get; set; }
    }
}
