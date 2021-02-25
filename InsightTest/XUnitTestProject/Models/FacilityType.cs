using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject
{
    public class FacilityType
    {
        public int FacilityTypeId { get; set; }
        public string FacilityTypeCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatePartyId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatePartyId { get; set; }
    }
}
