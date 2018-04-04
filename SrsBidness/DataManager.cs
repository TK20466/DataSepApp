using Abstractions;


namespace SrsBidness
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">the type being managed</typeparam>
    /// <typeparam name="U">the type of the unique id for the type being managed</typeparam>
    /// <typeparam name="V">the type of the search request for the type being managed</typeparam>
    public class DataManager<T, U, V>
         where T : class
    {
        public DataManager(IDataStore<T,U,V> dataStore)
        {
            this.DataStore = dataStore;
        }

        protected IDataStore<T, U, V> DataStore { get; }

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

        public virtual PagedSearchResult<T> PagedSearch(V searchParams)
        {
            return this.DataStore.PagedSearch(searchParams);
        }
    }
}
