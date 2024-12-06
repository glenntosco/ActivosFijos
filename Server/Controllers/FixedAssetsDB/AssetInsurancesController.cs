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
    [Route("odata/FixedAssetsDB/AssetInsurances")]
    public partial class AssetInsurancesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public AssetInsurancesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> GetAssetInsurances()
        {
            var items = this.context.AssetInsurances.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>();
            this.OnAssetInsurancesRead(ref items);

            return items;
        }

        partial void OnAssetInsurancesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> items);

        partial void OnAssetInsuranceGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/AssetInsurances(InsuranceId={InsuranceId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> GetAssetInsurance(int key)
        {
            var items = this.context.AssetInsurances.Where(i => i.InsuranceId == key);
            var result = SingleResult.Create(items);

            OnAssetInsuranceGet(ref result);

            return result;
        }
        partial void OnAssetInsuranceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        [HttpDelete("/odata/FixedAssetsDB/AssetInsurances(InsuranceId={InsuranceId})")]
        public IActionResult DeleteAssetInsurance(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.AssetInsurances
                    .Where(i => i.InsuranceId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnAssetInsuranceDeleted(item);
                this.context.AssetInsurances.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAssetInsuranceDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetInsuranceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        [HttpPut("/odata/FixedAssetsDB/AssetInsurances(InsuranceId={InsuranceId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAssetInsurance(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.InsuranceId != key))
                {
                    return BadRequest();
                }
                this.OnAssetInsuranceUpdated(item);
                this.context.AssetInsurances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetInsurances.Where(i => i.InsuranceId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterAssetInsuranceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/AssetInsurances(InsuranceId={InsuranceId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAssetInsurance(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.AssetInsurances.Where(i => i.InsuranceId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnAssetInsuranceUpdated(item);
                this.context.AssetInsurances.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetInsurances.Where(i => i.InsuranceId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");
                this.OnAfterAssetInsuranceUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAssetInsuranceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item)
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

                this.OnAssetInsuranceCreated(item);
                this.context.AssetInsurances.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.AssetInsurances.Where(i => i.InsuranceId == item.InsuranceId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset");

                this.OnAfterAssetInsuranceCreated(item);

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
