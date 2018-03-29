namespace DataSepApp.Widgets
{
    public interface IWidgetDataManager
    {
        Widget GetSingle(int id);

        Widget Add(Widget newItem);

        Widget Update(Widget item);

        void Delete(Widget item);
    }
}