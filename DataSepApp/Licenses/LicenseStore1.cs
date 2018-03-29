namespace DataSepApp.Licenses
{
    public class LicenseStore1 : ILicenseStore
    {
        public virtual ILicense Single(int id)
        {
            return new LicenseOne { Id = id, Description = "From LicenseStore1" };
        }

        public virtual void Add(ILicense license)
        {
        }

        public virtual ILicense Blank()
        {
            return new LicenseOne();
        }

        internal class LicenseOne : ILicense
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}