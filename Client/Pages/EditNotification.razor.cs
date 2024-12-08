using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace ActivosFiljos.Client.Pages
{
    public partial class EditNotification
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public FixedAssetsDBService FixedAssetsDBService { get; set; }

        [Parameter]
        public int NotificationId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            notification = await FixedAssetsDBService.GetNotificationByNotificationId(notificationId:NotificationId);
        }
        protected bool errorVisible;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.Notification notification;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.User> usersForUserId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> fixedAssetsForAssetId;


        protected int usersForUserIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.User usersForUserIdValue;
        protected async Task usersForUserIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetUsers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(FirstName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                usersForUserId = result.Value.AsODataEnumerable();
                usersForUserIdCount = result.Count;

                if (!object.Equals(notification.UserId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetUsers(filter: $"UserId eq {notification.UserId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        usersForUserIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load User" });
            }
        }

        protected int fixedAssetsForAssetIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedAssetsForAssetIdValue;
        protected async Task fixedAssetsForAssetIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetFixedAssets(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AssetTag, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                fixedAssetsForAssetId = result.Value.AsODataEnumerable();
                fixedAssetsForAssetIdCount = result.Count;

                if (!object.Equals(notification.AssetId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetFixedAssets(filter: $"AssetId eq {notification.AssetId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        fixedAssetsForAssetIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load FixedAsset" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await FixedAssetsDBService.UpdateNotification(notificationId:NotificationId, notification);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(notification);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;

        [Inject]
        protected SecurityService Security { get; set; }


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            notification = await FixedAssetsDBService.GetNotificationByNotificationId(notificationId:NotificationId);
        }
    }
}