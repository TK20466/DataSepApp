﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using DataTypes;
using Microsoft.AspNetCore.Mvc;
using SrsBidness.Widgets;

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
        public async Task<IActionResult> Get(int id)
        {
            var thing = await this.DataManager.GetSingle(id);

            if (thing == null)
            {
                return this.NotFound();
            }

            var model = new WidgetModel { Id = thing.Id, Name = thing.Description };

            return this.Ok(model);
        }

        [HttpGet("", Name = "Search")]
        [ProducesResponseType(200, Type = typeof(PagedSearchResult<WidgetModel>))]
        public async Task<IActionResult> Search([FromQuery]WidgetSearchRequest searchRequest)
        {
            // 1. Checking model state, this can probably be done in global filters and validations steps
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            // 2. can this be done in a filter validation steps?
            WidgetSearchRequest actual = searchRequest ?? new WidgetSearchRequest();

            // 3. Hmm, have to pass search request into manager somehow, is this the best method
            PagedSearchResult<Widget> searchResults = await this.DataManager.PagedSearch(searchRequest);

            // 4. Could inline this, or maybe extension methods
            IList<WidgetModel> models = searchResults.Results.Select(x => new WidgetModel { Id = x.Id, Name = x.Description }).ToList();

            PagedSearchResult<WidgetModel> results = new PagedSearchResult<WidgetModel>
            {
                Page = searchResults.Page,
                PageSize = searchResults.PageSize,
                Count = searchResults.Count,
                Results = new ReadOnlyCollection<WidgetModel>(models)
            };

            return this.Ok(results);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WidgetModel widget)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Widget w = new Widget { Description = widget.Name };

            Widget z = await this.DataManager.Add(w);

            WidgetModel y = new WidgetModel { Id = z.Id, Name = z.Description };

            return this.CreatedAtAction("Post", y);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]WidgetModel value)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (value == null || id != value.Id)
            {
                return this.BadRequest();
            }

            Widget w = await this.DataManager.GetSingle(id);

            w.Description = value.Name;

            w = await this.DataManager.Update(w);

            return this.Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await this.DataManager.GetSingle(id);

            if (item == null)
            {
                return this.NotFound();
            }

            await this.DataManager.Delete(item);

            return this.Ok();
        }
    }
}
