namespace AzureStorageDataStore
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Generic wrapper for Azure Table access
    /// </summary>
    /// <typeparam name="T">the type to access from the table</typeparam>
    internal class TableAccess<T> : IDisposable
        where T : class, ITableEntity
    {
        /// <summary> To detect redundant calls </summary>
        private bool disposedValue = false;

        /// <summary> users table </summary>
        private CloudTable cloudTable;

        /// <summary> table client </summary>
        private CloudTableClient cloudTableClient;

        /// <summary> storage account </summary>
        private CloudStorageAccount cloudStorageAccount;

        /// <summary> the table name </summary>
        private string tableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAccess{T}"/> class.
        /// </summary>
        /// <param name="connectionString"> azure table connection string </param>
        /// <param name="tableName"> table name to use for storage </param>
        public TableAccess(string connectionString, string tableName)
        {
            this.cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            this.cloudTableClient = this.cloudStorageAccount.CreateCloudTableClient();
            this.CloudTable = this.cloudTableClient.GetTableReference(tableName);
            this.tableName = tableName;
        }

        /// <summary> Gets or sets the table to read from </summary>
        protected internal CloudTable CloudTable { get => this.cloudTable; set => this.cloudTable = value; }

        /// <summary>
        /// Dispose of managed and unmanaged resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity"> the entity to add </param>
        /// <returns>true if success, false if failure</returns>
        public virtual async Task<TableResult> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.CreateIfNotExists();

            TableOperation insertOperation = TableOperation.Insert(entity);

            TableResult result = await this.CloudTable.ExecuteAsync(insertOperation);

            return result;
        }

        /// <summary>
        /// Get a single entity based on its partition and row keys
        /// </summary>
        /// <param name="partitionKey"> the partition key </param>
        /// <param name="rowKey"> the row key </param>
        /// <returns>An entity of type T </returns>
        /// <exception cref="NotFoundException"> Thrown if the entity is not found. </exception>
        public virtual async Task<T> GetSingleAsync(string partitionKey, string rowKey)
        {
            if (string.IsNullOrWhiteSpace(partitionKey) || string.IsNullOrWhiteSpace(rowKey))
            {
                throw new NotFoundException(partitionKey, rowKey);
            }

            await this.CreateIfNotExists();

            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            TableResult retrievedResult = await this.CloudTable.ExecuteAsync(retrieveOperation);

            if (retrievedResult.Result != null)
            {
                return (T)retrievedResult.Result;
            }

            throw new NotFoundException(partitionKey, rowKey);
        }

        /// <summary>
        /// Insert or Replace an entity
        /// </summary>
        /// <param name="entity"> the entity to insert or replace </param>
        /// <returns>A TableResult containing information about the operation. </returns>
        public virtual async Task<TableResult> InsertOrReplaceAsync(T entity)
        {
            await this.CreateIfNotExists();

            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(entity);

            TableResult result = await this.CloudTable.ExecuteAsync(insertOrReplaceOperation);

            return result;
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity"> the entity to delete </param>
        /// <returns>A table result </returns>
        public virtual async Task<TableResult> DeleteAsync(T entity)
        {
            await this.CreateIfNotExists();

            TableOperation retrieveOperation = TableOperation.Retrieve<T>(entity.PartitionKey, entity.RowKey);

            TableResult retrieveResult = await this.CloudTable.ExecuteAsync(retrieveOperation);

            T deleteEntity = (T)retrieveResult.Result;

            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                TableResult deleteResult = await this.CloudTable.ExecuteAsync(deleteOperation);

                return deleteResult;
            }

            return retrieveResult;
        }

        /// <summary>
        /// Dispose of managed and unmanaged resources
        /// </summary>
        /// <param name="disposing">true if called from Dispose, false during finalizer</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.CloudTable = null;
                    this.cloudTableClient = null;
                    this.cloudStorageAccount = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Create the table if it doesn't already exist
        /// </summary>
        /// <returns>A task </returns>
        protected async Task CreateIfNotExists()
        {
            bool exists = await this.CloudTable.ExistsAsync();

            if (!exists)
            {
                await this.CloudTable.CreateAsync();
            }
        }
    }
}
