using System.Collections.Generic;

namespace Abstractions
{
    public interface IDataStore<T, U, V> where T : class
    {
        T FindById(U id);

        T CreateNew(T item);

        T UpdateExisting(T item);

        void Delete(T item);

        PagedSearchResult<T> PagedSearch(V searchRequest);
    }
}
