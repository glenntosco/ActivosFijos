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
    [Route("odata/FixedAssetsDB/UserRoles")]
    public partial class UserRolesController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public UserRolesController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> GetUserRoles()
        {
            var items = this.context.UserRoles.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>();
            this.OnUserRolesRead(ref items);

            return items;
        }

        partial void OnUserRolesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> items);

        partial void OnUserRoleGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/UserRoles(UserRoleId={UserRoleId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> GetUserRole(int key)
        {
            var items = this.context.UserRoles.Where(i => i.UserRoleId == key);
            var result = SingleResult.Create(items);

            OnUserRoleGet(ref result);

            return result;
        }
        partial void OnUserRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        [HttpDelete("/odata/FixedAssetsDB/UserRoles(UserRoleId={UserRoleId})")]
        public IActionResult DeleteUserRole(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.UserRoles
                    .Where(i => i.UserRoleId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnUserRoleDeleted(item);
                this.context.UserRoles.Remove(item);
                this.context.SaveChanges();
                this.OnAfterUserRoleDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        [HttpPut("/odata/FixedAssetsDB/UserRoles(UserRoleId={UserRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutUserRole(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.UserRoleId != key))
                {
                    return BadRequest();
                }
                this.OnUserRoleUpdated(item);
                this.context.UserRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserRoles.Where(i => i.UserRoleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Role,User");
                this.OnAfterUserRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/UserRoles(UserRoleId={UserRoleId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchUserRole(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.UserRoles.Where(i => i.UserRoleId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnUserRoleUpdated(item);
                this.context.UserRoles.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserRoles.Where(i => i.UserRoleId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Role,User");
                this.OnAfterUserRoleUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnUserRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item)
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

                this.OnUserRoleCreated(item);
                this.context.UserRoles.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.UserRoles.Where(i => i.UserRoleId == item.UserRoleId);

                Request.QueryString = Request.QueryString.Add("$expand", "Role,User");

                this.OnAfterUserRoleCreated(item);

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
