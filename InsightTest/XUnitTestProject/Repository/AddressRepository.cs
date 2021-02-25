using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace XUnitTestProject.Repository
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public AddressRepository(string databaseConnectionString) : base(databaseConnectionString)
        {
        }

        public AddressRepository(SqlConnection connection) : base(connection)
        {
        }

        public override string SelectSingle => "SelectAddress";

        #region Select
        public IList<Address> GetAll()
        {
            return _connection.Query<Address>("FindAddresses");
        }

        public Address SelectAddress(int addressId)
        {
            return _connection.Query<Address>(SelectSingle, new { AddressId = addressId }).FirstOrDefault();
        }

        public IList<Address> SelectAddresses(List<int> addressIds)
        {
            var addresses = new List<Address>();
            addressIds.ForEach(i => addresses.Add(new Address() { AddressId = i }));
            return _connection.Query<Address>("SelectAddresss", addresses);
        }
        #endregion

        #region Insert
        public Address InsertAddress(Address address)
        {
            return _connection.Insert("InsertAddress", address);
        }

        public IEnumerable<Address> InsertAddresses(IList<Address> addresses)
        {
            return _connection.InsertList("InsertAddresses", addresses);
        }
        #endregion

        #region Delete
        public int DeleteAddress(Address address)
        {
            return DeleteAddress(address.AddressId);
        }

        public int DeleteAddress(int addressId)
        {
            return _connection.Execute("DeleteAddress", new Address { AddressId = addressId });
        }

        public int DeleteAddresses(List<Address> addresses)
        {
            var addressIds = new List<int>();
            addresses.ForEach(i => addressIds.Add(i.AddressId));
            return DeleteAddresses(addressIds);
        }

        public int DeleteAddresses(List<int> addressIds)
        {
            var addresses = new List<Address>();
            addressIds.ForEach(i => addresses.Add(new Address { AddressId = i }));
            return _connection.Execute("DeleteAddresses", addresses);
        }
        #endregion

        #region Find
        public IList<Address> FindAddresses(Address address)
        {
            return _connection.Query<Address>("FindAddresses", address);
        }

        public IList<Address> FindAddresses(object criteria)
        {
            return _connection.Query<Address>("FindAddresses", criteria);
        }
        #endregion
    }
}
