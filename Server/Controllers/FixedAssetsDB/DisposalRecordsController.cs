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
    [Route("odata/FixedAssetsDB/DisposalRecords")]
    public partial class DisposalRecordsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public DisposalRecordsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> GetDisposalRecords()
        {
            var items = this.context.DisposalRecords.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>();
            this.OnDisposalRecordsRead(ref items);

            return items;
        }

        partial void OnDisposalRecordsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> items);

        partial void OnDisposalRecordGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/DisposalRecords(DisposalId={DisposalId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> GetDisposalRecord(int key)
        {
            var items = this.context.DisposalRecords.Where(i => i.DisposalId == key);
            var result = SingleResult.Create(items);

            OnDisposalRecordGet(ref result);

            return result;
        }
        partial void OnDisposalRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        [HttpDelete("/odata/FixedAssetsDB/DisposalRecords(DisposalId={DisposalId})")]
        public IActionResult DeleteDisposalRecord(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.DisposalRecords
                    .Where(i => i.DisposalId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnDisposalRecordDeleted(item);
                this.context.DisposalRecords.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDisposalRecordDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDisposalRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        [HttpPut("/odata/FixedAssetsDB/DisposalRecords(DisposalId={DisposalId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDisposalRecord(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.DisposalId != key))
                {
                    return BadRequest();
                }
                this.OnDisposalRecordUpdated(item);
                this.context.DisposalRecords.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DisposalRecords.Where(i => i.DisposalId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterDisposalRecordUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/DisposalRecords(DisposalId={DisposalId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDisposalRecord(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.DisposalRecords.Where(i => i.DisposalId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnDisposalRecordUpdated(item);
                this.context.DisposalRecords.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DisposalRecords.Where(i => i.DisposalId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterDisposalRecordUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDisposalRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item)
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

                this.OnDisposalRecordCreated(item);
                this.context.DisposalRecords.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.DisposalRecords.Where(i => i.DisposalId == item.DisposalId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");

                this.OnAfterDisposalRecordCreated(item);

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
