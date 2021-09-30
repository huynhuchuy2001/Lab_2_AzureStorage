using AzureStorageTetst.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageTetst.DataAccess.Interfaces
{
    public interface IRepository<T> where T : TableEntity

    {
        Task<T> AddAsyne(T entity);

        Task<T> Updateasyne(T entity);

        Task Deleteasync(T entity);

        Task<T> FindAsync(string partitionkey, string rowKey);
        Task<IEnumerable<T>> FindAllByPartitionKeyAsync(string partitionkey);
        Task<IEnumerable<T>> FindAllasync();

        Task CreateTableAsync();
    }

}