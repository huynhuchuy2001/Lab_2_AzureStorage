using AzureStorageTetst.DataAccess.Interfaces;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageTetst.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool complete;
        private Dictionary<string, object> _repositories;

        public Queue<Task<Action>> Rollbackactions { get; set; }



        string Connectionstring { get; set; }
        public Queue<Task<Action>> RollbackActions { get; set; }
        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UnitOfWork(string connectionString)
        {
            Connectionstring = connectionString;
            Rollbackactions = new Queue<Task<Action>>();
        }



        public void CommitTransaction()
        {
            complete = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {


                try
                {
                    if (!complete)
                        RollbackTransaction();
                }
                finally
                {
                    RollbackActions.Clear();
                }
            }
            complete = false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void RollbackTransaction()
        {
            while (RollbackActions.Count > 0)
            {
                var undonction = RollbackActions.Dequeue();
                undonction.Result();
            }
        }

        public IRepository<T> Repository<T>() where T : TableEntity
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;

            if (_repositories.ContainsKey(type)) return (IRepository<T>)_repositories[type];
            var RepositoryType = typeof(Repository<>);
            var RepositoryInstance = Activator.CreateInstance(RepositoryType.MakeGenericType(typeof(T)), this);
            _repositories.Add(type, RepositoryInstance);
            return (IRepository < T >) _repositories[type];
        }

        public void CommitTransction()
        {
            throw new NotImplementedException();
        }
    }
}
