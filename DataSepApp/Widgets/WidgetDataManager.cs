namespace DataSepApp.Widgets
{
    public class WidgetDataManager : DataManager<Widget, int>, IWidgetDataManager
    {
        public WidgetDataManager(IDataStore<Widget, int> dataStore)
            : base(dataStore)
        {

        }

        public override Widget Add(Widget newItem)
        {
            // here i'd so some logging or other super fun stuff

            return base.Add(newItem);
        }
    }

}
