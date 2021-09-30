using AzureStorageTetst.DataAccess;
using AzureStorageTetst.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace AzureStorageTetst
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Azure Storage Account and Table Service Instances
            CloudStorageAccount storageAccount;

            CloudTableClient tableClient;

            // Connnect to Storage Account

            storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            // Create the Table "Book", if it not exists
                
            tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Book");
            table.CreateIfNotExistsAsync();

            // Create a Book instance

            Book book = new Book()
            {

                Author = "Rami",
                BookName = "ASP.NET Core With Azure",

                Publisher = "APress"
            };

            book.BookId = 1;
            book.RowKey = book.BookId.ToString();

            book.PartitionKey = book.Publisher;

            book.CreateDate = DateTime.UtcNow;

            book.UpdatedDate = DateTime.UtcNow;

            // Insert and execute operations

            TableOperation insertoperation = TableOperation.Insert(book);
            table.ExecuteAsync(insertoperation);

            Console.ReadLine();

            using (var _unitOfWork = new UnitOfWork("UseDevelopmentstorage-true;"))
            {

                var bookRepository = _unitOfWork.Repository<Book>();
                await bookRepository.CreateTableAsync();
                Book bookk = new Book()
                {
                    Author = "Ramiasdsad",
                    BookName = "ASP.NET Core With Azure",
                    Publisher = "APress"
                };

                bookk.BookId = 1;
                bookk.RowKey = bookk.BookId.ToString();
                bookk.PartitionKey = bookk.Publisher;

                var data = await bookRepository.AddAsyne(bookk);
                Console.WriteLine(data);
                _unitOfWork.CommitTransaction();
            }

            using (var _unitOfWork = new UnitOfWork("UseDevelopmentStorage=true;"))
            {
                var bookRepository = _unitOfWork.Repository<Book>();
                await bookRepository.CreateTableAsync();

                var data = await bookRepository.FindAsync("APress", "1");
                Console.WriteLine(data);

                data.Author = "Rami Vemula";

                var updatedData = await bookRepository.Updateasyne(data);
                Console.WriteLine(updatedData);

                _unitOfWork.CommitTransaction();

            }
            using (var _unitOfWork = new UnitOfWork("UseDevelopmentStorage=true;"))
            {

                var bookRepository = _unitOfWork.Repository<Book>();
                await bookRepository.CreateTableAsync();

                var data = await bookRepository.FindAsync("APress", "1");
                Console.WriteLine(data);

                await bookRepository.Deleteasync(data);
                Console.WriteLine("Deleted");

                // Throw an exception to test rollback actions

                // throw new Exception();
                _unitOfWork.CommitTransaction();
            }
        }

    } 
}
