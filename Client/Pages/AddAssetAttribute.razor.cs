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
    public partial class AddAssetAttribute
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
            assetAttribute = new ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute();
        }
        protected bool errorVisible;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetAttribute;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> assetCategoriesForCategoryId;


        protected int assetCategoriesForCategoryIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetCategoriesForCategoryIdValue;
        protected async Task assetCategoriesForCategoryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetAssetCategories(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(CategoryName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                assetCategoriesForCategoryId = result.Value.AsODataEnumerable();
                assetCategoriesForCategoryIdCount = result.Count;

                if (!object.Equals(assetAttribute.CategoryId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetAssetCategories(filter: $"CategoryId eq {assetAttribute.CategoryId}");
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
        protected async Task FormSubmit()
        {
            try
            {
                var result = await FixedAssetsDBService.CreateAssetAttribute(assetAttribute);
                DialogService.Close(assetAttribute);
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
    }
}