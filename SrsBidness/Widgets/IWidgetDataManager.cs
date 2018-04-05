using Abstractions;
using DataTypes;
using System.Threading.Tasks;

namespace SrsBidness.Widgets
{
    public interface IWidgetDataManager
    {
        Task<Widget> GetSingle(int id);

        Task<Widget> Add(Widget newItem);

        Task<Widget> Update(Widget item);

        Task Delete(Widget item);

        Task<PagedSearchResult<Widget>> PagedSearch(WidgetSearchRequest searchRequest);
    }
}