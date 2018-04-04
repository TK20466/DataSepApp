using Abstractions;
using DataTypes;

namespace SrsBidness.Widgets
{
    public interface IWidgetDataManager
    {
        Widget GetSingle(int id);

        Widget Add(Widget newItem);

        Widget Update(Widget item);

        void Delete(Widget item);

        PagedSearchResult<Widget> PagedSearch(WidgetSearchRequest searchRequest);
    }
}