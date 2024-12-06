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
    [Route("odata/FixedAssetsDB/AssetAttributes")]
    public partial class AssetAttributesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public AssetAttributesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> GetAssetAttributes()
        {
            var items = this.context.AssetAttributes.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>();
            this.OnAssetAttributesRead(ref items);

            return items;
        }

        partial void OnAssetAttributesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> items);

        partial void OnAssetAttributeGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/AssetAttributes(AttributeId={AttributeId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> GetAssetAttribute(int key)
        {
            var items = this.context.AssetAttributes.Where(i => i.AttributeId == key);
            var result = SingleResult.Create(items);

            OnAssetAttributeGet(ref result);

            return result;
        }
        partial void OnAssetAttributeDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        [HttpDelete("/odata/FixedAssetsDB/AssetAttributes(AttributeId={AttributeId})")]
        public IActionResult DeleteAssetAttribute(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AssetAttributes
                    .Where(i => i.AttributeId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAssetAttributeDeleted(item);
                this.context.AssetAttributes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAssetAttributeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAttributeUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        [HttpPut("/odata/FixedAssetsDB/AssetAttributes(AttributeId={AttributeId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAssetAttribute(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.AttributeId != key))
                {
                    return BadRequest();
                }
                this.OnAssetAttributeUpdated(item);
                this.context.AssetAttributes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributes.Where(i => i.AttributeId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory");
                this.OnAfterAssetAttributeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/AssetAttributes(AttributeId={AttributeId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAssetAttribute(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AssetAttributes.Where(i => i.AttributeId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAssetAttributeUpdated(item);
                this.context.AssetAttributes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributes.Where(i => i.AttributeId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory");
                this.OnAfterAssetAttributeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAttributeCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item)
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

                this.OnAssetAttributeCreated(item);
                this.context.AssetAttributes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributes.Where(i => i.AttributeId == item.AttributeId);

                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory");

                this.OnAfterAssetAttributeCreated(item);

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
