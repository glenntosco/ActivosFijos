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
    [Route("odata/FixedAssetsDB/AssetAssignments")]
    public partial class AssetAssignmentsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public AssetAssignmentsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> GetAssetAssignments()
        {
            var items = this.context.AssetAssignments.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>();
            this.OnAssetAssignmentsRead(ref items);

            return items;
        }

        partial void OnAssetAssignmentsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> items);

        partial void OnAssetAssignmentGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/AssetAssignments(AssignmentId={AssignmentId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> GetAssetAssignment(int key)
        {
            var items = this.context.AssetAssignments.Where(i => i.AssignmentId == key);
            var result = SingleResult.Create(items);

            OnAssetAssignmentGet(ref result);

            return result;
        }
        partial void OnAssetAssignmentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        [HttpDelete("/odata/FixedAssetsDB/AssetAssignments(AssignmentId={AssignmentId})")]
        public IActionResult DeleteAssetAssignment(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AssetAssignments
                    .Where(i => i.AssignmentId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAssetAssignmentDeleted(item);
                this.context.AssetAssignments.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAssetAssignmentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAssignmentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        [HttpPut("/odata/FixedAssetsDB/AssetAssignments(AssignmentId={AssignmentId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAssetAssignment(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.AssignmentId != key))
                {
                    return BadRequest();
                }
                this.OnAssetAssignmentUpdated(item);
                this.context.AssetAssignments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAssignments.Where(i => i.AssignmentId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterAssetAssignmentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/AssetAssignments(AssignmentId={AssignmentId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAssetAssignment(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AssetAssignments.Where(i => i.AssignmentId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAssetAssignmentUpdated(item);
                this.context.AssetAssignments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAssignments.Where(i => i.AssignmentId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterAssetAssignmentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAssignmentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item)
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

                this.OnAssetAssignmentCreated(item);
                this.context.AssetAssignments.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAssignments.Where(i => i.AssignmentId == item.AssignmentId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");

                this.OnAfterAssetAssignmentCreated(item);

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
