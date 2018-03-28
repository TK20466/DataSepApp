using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataSepApp.Licenses
{
    [Produces("application/json")]
    [Route("api/Licenses")]
    public class LicensesController : Controller
    {
        public LicensesController(ILicenseManager licenseManager)
        {
            LicenseManager = licenseManager;
        }

        public ILicenseManager LicenseManager { get; }

        [HttpGet]
        [Route("{id:int}", Name = "Single license")]
        public IActionResult Get(int id)
        {
            var license = this.LicenseManager.FindById(id);

            var licenseModel = new LicenseModel
            {
                Id = license.Id,
                Description = license.Description,
            };

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            licenseModel.TimeStamp = secondsSinceEpoch;

            return this.Ok(licenseModel);
        }
    }
}