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
    [Route("odata/FixedAssetsDB/Locations")]
    public partial class LocationsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public LocationsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Location> GetLocations()
        {
            var items = this.context.Locations.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Location>();
            this.OnLocationsRead(ref items);

            return items;
        }

        partial void OnLocationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Location> items);

        partial void OnLocationGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Location> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Locations(LocationId={LocationId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Location> GetLocation(int key)
        {
            var items = this.context.Locations.Where(i => i.LocationId == key);
            var result = SingleResult.Create(items);

            OnLocationGet(ref result);

            return result;
        }
        partial void OnLocationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        [HttpDelete("/odata/FixedAssetsDB/Locations(LocationId={LocationId})")]
        public IActionResult DeleteLocation(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Locations
                    .Where(i => i.LocationId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnLocationDeleted(item);
                this.context.Locations.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLocationDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLocationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        [HttpPut("/odata/FixedAssetsDB/Locations(LocationId={LocationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLocation(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Location item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.LocationId != key))
                {
                    return BadRequest();
                }
                this.OnLocationUpdated(item);
                this.context.Locations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.LocationId == key);
                
                this.OnAfterLocationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Locations(LocationId={LocationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLocation(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Location> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Locations.Where(i => i.LocationId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnLocationUpdated(item);
                this.context.Locations.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.LocationId == key);
                
                this.OnAfterLocationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLocationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Location item)
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

                this.OnLocationCreated(item);
                this.context.Locations.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Locations.Where(i => i.LocationId == item.LocationId);

                

                this.OnAfterLocationCreated(item);

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
