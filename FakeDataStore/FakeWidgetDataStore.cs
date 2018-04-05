using DataTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace FakeDataStore
{
    public class FakeWidgetDataStore : IDataStore<Widget, int, WidgetSearchRequest>
    {
        private static List<Widget> widgets = new List<Widget>();
        private static int nextId = 1;


        public Task<Widget> CreateNew(Widget item)
        {
            item.Id = nextId;
            nextId++;

            widgets.Add(item);

            return Task.FromResult<Widget>(item);
        }

        public Task Delete(Widget item)
        {
            widgets.Remove(item);

            return Task.FromResult(0);
        }

        public Task<Widget> FindById(int id)
        {
            return Task.FromResult<Widget>(widgets.SingleOrDefault(x => x.Id == id));
        }

        public Task<Widget> UpdateExisting(Widget item)
        {
            return Task.FromResult<Widget>(item);
        }

        public Task<PagedSearchResult<Widget>> PagedSearch(WidgetSearchRequest searchRequest)
        {
            IEnumerable<Widget> results = widgets;

            if (!string.IsNullOrEmpty(searchRequest.Name))
            {
                results = results.Where(x => x.Description.Contains(searchRequest.Name));
            }

            if (searchRequest.OrderBy.Any())
            {
                // TODO : make it go :)
            }
            else
            {
                results = results.OrderBy(x => x.Id);
            }

            results = results.Skip(searchRequest.PageSize * (searchRequest.Page - 1)).Take(searchRequest.PageSize);

            var stuffs = results.ToList();


            PagedSearchResult<Widget> result = new PagedSearchResult<Widget>
            {
                Page = searchRequest.Page,
                PageSize = searchRequest.PageSize,
                Count = widgets.Count(),
                Results = new ReadOnlyCollection<Widget>(stuffs)
            };


            return Task.FromResult<PagedSearchResult<Widget>>(result);
        }
    }
}
