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

        T GetById(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        List<T> GetManyRows(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        int GetCount(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType);

        List<T> GetAll(string sqlQuery, CommandType commandType);
    }
}