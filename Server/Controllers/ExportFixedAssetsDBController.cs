using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using ActivosFiljos.Server.Data;

namespace ActivosFiljos.Server.Controllers
{
    public partial class ExportFixedAssetsDBController : ExportController
    {
        private readonly FixedAssetsDBContext context;
        private readonly FixedAssetsDBService service;

        public ExportFixedAssetsDBController(FixedAssetsDBContext context, FixedAssetsDBService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/FixedAssetsDB/assetassignments/csv")]
        [HttpGet("/export/FixedAssetsDB/assetassignments/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAssignmentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAssetAssignments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetassignments/excel")]
        [HttpGet("/export/FixedAssetsDB/assetassignments/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAssignmentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAssetAssignments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetattributes/csv")]
        [HttpGet("/export/FixedAssetsDB/assetattributes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAttributesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAssetAttributes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetattributes/excel")]
        [HttpGet("/export/FixedAssetsDB/assetattributes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAttributesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAssetAttributes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetattributevalues/csv")]
        [HttpGet("/export/FixedAssetsDB/assetattributevalues/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAttributeValuesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAssetAttributeValues(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetattributevalues/excel")]
        [HttpGet("/export/FixedAssetsDB/assetattributevalues/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetAttributeValuesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAssetAttributeValues(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetcategories/csv")]
        [HttpGet("/export/FixedAssetsDB/assetcategories/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAssetCategories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetcategories/excel")]
        [HttpGet("/export/FixedAssetsDB/assetcategories/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAssetCategories(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetinsurances/csv")]
        [HttpGet("/export/FixedAssetsDB/assetinsurances/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetInsurancesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAssetInsurances(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/assetinsurances/excel")]
        [HttpGet("/export/FixedAssetsDB/assetinsurances/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAssetInsurancesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAssetInsurances(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/depreciations/csv")]
        [HttpGet("/export/FixedAssetsDB/depreciations/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepreciationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDepreciations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/depreciations/excel")]
        [HttpGet("/export/FixedAssetsDB/depreciations/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDepreciationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDepreciations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/disposalrecords/csv")]
        [HttpGet("/export/FixedAssetsDB/disposalrecords/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDisposalRecordsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDisposalRecords(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/disposalrecords/excel")]
        [HttpGet("/export/FixedAssetsDB/disposalrecords/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDisposalRecordsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDisposalRecords(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/documents/csv")]
        [HttpGet("/export/FixedAssetsDB/documents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDocuments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/documents/excel")]
        [HttpGet("/export/FixedAssetsDB/documents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDocuments(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/fixedassets/csv")]
        [HttpGet("/export/FixedAssetsDB/fixedassets/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFixedAssetsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFixedAssets(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/fixedassets/excel")]
        [HttpGet("/export/FixedAssetsDB/fixedassets/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFixedAssetsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFixedAssets(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/locations/csv")]
        [HttpGet("/export/FixedAssetsDB/locations/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLocationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLocations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/locations/excel")]
        [HttpGet("/export/FixedAssetsDB/locations/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLocationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLocations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/maintenancerecords/csv")]
        [HttpGet("/export/FixedAssetsDB/maintenancerecords/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMaintenanceRecordsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMaintenanceRecords(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/maintenancerecords/excel")]
        [HttpGet("/export/FixedAssetsDB/maintenancerecords/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMaintenanceRecordsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMaintenanceRecords(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/notifications/csv")]
        [HttpGet("/export/FixedAssetsDB/notifications/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotificationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNotifications(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/notifications/excel")]
        [HttpGet("/export/FixedAssetsDB/notifications/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotificationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNotifications(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/roles/csv")]
        [HttpGet("/export/FixedAssetsDB/roles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/roles/excel")]
        [HttpGet("/export/FixedAssetsDB/roles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/scheduledmaintenances/csv")]
        [HttpGet("/export/FixedAssetsDB/scheduledmaintenances/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportScheduledMaintenancesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetScheduledMaintenances(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/scheduledmaintenances/excel")]
        [HttpGet("/export/FixedAssetsDB/scheduledmaintenances/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportScheduledMaintenancesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetScheduledMaintenances(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/userroles/csv")]
        [HttpGet("/export/FixedAssetsDB/userroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUserRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/userroles/excel")]
        [HttpGet("/export/FixedAssetsDB/userroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUserRoles(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/users/csv")]
        [HttpGet("/export/FixedAssetsDB/users/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/users/excel")]
        [HttpGet("/export/FixedAssetsDB/users/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/statuses/csv")]
        [HttpGet("/export/FixedAssetsDB/statuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetStatuses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/FixedAssetsDB/statuses/excel")]
        [HttpGet("/export/FixedAssetsDB/statuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetStatuses(), Request.Query, false), fileName);
        }
    }
}
