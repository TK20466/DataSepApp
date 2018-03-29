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
        [ProducesResponseType(200, Type = typeof(LicenseModel))]
        public IActionResult Get(int id)
        {
            var license = this.LicenseManager.FindById(id);

            if(license == null)
            {
                return this.NotFound();
            }

            var licenseModel = this.BuildModel(license);

            return this.Ok(licenseModel);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(201, Type = typeof(LicenseModel))]
        public IActionResult Post(LicenseModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            ILicense license = this.LicenseManager.Default();

            license.Description = model.Description;

            this.LicenseManager.Create(license);

            var created = this.BuildModel(license);
            // nope
            return this.CreatedAtAction("Get", created);
        }

        private LicenseModel BuildModel(ILicense license)
        {
            var licenseModel = new LicenseModel
            {
                Id = license.Id,
                Description = license.Description,
            };

            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            licenseModel.TimeStamp = secondsSinceEpoch;

            return licenseModel;
        }
    }
}