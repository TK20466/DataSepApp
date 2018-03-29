using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSepApp.Licenses
{
    public class LicenseManager : ILicenseManager
    {
        public LicenseManager(ILicenseStore store)
        {
            this.Store = store;
        }

        public ILicenseStore Store { get; }

        public virtual ILicense FindById(int id)
        {
            return this.Store.Single(id);
        }

        public virtual void Create(ILicense license)
        {
            this.Store.Add(license);
        }

        public virtual ILicense Default()
        {
            return this.Store.Blank();
        }
    }
}
