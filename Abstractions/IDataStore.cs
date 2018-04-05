using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface IDataStore<T, U, V> where T : class
    {
        Task<T> FindById(U id);

        Task<T> CreateNew(T item);

        Task<T> UpdateExisting(T item);

        Task Delete(T item);

        Task<PagedSearchResult<T>> PagedSearch(V searchRequest);
    }
}
