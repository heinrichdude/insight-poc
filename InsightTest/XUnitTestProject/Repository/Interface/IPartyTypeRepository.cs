using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IPartyTypeRepository
    {
        PartyType SelectPartyType(int partyTypeId);
        void InsertAddress(PartyType partyType);
        void UpdateAddress(PartyType partyType);
    }
}
