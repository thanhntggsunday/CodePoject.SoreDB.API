using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Interfaces.DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeProject.StoreDB.DataService.DapperRepository
{
    public class DapperRepositoryBase<T> : BaseClass, IDapperRepositoryBase<T> where T : class
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public DapperRepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
            OpenConnection();
        }

        private void OpenConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        public void Insert(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            Connection.Execute(sqlQuery, dynamicParameters, commandType: commandType, transaction: Transaction);
        }

        public void Delete(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            Connection.Execute(sqlQuery, dynamicParameters, commandType: commandType, transaction: Transaction);
        }

        public void Update(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            Connection.Execute(sqlQuery, dynamicParameters, commandType: commandType, transaction: Transaction);
        }

        public T GetById(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            IList<T> items = Connection.Query<T>(
                sqlQuery, dynamicParameters, commandType: commandType,
                transaction: Transaction
             ).ToList();

            var obj = items.ToList().FirstOrDefault();

            return obj;
        }

        public List<T> GetManyRows(string sqlQuery, DynamicParameters dynamicParameters,
                                          CommandType commandType)
        {
            IList<T> items = Connection.Query<T>(
                sqlQuery, dynamicParameters,
                commandType: commandType, transaction: Transaction
            ).ToList();

            return items.ToList();
        }

        public int GetCount(string sqlQuery, DynamicParameters dynamicParameters, CommandType commandType)
        {
            var result = Connection.ExecuteScalar<int>(
                sqlQuery, param: dynamicParameters,
                commandType: commandType, transaction: Transaction
             );

            return result;
        }

        public List<T> GetAll(string sqlQuery, CommandType commandType)
        {
            IList<T> items = Connection.Query<T>(
                sqlQuery, commandType: commandType,
                transaction: Transaction
             ).ToList();

            return items.ToList();
        }

        
    }
}