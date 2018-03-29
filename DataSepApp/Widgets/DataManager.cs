namespace DataSepApp.Widgets
{
    public class DataManager<T, U>
         where T : class
    {
        public DataManager(IDataStore<T,U> dataStore)
        {
            this.DataStore = dataStore;
        }

        protected IDataStore<T, U> DataStore { get; }

        public virtual T GetSingle(U id)
        {
            return this.DataStore.FindById(id);
        }

        public virtual T Add(T newItem)
        {
            return this.DataStore.CreateNew(newItem);
        }

        public virtual T Update(T item)
        {
            return this.DataStore.UpdateExisting(item);
        }

        public virtual void Delete(T item)
        {
            this.DataStore.Delete(item);
        }

    }

}
