namespace DataSepApp.Licenses
{
    public interface ILicenseStore
    {
        ILicense Single(int id);

        void Add(ILicense license);

        ILicense Blank();
    }
}