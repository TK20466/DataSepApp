using DataTypes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Abstractions;
using System.Linq;

namespace FakeDataStore
{
    public class FakeWidgetDataStore : IDataStore<Widget, int, WidgetSearchRequest>
    {
        private static List<Widget> widgets = new List<Widget>();
        private static int nextId = 1;


        public Widget CreateNew(Widget item)
        {
            item.Id = nextId;
            nextId++;

            widgets.Add(item);

            return item;
        }

        public void Delete(Widget item)
        {
            widgets.Remove(item);            
        }

        public Widget FindById(int id)
        {
            return widgets.SingleOrDefault(x => x.Id == id);
        }

        public Widget UpdateExisting(Widget item)
        {
            return item;
        }

        public PagedSearchResult<Widget> PagedSearch(WidgetSearchRequest searchRequest)
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


            return result;
        }
    }
}
