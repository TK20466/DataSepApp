namespace DataSepApp.Widgets
{
    public class FakeWidgetDataStore : IDataStore<Widget, int>
    {
        public Widget CreateNew(Widget item)
        {
            return item;
        }

        public void Delete(Widget item)
        {
            
        }

        public Widget FindById(int id)
        {
            return new Widget { Id = id, Description = "From fake data store" };
        }

        public Widget UpdateExisting(Widget item)
        {
            return item;
        }
    }
}
