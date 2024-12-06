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
    [Route("odata/FixedAssetsDB/Statuses")]
    public partial class StatusesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public StatusesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Status> GetStatuses()
        {
            var items = this.context.Statuses.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Status>();
            this.OnStatusesRead(ref items);

            return items;
        }

        partial void OnStatusesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Status> items);

        partial void OnStatusGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Status> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Statuses(StatusId={StatusId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Status> GetStatus(int key)
        {
            var items = this.context.Statuses.Where(i => i.StatusId == key);
            var result = SingleResult.Create(items);

            OnStatusGet(ref result);

            return result;
        }
        partial void OnStatusDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        [HttpDelete("/odata/FixedAssetsDB/Statuses(StatusId={StatusId})")]
        public IActionResult DeleteStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Statuses
                    .Where(i => i.StatusId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnStatusDeleted(item);
                this.context.Statuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnStatusUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        [HttpPut("/odata/FixedAssetsDB/Statuses(StatusId={StatusId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutStatus(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Status item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.StatusId != key))
                {
                    return BadRequest();
                }
                this.OnStatusUpdated(item);
                this.context.Statuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Statuses.Where(i => i.StatusId == key);
                
                this.OnAfterStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Statuses(StatusId={StatusId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchStatus(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Status> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Statuses.Where(i => i.StatusId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnStatusUpdated(item);
                this.context.Statuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Statuses.Where(i => i.StatusId == key);
                
                this.OnAfterStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnStatusCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Status item)
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

                this.OnStatusCreated(item);
                this.context.Statuses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Statuses.Where(i => i.StatusId == item.StatusId);

                

                this.OnAfterStatusCreated(item);

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
