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
    [Route("odata/FixedAssetsDB/Depreciations")]
    public partial class DepreciationsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public DepreciationsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> GetDepreciations()
        {
            var items = this.context.Depreciations.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>();
            this.OnDepreciationsRead(ref items);

            return items;
        }

        partial void OnDepreciationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> items);

        partial void OnDepreciationGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Depreciations(DepreciationId={DepreciationId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> GetDepreciation(int key)
        {
            var items = this.context.Depreciations.Where(i => i.DepreciationId == key);
            var result = SingleResult.Create(items);

            OnDepreciationGet(ref result);

            return result;
        }
        partial void OnDepreciationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        [HttpDelete("/odata/FixedAssetsDB/Depreciations(DepreciationId={DepreciationId})")]
        public IActionResult DeleteDepreciation(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Depreciations
                    .Where(i => i.DepreciationId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnDepreciationDeleted(item);
                this.context.Depreciations.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDepreciationDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepreciationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        [HttpPut("/odata/FixedAssetsDB/Depreciations(DepreciationId={DepreciationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDepreciation(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.DepreciationId != key))
                {
                    return BadRequest();
                }
                this.OnDepreciationUpdated(item);
                this.context.Depreciations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depreciations.Where(i => i.DepreciationId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterDepreciationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Depreciations(DepreciationId={DepreciationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDepreciation(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Depreciations.Where(i => i.DepreciationId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnDepreciationUpdated(item);
                this.context.Depreciations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depreciations.Where(i => i.DepreciationId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterDepreciationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDepreciationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item)
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

                this.OnDepreciationCreated(item);
                this.context.Depreciations.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Depreciations.Where(i => i.DepreciationId == item.DepreciationId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");

                this.OnAfterDepreciationCreated(item);

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
