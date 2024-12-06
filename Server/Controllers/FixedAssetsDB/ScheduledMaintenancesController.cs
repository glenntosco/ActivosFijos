using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ActivosFiljos.Server.Controllers.FixedAssetsDB
{
    [Route("odata/FixedAssetsDB/ScheduledMaintenances")]
    public partial class ScheduledMaintenancesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public ScheduledMaintenancesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> GetScheduledMaintenances()
        {
            var items = this.context.ScheduledMaintenances.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>();
            this.OnScheduledMaintenancesRead(ref items);

            return items;
        }

        partial void OnScheduledMaintenancesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> items);

        partial void OnScheduledMaintenanceGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/ScheduledMaintenances(ScheduleId={ScheduleId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> GetScheduledMaintenance(int key)
        {
            var items = this.context.ScheduledMaintenances.Where(i => i.ScheduleId == key);
            var result = SingleResult.Create(items);

            OnScheduledMaintenanceGet(ref result);

            return result;
        }
        partial void OnScheduledMaintenanceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        [HttpDelete("/odata/FixedAssetsDB/ScheduledMaintenances(ScheduleId={ScheduleId})")]
        public IActionResult DeleteScheduledMaintenance(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.ScheduledMaintenances
                    .Where(i => i.ScheduleId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnScheduledMaintenanceDeleted(item);
                this.context.ScheduledMaintenances.Remove(item);
                this.context.SaveChanges();
                this.OnAfterScheduledMaintenanceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnScheduledMaintenanceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        [HttpPut("/odata/FixedAssetsDB/ScheduledMaintenances(ScheduleId={ScheduleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutScheduledMaintenance(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.ScheduleId != key))
                {
                    return BadRequest();
                }
                this.OnScheduledMaintenanceUpdated(item);
                this.context.ScheduledMaintenances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ScheduledMaintenances.Where(i => i.ScheduleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterScheduledMaintenanceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/ScheduledMaintenances(ScheduleId={ScheduleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchScheduledMaintenance(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.ScheduledMaintenances.Where(i => i.ScheduleId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnScheduledMaintenanceUpdated(item);
                this.context.ScheduledMaintenances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ScheduledMaintenances.Where(i => i.ScheduleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterScheduledMaintenanceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnScheduledMaintenanceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnScheduledMaintenanceCreated(item);
                this.context.ScheduledMaintenances.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.ScheduledMaintenances.Where(i => i.ScheduleId == item.ScheduleId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");

                this.OnAfterScheduledMaintenanceCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
