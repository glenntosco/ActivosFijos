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
    [Route("odata/FixedAssetsDB/MaintenanceRecords")]
    public partial class MaintenanceRecordsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public MaintenanceRecordsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> GetMaintenanceRecords()
        {
            var items = this.context.MaintenanceRecords.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>();
            this.OnMaintenanceRecordsRead(ref items);

            return items;
        }

        partial void OnMaintenanceRecordsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> items);

        partial void OnMaintenanceRecordGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/MaintenanceRecords(MaintenanceId={MaintenanceId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> GetMaintenanceRecord(int key)
        {
            var items = this.context.MaintenanceRecords.Where(i => i.MaintenanceId == key);
            var result = SingleResult.Create(items);

            OnMaintenanceRecordGet(ref result);

            return result;
        }
        partial void OnMaintenanceRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        [HttpDelete("/odata/FixedAssetsDB/MaintenanceRecords(MaintenanceId={MaintenanceId})")]
        public IActionResult DeleteMaintenanceRecord(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.MaintenanceRecords
                    .Where(i => i.MaintenanceId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnMaintenanceRecordDeleted(item);
                this.context.MaintenanceRecords.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMaintenanceRecordDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMaintenanceRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        [HttpPut("/odata/FixedAssetsDB/MaintenanceRecords(MaintenanceId={MaintenanceId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMaintenanceRecord(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.MaintenanceId != key))
                {
                    return BadRequest();
                }
                this.OnMaintenanceRecordUpdated(item);
                this.context.MaintenanceRecords.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaintenanceRecords.Where(i => i.MaintenanceId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterMaintenanceRecordUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/MaintenanceRecords(MaintenanceId={MaintenanceId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMaintenanceRecord(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.MaintenanceRecords.Where(i => i.MaintenanceId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnMaintenanceRecordUpdated(item);
                this.context.MaintenanceRecords.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaintenanceRecords.Where(i => i.MaintenanceId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterMaintenanceRecordUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMaintenanceRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item)
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

                this.OnMaintenanceRecordCreated(item);
                this.context.MaintenanceRecords.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.MaintenanceRecords.Where(i => i.MaintenanceId == item.MaintenanceId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");

                this.OnAfterMaintenanceRecordCreated(item);

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
