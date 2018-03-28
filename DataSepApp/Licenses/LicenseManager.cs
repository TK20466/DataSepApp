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

        public ILicense FindById(int id)
        {
            return this.Store.Single(id);
        }
    }
}
