namespace DataSepApp.Licenses
{
    public interface ILicenseStore
    {
        ILicense Single(int id);
    }

    public class LicenseStore1 : ILicenseStore
    {
        public ILicense Single(int id)
        {
            return new LicenseOne { Id = id, Description = "From LicenseStore1" };
        }

        internal class LicenseOne : ILicense
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }

    public class LicenseStore2 : ILicenseStore
    {
        public ILicense Single(int id)
        {
            return new LicenseTwo { Id = id, Description = "From LicenseStore2" };
        }

        internal class LicenseTwo : ILicense
        {
            public int Id { get; set; }

            public string Description { get; set; }
        }
    }
}