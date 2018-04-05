using Abstractions;
using DataTypes;
using System.Threading.Tasks;

namespace SrsBidness.Widgets
{
    public class WidgetDataManager : DataManager<Widget, int, WidgetSearchRequest>, IWidgetDataManager
    {
        public WidgetDataManager(IDataStore<Widget, int, WidgetSearchRequest> dataStore)
            : base(dataStore)
        {

        }

        public override Task<Widget> Add(Widget newItem)
        {
            // here i'd so some logging or other super fun stuff

            return base.Add(newItem);
        }
    }

}
