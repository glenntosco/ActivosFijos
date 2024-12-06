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
    public partial class AddUserRole
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
            userRole = new ActivosFiljos.Server.Models.FixedAssetsDB.UserRole();
        }
        protected bool errorVisible;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.UserRole userRole;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.User> usersForUserId;

        protected IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Role> rolesForRoleId;


        protected int usersForUserIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.User usersForUserIdValue;
        protected async Task usersForUserIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetUsers(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(FirstName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                usersForUserId = result.Value.AsODataEnumerable();
                usersForUserIdCount = result.Count;

                if (!object.Equals(userRole.UserId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetUsers(filter: $"UserId eq {userRole.UserId}");
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

        protected int rolesForRoleIdCount;
        protected ActivosFiljos.Server.Models.FixedAssetsDB.Role rolesForRoleIdValue;
        protected async Task rolesForRoleIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await FixedAssetsDBService.GetRoles(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(RoleName, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                rolesForRoleId = result.Value.AsODataEnumerable();
                rolesForRoleIdCount = result.Count;

                if (!object.Equals(userRole.RoleId, null))
                {
                    var valueResult = await FixedAssetsDBService.GetRoles(filter: $"RoleId eq {userRole.RoleId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        rolesForRoleIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Role" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                var result = await FixedAssetsDBService.CreateUserRole(userRole);
                DialogService.Close(userRole);
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