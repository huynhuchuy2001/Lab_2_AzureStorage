using AzureStorageTetst.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageTetst.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Queue<Task<Action>> RollbackActions { get; set; }

        string ConnectionString { get; set; }

        IRepository<T> Repository<T>() where T : TableEntity;

        void CommitTransction();
    }


}