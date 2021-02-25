using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Insight.Database;

namespace XUnitTestProject.Repository
{
    public class AddressTypeRepository : BaseRepository, IAddressTypeRepository
    {
        public AddressTypeRepository(string databaseConnectionString) : base(databaseConnectionString)
        {
        }

        public AddressTypeRepository(SqlConnection connection) : base(connection)
        {
        }

        public override string SelectSingle => "SelectAddressType";

        #region Select
        public IList<AddressType> GetAll()
        {
            return _connection.Query<AddressType>("FindAddressTypes");
        }

        public AddressType SelectAddressType(int addressTypeId)
        {
            return _connection.Query<AddressType>(SelectSingle, new { AddressTypeId = addressTypeId }).FirstOrDefault();
        }

        public IList<AddressType> SelectAddressTypes(List<int> addressTypeIds)
        {
            var addressTypes = new List<AddressType>();
            addressTypeIds.ForEach(i => addressTypes.Add(new AddressType() { AddressTypeId = i }));
            return _connection.Query<AddressType>("SelectAddressTypes", addressTypes);
        }
        #endregion

        #region Insert
        public AddressType InsertAddressType(AddressType addressType)
        {
            return _connection.Insert("InsertAddressType", addressType);
        }

        public IEnumerable<AddressType> InsertAddressTypes(IList<AddressType> addressTypes)
        {
            return _connection.InsertList("InsertAddressTypes", addressTypes);
        }
        #endregion

        #region Delete
        public int DeleteAddressType(AddressType addressType)
        {
            return DeleteAddressType(addressType.AddressTypeId);
        }

        public int DeleteAddressType(int addressTypeId)
        {
            return _connection.Execute("DeleteAddressType", new AddressType { AddressTypeId = addressTypeId });
        }

        public int DeleteAddressTypes(List<AddressType> addressTypes)
        {
            var addressTypeIds = new List<int>();
            addressTypes.ForEach(i => addressTypeIds.Add(i.AddressTypeId));
            return DeleteAddressTypes(addressTypeIds);
        }

        public int DeleteAddressTypes(List<int> addressTypeIds)
        {
            var addressTypes = new List<AddressType>();
            addressTypeIds.ForEach(i => addressTypes.Add(new AddressType { AddressTypeId = i }));
            return _connection.Execute("DeleteAddressTypes", addressTypes);
        }
        #endregion

        #region Find
        public IList<AddressType> FindAddressTypes(AddressType addressType)
        {
            return _connection.Query<AddressType>("FindAddressTypes", addressType);
        }

        public IList<AddressType> FindAddressTypes(object criteria)
        {
            return _connection.Query<AddressType>("FindAddressTypes", criteria);
        }
        #endregion
    }
}
