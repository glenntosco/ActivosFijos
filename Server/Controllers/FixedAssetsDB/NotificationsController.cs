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
    [Route("odata/FixedAssetsDB/Notifications")]
    public partial class NotificationsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public NotificationsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> GetNotifications()
        {
            var items = this.context.Notifications.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>();
            this.OnNotificationsRead(ref items);

            return items;
        }

        partial void OnNotificationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> items);

        partial void OnNotificationGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Notifications(NotificationId={NotificationId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> GetNotification(int key)
        {
            var items = this.context.Notifications.Where(i => i.NotificationId == key);
            var result = SingleResult.Create(items);

            OnNotificationGet(ref result);

            return result;
        }
        partial void OnNotificationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        [HttpDelete("/odata/FixedAssetsDB/Notifications(NotificationId={NotificationId})")]
        public IActionResult DeleteNotification(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Notifications
                    .Where(i => i.NotificationId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnNotificationDeleted(item);
                this.context.Notifications.Remove(item);
                this.context.SaveChanges();
                this.OnAfterNotificationDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnNotificationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        [HttpPut("/odata/FixedAssetsDB/Notifications(NotificationId={NotificationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutNotification(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Notification item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.NotificationId != key))
                {
                    return BadRequest();
                }
                this.OnNotificationUpdated(item);
                this.context.Notifications.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Notifications.Where(i => i.NotificationId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterNotificationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Notifications(NotificationId={NotificationId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchNotification(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Notifications.Where(i => i.NotificationId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnNotificationUpdated(item);
                this.context.Notifications.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Notifications.Where(i => i.NotificationId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterNotificationUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnNotificationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Notification item)
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

                this.OnNotificationCreated(item);
                this.context.Notifications.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Notifications.Where(i => i.NotificationId == item.NotificationId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");

                this.OnAfterNotificationCreated(item);

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
