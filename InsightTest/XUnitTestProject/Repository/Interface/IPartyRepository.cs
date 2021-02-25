using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject.Repository
{
    public interface IPartyRepository
    {
        Party SelectParty(int partyId);
        void InsertParty(Party party);
        void UpdateParty(Address party);
    }
}
