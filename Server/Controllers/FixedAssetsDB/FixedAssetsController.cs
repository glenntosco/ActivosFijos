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
    [Route("odata/FixedAssetsDB/FixedAssets")]
    public partial class FixedAssetsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public FixedAssetsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> GetFixedAssets()
        {
            var items = this.context.FixedAssets.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>();
            this.OnFixedAssetsRead(ref items);

            return items;
        }

        partial void OnFixedAssetsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> items);

        partial void OnFixedAssetGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/FixedAssets(AssetId={AssetId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> GetFixedAsset(int key)
        {
            var items = this.context.FixedAssets.Where(i => i.AssetId == key);
            var result = SingleResult.Create(items);

            OnFixedAssetGet(ref result);

            return result;
        }
        partial void OnFixedAssetDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        [HttpDelete("/odata/FixedAssetsDB/FixedAssets(AssetId={AssetId})")]
        public IActionResult DeleteFixedAsset(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.FixedAssets
                    .Where(i => i.AssetId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnFixedAssetDeleted(item);
                this.context.FixedAssets.Remove(item);
                this.context.SaveChanges();
                this.OnAfterFixedAssetDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFixedAssetUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        [HttpPut("/odata/FixedAssetsDB/FixedAssets(AssetId={AssetId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutFixedAsset(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.AssetId != key))
                {
                    return BadRequest();
                }
                this.OnFixedAssetUpdated(item);
                this.context.FixedAssets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FixedAssets.Where(i => i.AssetId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory,Location,Status,User");
                this.OnAfterFixedAssetUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/FixedAssets(AssetId={AssetId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchFixedAsset(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.FixedAssets.Where(i => i.AssetId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnFixedAssetUpdated(item);
                this.context.FixedAssets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FixedAssets.Where(i => i.AssetId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory,Location,Status,User");
                this.OnAfterFixedAssetUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnFixedAssetCreated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetCreated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item)
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

                this.OnFixedAssetCreated(item);
                this.context.FixedAssets.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.FixedAssets.Where(i => i.AssetId == item.AssetId);

                Request.QueryString = Request.QueryString.Add("$expand", "AssetCategory,Location,Status,User");

                this.OnAfterFixedAssetCreated(item);

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
