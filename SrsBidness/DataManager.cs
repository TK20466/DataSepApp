using Abstractions;
using System.Threading.Tasks;

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

        public virtual Task<T> GetSingle(U id)
        {
            return this.DataStore.FindById(id);
        }

        public virtual Task<T> Add(T newItem)
        {
            return this.DataStore.CreateNew(newItem);
        }

        public virtual Task<T> Update(T item)
        {
            return this.DataStore.UpdateExisting(item);
        }

        public async virtual Task Delete(T item)
        {
            await this.DataStore.Delete(item);
        }

        public virtual Task<PagedSearchResult<T>> PagedSearch(V searchParams)
        {
            return this.DataStore.PagedSearch(searchParams);
        }
    }
}
