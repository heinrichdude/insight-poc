using System;
using System.Collections.Generic;
using System.Text;
using Insight.Database;

namespace XUnitTestProject
{
    public class Facility
    {
        [RecordId]
        public int FacilityId { get; set; }
        public int? ParentFacilityId { get; set; }
        public int FacilityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FacilityNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatePartyId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdatePartyId { get; set; }
        [ChildRecords]
        public List<Address> Addresses { get; set; }
    }
}
