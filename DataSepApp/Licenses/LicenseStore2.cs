namespace DataSepApp.Licenses
{
    public class LicenseStore2 : ILicenseStore
    {
        public virtual ILicense Single(int id)
        {
            return new LicenseTwo { Id = id, Description = "From LicenseStore2" };
        }

        public virtual void Add(ILicense license)
        {
        }

        public virtual ILicense Blank()
        {
            return new LicenseTwo();
        }

        internal class LicenseTwo : ILicense
        {
            public int Id { get; set; }

            public string Description { get; set; }
        }
    }
}