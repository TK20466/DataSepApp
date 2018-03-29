namespace DataSepApp.Widgets
{
    public interface IDataStore<T, U>
    {
        T FindById(U id);

        T CreateNew(T item);

        T UpdateExisting(T item);

        void Delete(T item);
    }

}
