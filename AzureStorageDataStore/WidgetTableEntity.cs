using Microsoft.WindowsAzure.Storage.Table;

namespace AzureStorageDataStore
{
    public class WidgetTableEntity : TableEntity
    {
        public WidgetTableEntity(int id)
        {
            this.PartitionKey = "widget";
            this.RowKey = id.ToString();
        }

        public WidgetTableEntity(int id, string name)
            : this(id)
        {
            this.Name = name;
        }

        public int Id { get => int.Parse(this.RowKey); }

        public string Name { get; set; }
    }
}
