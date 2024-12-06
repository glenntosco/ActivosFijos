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
    [Route("odata/FixedAssetsDB/Roles")]
    public partial class RolesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public RolesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Role> GetRoles()
        {
            var items = this.context.Roles.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Role>();
            this.OnRolesRead(ref items);

            return items;
        }

        partial void OnRolesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Role> items);

        partial void OnRoleGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Role> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Roles(RoleId={RoleId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Role> GetRole(int key)
        {
            var items = this.context.Roles.Where(i => i.RoleId == key);
            var result = SingleResult.Create(items);

            OnRoleGet(ref result);

            return result;
        }
        partial void OnRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        [HttpDelete("/odata/FixedAssetsDB/Roles(RoleId={RoleId})")]
        public IActionResult DeleteRole(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Roles
                    .Where(i => i.RoleId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnRoleDeleted(item);
                this.context.Roles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        [HttpPut("/odata/FixedAssetsDB/Roles(RoleId={RoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRole(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Role item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.RoleId != key))
                {
                    return BadRequest();
                }
                this.OnRoleUpdated(item);
                this.context.Roles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roles.Where(i => i.RoleId == key);
                
                this.OnAfterRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Roles(RoleId={RoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRole(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Role> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Roles.Where(i => i.RoleId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnRoleUpdated(item);
                this.context.Roles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roles.Where(i => i.RoleId == key);
                
                this.OnAfterRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Role item)
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

                this.OnRoleCreated(item);
                this.context.Roles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Roles.Where(i => i.RoleId == item.RoleId);

                

                this.OnAfterRoleCreated(item);

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
