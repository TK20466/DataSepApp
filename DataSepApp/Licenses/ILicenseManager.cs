namespace DataSepApp.Licenses
{
    public interface ILicenseManager
    {
        ILicense FindById(int id);

        void Create(ILicense license);

        ILicense Default();
    }
}