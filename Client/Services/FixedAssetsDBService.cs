
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Radzen;

namespace ActivosFiljos.Client
{
    public partial class FixedAssetsDBService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;

        public FixedAssetsDBService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/FixedAssetsDB/");
        }


        public async System.Threading.Tasks.Task ExportAssetAssignmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetassignments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetassignments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAssetAssignmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetassignments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetassignments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAssetAssignments(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>> GetAssetAssignments(Query query)
        {
            return await GetAssetAssignments(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>> GetAssetAssignments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AssetAssignments");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAssignments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>>(response);
        }

        partial void OnCreateAssetAssignment(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> CreateAssetAssignment(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment assetAssignment = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment))
        {
            var uri = new Uri(baseUri, $"AssetAssignments");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAssignment), Encoding.UTF8, "application/json");

            OnCreateAssetAssignment(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>(response);
        }

        partial void OnDeleteAssetAssignment(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAssetAssignment(int assignmentId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAssignments({assignmentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAssetAssignment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAssetAssignmentByAssignmentId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> GetAssetAssignmentByAssignmentId(string expand = default(string), int assignmentId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAssignments({assignmentId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAssignmentByAssignmentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>(response);
        }

        partial void OnUpdateAssetAssignment(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAssetAssignment(int assignmentId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment assetAssignment = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment))
        {
            var uri = new Uri(baseUri, $"AssetAssignments({assignmentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAssignment), Encoding.UTF8, "application/json");

            OnUpdateAssetAssignment(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAssetAttributesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAssetAttributesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAssetAttributes(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>> GetAssetAttributes(Query query)
        {
            return await GetAssetAttributes(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>> GetAssetAttributes(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AssetAttributes");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAttributes(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>>(response);
        }

        partial void OnCreateAssetAttribute(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> CreateAssetAttribute(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetAttribute = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute))
        {
            var uri = new Uri(baseUri, $"AssetAttributes");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAttribute), Encoding.UTF8, "application/json");

            OnCreateAssetAttribute(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>(response);
        }

        partial void OnDeleteAssetAttribute(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAssetAttribute(int attributeId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAttributes({attributeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAssetAttribute(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAssetAttributeByAttributeId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> GetAssetAttributeByAttributeId(string expand = default(string), int attributeId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAttributes({attributeId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAttributeByAttributeId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>(response);
        }

        partial void OnUpdateAssetAttribute(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAssetAttribute(int attributeId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetAttribute = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute))
        {
            var uri = new Uri(baseUri, $"AssetAttributes({attributeId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAttribute), Encoding.UTF8, "application/json");

            OnUpdateAssetAttribute(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAssetAttributeValuesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributevalues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributevalues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAssetAttributeValuesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributevalues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributevalues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAssetAttributeValues(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>> GetAssetAttributeValues(Query query)
        {
            return await GetAssetAttributeValues(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>> GetAssetAttributeValues(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AssetAttributeValues");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAttributeValues(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>>(response);
        }

        partial void OnCreateAssetAttributeValue(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> CreateAssetAttributeValue(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue assetAttributeValue = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue))
        {
            var uri = new Uri(baseUri, $"AssetAttributeValues");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAttributeValue), Encoding.UTF8, "application/json");

            OnCreateAssetAttributeValue(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>(response);
        }

        partial void OnDeleteAssetAttributeValue(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAssetAttributeValue(int attributeValueId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAttributeValues({attributeValueId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAssetAttributeValue(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAssetAttributeValueByAttributeValueId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> GetAssetAttributeValueByAttributeValueId(string expand = default(string), int attributeValueId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetAttributeValues({attributeValueId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetAttributeValueByAttributeValueId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>(response);
        }

        partial void OnUpdateAssetAttributeValue(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAssetAttributeValue(int attributeValueId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue assetAttributeValue = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue))
        {
            var uri = new Uri(baseUri, $"AssetAttributeValues({attributeValueId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetAttributeValue), Encoding.UTF8, "application/json");

            OnUpdateAssetAttributeValue(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAssetCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAssetCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAssetCategories(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>> GetAssetCategories(Query query)
        {
            return await GetAssetCategories(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>> GetAssetCategories(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AssetCategories");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetCategories(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>>(response);
        }

        partial void OnCreateAssetCategory(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> CreateAssetCategory(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetCategory = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory))
        {
            var uri = new Uri(baseUri, $"AssetCategories");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetCategory), Encoding.UTF8, "application/json");

            OnCreateAssetCategory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>(response);
        }

        partial void OnDeleteAssetCategory(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAssetCategory(int categoryId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetCategories({categoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAssetCategory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAssetCategoryByCategoryId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> GetAssetCategoryByCategoryId(string expand = default(string), int categoryId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetCategories({categoryId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetCategoryByCategoryId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>(response);
        }

        partial void OnUpdateAssetCategory(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAssetCategory(int categoryId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetCategory = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory))
        {
            var uri = new Uri(baseUri, $"AssetCategories({categoryId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetCategory), Encoding.UTF8, "application/json");

            OnUpdateAssetCategory(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportAssetInsurancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetinsurances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetinsurances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAssetInsurancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetinsurances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetinsurances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetAssetInsurances(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>> GetAssetInsurances(Query query)
        {
            return await GetAssetInsurances(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>> GetAssetInsurances(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"AssetInsurances");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetInsurances(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>>(response);
        }

        partial void OnCreateAssetInsurance(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> CreateAssetInsurance(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance assetInsurance = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance))
        {
            var uri = new Uri(baseUri, $"AssetInsurances");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetInsurance), Encoding.UTF8, "application/json");

            OnCreateAssetInsurance(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>(response);
        }

        partial void OnDeleteAssetInsurance(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteAssetInsurance(int insuranceId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetInsurances({insuranceId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAssetInsurance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetAssetInsuranceByInsuranceId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> GetAssetInsuranceByInsuranceId(string expand = default(string), int insuranceId = default(int))
        {
            var uri = new Uri(baseUri, $"AssetInsurances({insuranceId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAssetInsuranceByInsuranceId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>(response);
        }

        partial void OnUpdateAssetInsurance(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateAssetInsurance(int insuranceId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance assetInsurance = default(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance))
        {
            var uri = new Uri(baseUri, $"AssetInsurances({insuranceId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(assetInsurance), Encoding.UTF8, "application/json");

            OnUpdateAssetInsurance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDepreciationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/depreciations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/depreciations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDepreciationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/depreciations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/depreciations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDepreciations(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>> GetDepreciations(Query query)
        {
            return await GetDepreciations(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>> GetDepreciations(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Depreciations");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepreciations(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>>(response);
        }

        partial void OnCreateDepreciation(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> CreateDepreciation(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation depreciation = default(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation))
        {
            var uri = new Uri(baseUri, $"Depreciations");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depreciation), Encoding.UTF8, "application/json");

            OnCreateDepreciation(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>(response);
        }

        partial void OnDeleteDepreciation(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDepreciation(int depreciationId = default(int))
        {
            var uri = new Uri(baseUri, $"Depreciations({depreciationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDepreciation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDepreciationByDepreciationId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> GetDepreciationByDepreciationId(string expand = default(string), int depreciationId = default(int))
        {
            var uri = new Uri(baseUri, $"Depreciations({depreciationId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDepreciationByDepreciationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>(response);
        }

        partial void OnUpdateDepreciation(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDepreciation(int depreciationId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation depreciation = default(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation))
        {
            var uri = new Uri(baseUri, $"Depreciations({depreciationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(depreciation), Encoding.UTF8, "application/json");

            OnUpdateDepreciation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDisposalRecordsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/disposalrecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/disposalrecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDisposalRecordsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/disposalrecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/disposalrecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDisposalRecords(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>> GetDisposalRecords(Query query)
        {
            return await GetDisposalRecords(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>> GetDisposalRecords(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"DisposalRecords");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDisposalRecords(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>>(response);
        }

        partial void OnCreateDisposalRecord(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> CreateDisposalRecord(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord disposalRecord = default(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord))
        {
            var uri = new Uri(baseUri, $"DisposalRecords");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(disposalRecord), Encoding.UTF8, "application/json");

            OnCreateDisposalRecord(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>(response);
        }

        partial void OnDeleteDisposalRecord(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDisposalRecord(int disposalId = default(int))
        {
            var uri = new Uri(baseUri, $"DisposalRecords({disposalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDisposalRecord(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDisposalRecordByDisposalId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> GetDisposalRecordByDisposalId(string expand = default(string), int disposalId = default(int))
        {
            var uri = new Uri(baseUri, $"DisposalRecords({disposalId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDisposalRecordByDisposalId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>(response);
        }

        partial void OnUpdateDisposalRecord(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDisposalRecord(int disposalId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord disposalRecord = default(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord))
        {
            var uri = new Uri(baseUri, $"DisposalRecords({disposalId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(disposalRecord), Encoding.UTF8, "application/json");

            OnUpdateDisposalRecord(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportDocumentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportDocumentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetDocuments(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Document>> GetDocuments(Query query)
        {
            return await GetDocuments(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Document>> GetDocuments(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Documents");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocuments(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Document>>(response);
        }

        partial void OnCreateDocument(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> CreateDocument(ActivosFiljos.Server.Models.FixedAssetsDB.Document document = default(ActivosFiljos.Server.Models.FixedAssetsDB.Document))
        {
            var uri = new Uri(baseUri, $"Documents");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(document), Encoding.UTF8, "application/json");

            OnCreateDocument(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Document>(response);
        }

        partial void OnDeleteDocument(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteDocument(int documentId = default(int))
        {
            var uri = new Uri(baseUri, $"Documents({documentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetDocumentByDocumentId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> GetDocumentByDocumentId(string expand = default(string), int documentId = default(int))
        {
            var uri = new Uri(baseUri, $"Documents({documentId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetDocumentByDocumentId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Document>(response);
        }

        partial void OnUpdateDocument(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateDocument(int documentId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Document document = default(ActivosFiljos.Server.Models.FixedAssetsDB.Document))
        {
            var uri = new Uri(baseUri, $"Documents({documentId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(document), Encoding.UTF8, "application/json");

            OnUpdateDocument(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportFixedAssetsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/fixedassets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/fixedassets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFixedAssetsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/fixedassets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/fixedassets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetFixedAssets(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>> GetFixedAssets(Query query)
        {
            return await GetFixedAssets(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>> GetFixedAssets(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FixedAssets");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFixedAssets(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>>(response);
        }

        partial void OnCreateFixedAsset(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> CreateFixedAsset(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedAsset = default(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset))
        {
            var uri = new Uri(baseUri, $"FixedAssets");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(fixedAsset), Encoding.UTF8, "application/json");

            OnCreateFixedAsset(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>(response);
        }

        partial void OnDeleteFixedAsset(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteFixedAsset(int assetId = default(int))
        {
            var uri = new Uri(baseUri, $"FixedAssets({assetId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFixedAsset(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetFixedAssetByAssetId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> GetFixedAssetByAssetId(string expand = default(string), int assetId = default(int))
        {
            var uri = new Uri(baseUri, $"FixedAssets({assetId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFixedAssetByAssetId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>(response);
        }

        partial void OnUpdateFixedAsset(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateFixedAsset(int assetId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedAsset = default(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset))
        {
            var uri = new Uri(baseUri, $"FixedAssets({assetId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(fixedAsset), Encoding.UTF8, "application/json");

            OnUpdateFixedAsset(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportLocationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportLocationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetLocations(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Location>> GetLocations(Query query)
        {
            return await GetLocations(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Location>> GetLocations(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Locations");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocations(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Location>>(response);
        }

        partial void OnCreateLocation(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> CreateLocation(ActivosFiljos.Server.Models.FixedAssetsDB.Location location = default(ActivosFiljos.Server.Models.FixedAssetsDB.Location))
        {
            var uri = new Uri(baseUri, $"Locations");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnCreateLocation(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Location>(response);
        }

        partial void OnDeleteLocation(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteLocation(int locationId = default(int))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetLocationByLocationId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> GetLocationByLocationId(string expand = default(string), int locationId = default(int))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetLocationByLocationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Location>(response);
        }

        partial void OnUpdateLocation(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateLocation(int locationId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Location location = default(ActivosFiljos.Server.Models.FixedAssetsDB.Location))
        {
            var uri = new Uri(baseUri, $"Locations({locationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(location), Encoding.UTF8, "application/json");

            OnUpdateLocation(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportMaintenanceRecordsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/maintenancerecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/maintenancerecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportMaintenanceRecordsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/maintenancerecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/maintenancerecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetMaintenanceRecords(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>> GetMaintenanceRecords(Query query)
        {
            return await GetMaintenanceRecords(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>> GetMaintenanceRecords(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"MaintenanceRecords");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMaintenanceRecords(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>>(response);
        }

        partial void OnCreateMaintenanceRecord(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> CreateMaintenanceRecord(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord maintenanceRecord = default(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord))
        {
            var uri = new Uri(baseUri, $"MaintenanceRecords");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(maintenanceRecord), Encoding.UTF8, "application/json");

            OnCreateMaintenanceRecord(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>(response);
        }

        partial void OnDeleteMaintenanceRecord(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteMaintenanceRecord(int maintenanceId = default(int))
        {
            var uri = new Uri(baseUri, $"MaintenanceRecords({maintenanceId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteMaintenanceRecord(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetMaintenanceRecordByMaintenanceId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> GetMaintenanceRecordByMaintenanceId(string expand = default(string), int maintenanceId = default(int))
        {
            var uri = new Uri(baseUri, $"MaintenanceRecords({maintenanceId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetMaintenanceRecordByMaintenanceId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>(response);
        }

        partial void OnUpdateMaintenanceRecord(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateMaintenanceRecord(int maintenanceId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord maintenanceRecord = default(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord))
        {
            var uri = new Uri(baseUri, $"MaintenanceRecords({maintenanceId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(maintenanceRecord), Encoding.UTF8, "application/json");

            OnUpdateMaintenanceRecord(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportNotificationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/notifications/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/notifications/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportNotificationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/notifications/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/notifications/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetNotifications(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>> GetNotifications(Query query)
        {
            return await GetNotifications(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>> GetNotifications(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Notifications");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetNotifications(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>>(response);
        }

        partial void OnCreateNotification(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> CreateNotification(ActivosFiljos.Server.Models.FixedAssetsDB.Notification notification = default(ActivosFiljos.Server.Models.FixedAssetsDB.Notification))
        {
            var uri = new Uri(baseUri, $"Notifications");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(notification), Encoding.UTF8, "application/json");

            OnCreateNotification(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>(response);
        }

        partial void OnDeleteNotification(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteNotification(int notificationId = default(int))
        {
            var uri = new Uri(baseUri, $"Notifications({notificationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteNotification(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetNotificationByNotificationId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> GetNotificationByNotificationId(string expand = default(string), int notificationId = default(int))
        {
            var uri = new Uri(baseUri, $"Notifications({notificationId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetNotificationByNotificationId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>(response);
        }

        partial void OnUpdateNotification(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateNotification(int notificationId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Notification notification = default(ActivosFiljos.Server.Models.FixedAssetsDB.Notification))
        {
            var uri = new Uri(baseUri, $"Notifications({notificationId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(notification), Encoding.UTF8, "application/json");

            OnUpdateNotification(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Role>> GetRoles(Query query)
        {
            return await GetRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Role>> GetRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Roles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Role>>(response);
        }

        partial void OnCreateRole(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> CreateRole(ActivosFiljos.Server.Models.FixedAssetsDB.Role role = default(ActivosFiljos.Server.Models.FixedAssetsDB.Role))
        {
            var uri = new Uri(baseUri, $"Roles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

            OnCreateRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Role>(response);
        }

        partial void OnDeleteRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteRole(int roleId = default(int))
        {
            var uri = new Uri(baseUri, $"Roles({roleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetRoleByRoleId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> GetRoleByRoleId(string expand = default(string), int roleId = default(int))
        {
            var uri = new Uri(baseUri, $"Roles({roleId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetRoleByRoleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Role>(response);
        }

        partial void OnUpdateRole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateRole(int roleId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Role role = default(ActivosFiljos.Server.Models.FixedAssetsDB.Role))
        {
            var uri = new Uri(baseUri, $"Roles({roleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

            OnUpdateRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportScheduledMaintenancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/scheduledmaintenances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/scheduledmaintenances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportScheduledMaintenancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/scheduledmaintenances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/scheduledmaintenances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetScheduledMaintenances(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>> GetScheduledMaintenances(Query query)
        {
            return await GetScheduledMaintenances(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>> GetScheduledMaintenances(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"ScheduledMaintenances");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetScheduledMaintenances(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>>(response);
        }

        partial void OnCreateScheduledMaintenance(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> CreateScheduledMaintenance(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance scheduledMaintenance = default(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance))
        {
            var uri = new Uri(baseUri, $"ScheduledMaintenances");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(scheduledMaintenance), Encoding.UTF8, "application/json");

            OnCreateScheduledMaintenance(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>(response);
        }

        partial void OnDeleteScheduledMaintenance(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteScheduledMaintenance(int scheduleId = default(int))
        {
            var uri = new Uri(baseUri, $"ScheduledMaintenances({scheduleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteScheduledMaintenance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetScheduledMaintenanceByScheduleId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> GetScheduledMaintenanceByScheduleId(string expand = default(string), int scheduleId = default(int))
        {
            var uri = new Uri(baseUri, $"ScheduledMaintenances({scheduleId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetScheduledMaintenanceByScheduleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>(response);
        }

        partial void OnUpdateScheduledMaintenance(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateScheduledMaintenance(int scheduleId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance scheduledMaintenance = default(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance))
        {
            var uri = new Uri(baseUri, $"ScheduledMaintenances({scheduleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(scheduledMaintenance), Encoding.UTF8, "application/json");

            OnUpdateScheduledMaintenance(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUserRoles(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>> GetUserRoles(Query query)
        {
            return await GetUserRoles(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>> GetUserRoles(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"UserRoles");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserRoles(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>>(response);
        }

        partial void OnCreateUserRole(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> CreateUserRole(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole userRole = default(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole))
        {
            var uri = new Uri(baseUri, $"UserRoles");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(userRole), Encoding.UTF8, "application/json");

            OnCreateUserRole(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>(response);
        }

        partial void OnDeleteUserRole(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUserRole(int userRoleId = default(int))
        {
            var uri = new Uri(baseUri, $"UserRoles({userRoleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUserRoleByUserRoleId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> GetUserRoleByUserRoleId(string expand = default(string), int userRoleId = default(int))
        {
            var uri = new Uri(baseUri, $"UserRoles({userRoleId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserRoleByUserRoleId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>(response);
        }

        partial void OnUpdateUserRole(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUserRole(int userRoleId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.UserRole userRole = default(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole))
        {
            var uri = new Uri(baseUri, $"UserRoles({userRoleId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(userRole), Encoding.UTF8, "application/json");

            OnUpdateUserRole(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetUsers(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.User>> GetUsers(Query query)
        {
            return await GetUsers(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.User>> GetUsers(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Users");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUsers(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.User>>(response);
        }

        partial void OnCreateUser(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> CreateUser(ActivosFiljos.Server.Models.FixedAssetsDB.User user = default(ActivosFiljos.Server.Models.FixedAssetsDB.User))
        {
            var uri = new Uri(baseUri, $"Users");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnCreateUser(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.User>(response);
        }

        partial void OnDeleteUser(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteUser(int userId = default(int))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetUserByUserId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> GetUserByUserId(string expand = default(string), int userId = default(int))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetUserByUserId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.User>(response);
        }

        partial void OnUpdateUser(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateUser(int userId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.User user = default(ActivosFiljos.Server.Models.FixedAssetsDB.User))
        {
            var uri = new Uri(baseUri, $"Users({userId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            OnUpdateUser(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        public async System.Threading.Tasks.Task ExportStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/statuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/statuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/statuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/statuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGetStatuses(HttpRequestMessage requestMessage);

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Status>> GetStatuses(Query query)
        {
            return await GetStatuses(filter:$"{query.Filter}", orderby:$"{query.OrderBy}", top:query.Top, skip:query.Skip, count:query.Top != null && query.Skip != null);
        }

        public async Task<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Status>> GetStatuses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Statuses");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatuses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<ActivosFiljos.Server.Models.FixedAssetsDB.Status>>(response);
        }

        partial void OnCreateStatus(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> CreateStatus(ActivosFiljos.Server.Models.FixedAssetsDB.Status status = default(ActivosFiljos.Server.Models.FixedAssetsDB.Status))
        {
            var uri = new Uri(baseUri, $"Statuses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(status), Encoding.UTF8, "application/json");

            OnCreateStatus(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Status>(response);
        }

        partial void OnDeleteStatus(HttpRequestMessage requestMessage);

        public async Task<HttpResponseMessage> DeleteStatus(int statusId = default(int))
        {
            var uri = new Uri(baseUri, $"Statuses({statusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }

        partial void OnGetStatusByStatusId(HttpRequestMessage requestMessage);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> GetStatusByStatusId(string expand = default(string), int statusId = default(int))
        {
            var uri = new Uri(baseUri, $"Statuses({statusId})");

            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetStatusByStatusId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<ActivosFiljos.Server.Models.FixedAssetsDB.Status>(response);
        }

        partial void OnUpdateStatus(HttpRequestMessage requestMessage);
        
        public async Task<HttpResponseMessage> UpdateStatus(int statusId = default(int), ActivosFiljos.Server.Models.FixedAssetsDB.Status status = default(ActivosFiljos.Server.Models.FixedAssetsDB.Status))
        {
            var uri = new Uri(baseUri, $"Statuses({statusId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(status), Encoding.UTF8, "application/json");

            OnUpdateStatus(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}