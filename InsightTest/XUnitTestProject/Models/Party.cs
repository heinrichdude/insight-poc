using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    public class Party
    {
        public int PartyId { get; set; }
        public int PartyTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public string RegisteredFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatePartyId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatePartyId { get; set; }
    }
}
