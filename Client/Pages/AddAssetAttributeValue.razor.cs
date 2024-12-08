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
    public partial class AddAssetAttributeValue
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

        protected override async Task OnInitializedAsync()
        {
            assetAttributeValue = new ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue();
        }
        protected bool errorVisible;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue assetAttributeValue;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> fixedAssetsForAssetId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> assetAttributesForAttributeId;


        protected int fixedAssetsForAssetIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedAssetsForAssetIdValue;
        protected async Task fixedAssetsForAssetIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetFixedAssets(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AssetTag, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                fixedAssetsForAssetId = result.Value.AsODataEnumerable();
                fixedAssetsForAssetIdCount = result.Count;

                if (!object.Equals(assetAttributeValue.AssetId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetFixedAssets(filter: $"AssetId eq {assetAttributeValue.AssetId}");
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

        protected int assetAttributesForAttributeIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetAttributesForAttributeIdValue;
        protected async Task assetAttributesForAttributeIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetAssetAttributes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(AttributeName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                assetAttributesForAttributeId = result.Value.AsODataEnumerable();
                assetAttributesForAttributeIdCount = result.Count;

                if (!object.Equals(assetAttributeValue.AttributeId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetAssetAttributes(filter: $"AttributeId eq {assetAttributeValue.AttributeId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        assetAttributesForAttributeIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load AssetAttribute" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await FixedAssetsDBService.CreateAssetAttributeValue(assetAttributeValue);
                DialogService.Close(assetAttributeValue);
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
    }
}