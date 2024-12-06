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
    [Route("odata/FixedAssetsDB/AssetAttributeValues")]
    public partial class AssetAttributeValuesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public AssetAttributeValuesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> GetAssetAttributeValues()
        {
            var items = this.context.AssetAttributeValues.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>();
            this.OnAssetAttributeValuesRead(ref items);

            return items;
        }

        partial void OnAssetAttributeValuesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> items);

        partial void OnAssetAttributeValueGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/AssetAttributeValues(AttributeValueId={AttributeValueId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> GetAssetAttributeValue(int key)
        {
            var items = this.context.AssetAttributeValues.Where(i => i.AttributeValueId == key);
            var result = SingleResult.Create(items);

            OnAssetAttributeValueGet(ref result);

            return result;
        }
        partial void OnAssetAttributeValueDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        [HttpDelete("/odata/FixedAssetsDB/AssetAttributeValues(AttributeValueId={AttributeValueId})")]
        public IActionResult DeleteAssetAttributeValue(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AssetAttributeValues
                    .Where(i => i.AttributeValueId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAssetAttributeValueDeleted(item);
                this.context.AssetAttributeValues.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAssetAttributeValueDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAttributeValueUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        [HttpPut("/odata/FixedAssetsDB/AssetAttributeValues(AttributeValueId={AttributeValueId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAssetAttributeValue(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.AttributeValueId != key))
                {
                    return BadRequest();
                }
                this.OnAssetAttributeValueUpdated(item);
                this.context.AssetAttributeValues.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributeValues.Where(i => i.AttributeValueId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,AssetAttribute");
                this.OnAfterAssetAttributeValueUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/AssetAttributeValues(AttributeValueId={AttributeValueId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAssetAttributeValue(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AssetAttributeValues.Where(i => i.AttributeValueId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAssetAttributeValueUpdated(item);
                this.context.AssetAttributeValues.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributeValues.Where(i => i.AttributeValueId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,AssetAttribute");
                this.OnAfterAssetAttributeValueUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetAttributeValueCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item)
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

                this.OnAssetAttributeValueCreated(item);
                this.context.AssetAttributeValues.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetAttributeValues.Where(i => i.AttributeValueId == item.AttributeValueId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,AssetAttribute");

                this.OnAfterAssetAttributeValueCreated(item);

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
