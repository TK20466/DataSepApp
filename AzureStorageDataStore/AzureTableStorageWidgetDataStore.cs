using Abstractions;
using DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Widget> CreateNew(Widget item)
        {
            int id = idGenerator.Next(1, int.MaxValue);

            WidgetTableEntity entity = new WidgetTableEntity(id, item.Description);

            await tableAccess.AddAsync(entity);

            item.Id = id;

            return item;
        }

        public async Task Delete(Widget item)
        {
            WidgetTableEntity entity = await tableAccess.GetSingleAsync("widget", item.Id.ToString());

            if (entity != null)
            {
                await tableAccess.DeleteAsync(entity);
            }
        }

        public async Task<Widget> FindById(int id)
        {
            WidgetTableEntity entity = await tableAccess.GetSingleAsync("widget", id.ToString());
            
            if (entity != null)
            {
                Widget w = new Widget
                {
                    Id = entity.Id,
                    Description = entity.Name
                };

                return w;
            }

            return null;
        }

        public Task<PagedSearchResult<Widget>> PagedSearch(WidgetSearchRequest searchRequest)
        {
            return null;
        }

        public async Task<Widget> UpdateExisting(Widget item)
        {
            WidgetTableEntity entity = await tableAccess.GetSingleAsync("widget", item.Id.ToString());

            if (entity != null)
            {
                entity.Name = item.Description;

                await tableAccess.InsertOrReplaceAsync(entity);
            }

            return item;
        }
    }
}
