using Dapper;
using System.Collections.Generic;
using System.Data;

namespace CodeProject.StoreDB.Interfaces.DAL
{
    public interface IDapperRepositoryBase<T>
    {
        void Insert(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        void Delete(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        void Update(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        dynamic GetById(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        List<dynamic> GetManyRows(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        int GetCount(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        List<dynamic> GetAll(string sqlQuery, CommandType commandType);
    }
}