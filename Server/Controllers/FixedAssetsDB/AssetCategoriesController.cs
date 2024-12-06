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
    [Route("odata/FixedAssetsDB/AssetCategories")]
    public partial class AssetCategoriesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public AssetCategoriesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> GetAssetCategories()
        {
            var items = this.context.AssetCategories.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>();
            this.OnAssetCategoriesRead(ref items);

            return items;
        }

        partial void OnAssetCategoriesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> items);

        partial void OnAssetCategoryGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/AssetCategories(CategoryId={CategoryId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> GetAssetCategory(int key)
        {
            var items = this.context.AssetCategories.Where(i => i.CategoryId == key);
            var result = SingleResult.Create(items);

            OnAssetCategoryGet(ref result);

            return result;
        }
        partial void OnAssetCategoryDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        [HttpDelete("/odata/FixedAssetsDB/AssetCategories(CategoryId={CategoryId})")]
        public IActionResult DeleteAssetCategory(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AssetCategories
                    .Where(i => i.CategoryId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAssetCategoryDeleted(item);
                this.context.AssetCategories.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAssetCategoryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetCategoryUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        [HttpPut("/odata/FixedAssetsDB/AssetCategories(CategoryId={CategoryId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAssetCategory(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.CategoryId != key))
                {
                    return BadRequest();
                }
                this.OnAssetCategoryUpdated(item);
                this.context.AssetCategories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetCategories.Where(i => i.CategoryId == key);
                
                this.OnAfterAssetCategoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/AssetCategories(CategoryId={CategoryId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAssetCategory(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AssetCategories.Where(i => i.CategoryId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAssetCategoryUpdated(item);
                this.context.AssetCategories.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetCategories.Where(i => i.CategoryId == key);
                
                this.OnAfterAssetCategoryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetCategoryCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item)
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

                this.OnAssetCategoryCreated(item);
                this.context.AssetCategories.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetCategories.Where(i => i.CategoryId == item.CategoryId);

                

                this.OnAfterAssetCategoryCreated(item);

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
