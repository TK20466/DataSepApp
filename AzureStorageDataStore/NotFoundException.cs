
namespace AzureStorageDataStore
{ 
    using System;

    /// <summary>
    /// An exception for a not found entity in an azure table
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="partitionKey">partition key </param>
        /// <param name="rowKey"> row key </param>
        public NotFoundException(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        /// <summary>
        /// Gets or sets the partition key that wasn't found
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// Gets or sets the row key that wasn't found
        /// </summary>
        public string RowKey { get; set; }
    }
}
