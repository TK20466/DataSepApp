using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataSepApp.Widgets
{
    [Produces("application/json")]
    [Route("api/Widgets")]
    public class WidgetsController : Controller
    {
        public WidgetsController(IWidgetDataManager dataManager)
        {
            this.DataManager = dataManager;
        }
        public IWidgetDataManager DataManager { get; }


        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var thing = this.DataManager.GetSingle(id);

            if (thing == null)
            {
                return this.NotFound();
            }

            var model = new WidgetModel { Id = thing.Id, Name = thing.Description };

            return this.Ok(model);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]WidgetModel widget)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Widget w = new Widget { Id = widget.Id };

            Widget z = this.DataManager.Add(w);

            WidgetModel y = new WidgetModel { Id = z.Id };

            return this.CreatedAtAction("Post", y);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WidgetModel value)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (value == null || id != value.Id)
            {
                return this.BadRequest();
            }

            Widget w = this.DataManager.GetSingle(id);

            w.Description = value.Name;

            w = this.DataManager.Update(w);

            return this.Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this.DataManager.GetSingle(id);

            if (item == null)
            {
                return this.NotFound();
            }

            this.DataManager.Delete(item);

            return this.Ok();
        }
    }
}
