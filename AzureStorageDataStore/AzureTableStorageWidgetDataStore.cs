using Abstractions;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureStorageDataStore
{
    public class AzureTableStorageWidgetDataStore : IDataStore<Widget, int, WidgetSearchRequest>
    {
        TableAccess<WidgetTableEntity> tableAccess;

        static Random idGenerator = new Random();

        public AzureTableStorageWidgetDataStore(string connectionString, string tableName = "widget")
        {
            tableAccess = new TableAccess<WidgetTableEntity>(connectionString, tableName);
        }

        public Widget CreateNew(Widget item)
        {
            int id = idGenerator.Next(1, int.MaxValue);

            WidgetTableEntity entity = new WidgetTableEntity(id, item.Description);

            tableAccess.AddAsync(entity);

            item.Id = id;

            return item;
        }

        public void Delete(Widget item)
        {
        }

        public Widget FindById(int id)
        {
            return null;
        }

        public PagedSearchResult<Widget> PagedSearch(WidgetSearchRequest searchRequest)
        {
            return null;
        }

        public Widget UpdateExisting(Widget item)
        {
            return null;
        }
    }
}
