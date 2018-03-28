namespace DataSepApp.Licenses
{
    public interface ILicenseManager
    {
        ILicense FindById(int id);
    }
}