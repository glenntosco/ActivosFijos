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
    public partial class EditFixedAsset
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
        public int AssetId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            fixedAsset = await FixedAssetsDBService.GetFixedAssetByAssetId(assetId:AssetId);
        }
        protected bool errorVisible;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedAsset;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> assetCategoriesForCategoryId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Location> locationsForLocationId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.User> usersForUserId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Status> statusesForStatusId;


        protected int assetCategoriesForCategoryIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetCategoriesForCategoryIdValue;
        protected async Task assetCategoriesForCategoryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetAssetCategories(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(CategoryName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                assetCategoriesForCategoryId = result.Value.AsODataEnumerable();
                assetCategoriesForCategoryIdCount = result.Count;

                if (!object.Equals(fixedAsset.CategoryId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetAssetCategories(filter: $"CategoryId eq {fixedAsset.CategoryId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        assetCategoriesForCategoryIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AssetCategory" });
            }
        }

        protected int locationsForLocationIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.Location locationsForLocationIdValue;
        protected async Task locationsForLocationIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetLocations(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(LocationName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                locationsForLocationId = result.Value.AsODataEnumerable();
                locationsForLocationIdCount = result.Count;

                if (!object.Equals(fixedAsset.LocationId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetLocations(filter: $"LocationId eq {fixedAsset.LocationId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        locationsForLocationIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Location" });
            }
        }

        protected int usersForUserIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.User usersForUserIdValue;
        protected async Task usersForUserIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetUsers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(FirstName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                usersForUserId = result.Value.AsODataEnumerable();
                usersForUserIdCount = result.Count;

                if (!object.Equals(fixedAsset.UserId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetUsers(filter: $"UserId eq {fixedAsset.UserId}");
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

        protected int statusesForStatusIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.Status statusesForStatusIdValue;
        protected async Task statusesForStatusIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetStatuses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(StatusName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                statusesForStatusId = result.Value.AsODataEnumerable();
                statusesForStatusIdCount = result.Count;

                if (!object.Equals(fixedAsset.StatusId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetStatuses(filter: $"StatusId eq {fixedAsset.StatusId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        statusesForStatusIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Status" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await FixedAssetsDBService.UpdateFixedAsset(assetId:AssetId, fixedAsset);
                if (result.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                {
                     hasChanges = true;
                     canEdit = false;
                     return;
                }
                DialogService.Close(fixedAsset);
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


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
            hasChanges = false;
            canEdit = true;

            fixedAsset = await FixedAssetsDBService.GetFixedAssetByAssetId(assetId:AssetId);
        }
    }
}