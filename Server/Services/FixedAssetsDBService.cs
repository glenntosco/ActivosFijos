using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Globalization;

using ActivosFiljos.Server.Data;

namespace ActivosFiljos.Server
{
    public partial class FixedAssetsDBService
    {
        FixedAssetsDBContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly FixedAssetsDBContext context;
        private readonly NavigationManager navigationManager;

        public FixedAssetsDBService(FixedAssetsDBContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAssetAssignmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetassignments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetassignments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAssetAssignmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetassignments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetassignments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAssetAssignmentsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>> GetAssetAssignments(Query query = null)
        {
            var items = Context.AssetAssignments.AsQueryable();

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAssetAssignmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAssetAssignmentGet(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnGetAssetAssignmentByAssignmentId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> GetAssetAssignmentByAssignmentId(int assignmentid)
        {
            var items = Context.AssetAssignments
                              .AsNoTracking()
                              .Where(i => i.AssignmentId == assignmentid);

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);
 
            OnGetAssetAssignmentByAssignmentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAssetAssignmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAssetAssignmentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> CreateAssetAssignment(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment assetassignment)
        {
            OnAssetAssignmentCreated(assetassignment);

            var existingItem = Context.AssetAssignments
                              .Where(i => i.AssignmentId == assetassignment.AssignmentId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AssetAssignments.Add(assetassignment);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(assetassignment).State = EntityState.Detached;
                throw;
            }

            OnAfterAssetAssignmentCreated(assetassignment);

            return assetassignment;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> CancelAssetAssignmentChanges(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAssetAssignmentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> UpdateAssetAssignment(int assignmentid, ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment assetassignment)
        {
            OnAssetAssignmentUpdated(assetassignment);

            var itemToUpdate = Context.AssetAssignments
                              .Where(i => i.AssignmentId == assetassignment.AssignmentId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(assetassignment);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAssetAssignmentUpdated(assetassignment);

            return assetassignment;
        }

        partial void OnAssetAssignmentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);
        partial void OnAfterAssetAssignmentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> DeleteAssetAssignment(int assignmentid)
        {
            var itemToDelete = Context.AssetAssignments
                              .Where(i => i.AssignmentId == assignmentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAssetAssignmentDeleted(itemToDelete);


            Context.AssetAssignments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAssetAssignmentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAssetAttributesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAssetAttributesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAssetAttributesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>> GetAssetAttributes(Query query = null)
        {
            var items = Context.AssetAttributes.AsQueryable();

            items = items.Include(i => i.AssetCategory);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAssetAttributesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAssetAttributeGet(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnGetAssetAttributeByAttributeId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> GetAssetAttributeByAttributeId(int attributeid)
        {
            var items = Context.AssetAttributes
                              .AsNoTracking()
                              .Where(i => i.AttributeId == attributeid);

            items = items.Include(i => i.AssetCategory);
 
            OnGetAssetAttributeByAttributeId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAssetAttributeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAssetAttributeCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> CreateAssetAttribute(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetattribute)
        {
            OnAssetAttributeCreated(assetattribute);

            var existingItem = Context.AssetAttributes
                              .Where(i => i.AttributeId == assetattribute.AttributeId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AssetAttributes.Add(assetattribute);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(assetattribute).State = EntityState.Detached;
                throw;
            }

            OnAfterAssetAttributeCreated(assetattribute);

            return assetattribute;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> CancelAssetAttributeChanges(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAssetAttributeUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> UpdateAssetAttribute(int attributeid, ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute assetattribute)
        {
            OnAssetAttributeUpdated(assetattribute);

            var itemToUpdate = Context.AssetAttributes
                              .Where(i => i.AttributeId == assetattribute.AttributeId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(assetattribute);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAssetAttributeUpdated(assetattribute);

            return assetattribute;
        }

        partial void OnAssetAttributeDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);
        partial void OnAfterAssetAttributeDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> DeleteAssetAttribute(int attributeid)
        {
            var itemToDelete = Context.AssetAttributes
                              .Where(i => i.AttributeId == attributeid)
                              .Include(i => i.AssetAttributeValues)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAssetAttributeDeleted(itemToDelete);


            Context.AssetAttributes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAssetAttributeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAssetAttributeValuesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributevalues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributevalues/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAssetAttributeValuesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetattributevalues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetattributevalues/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAssetAttributeValuesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>> GetAssetAttributeValues(Query query = null)
        {
            var items = Context.AssetAttributeValues.AsQueryable();

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.AssetAttribute);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAssetAttributeValuesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAssetAttributeValueGet(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnGetAssetAttributeValueByAttributeValueId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> GetAssetAttributeValueByAttributeValueId(int attributevalueid)
        {
            var items = Context.AssetAttributeValues
                              .AsNoTracking()
                              .Where(i => i.AttributeValueId == attributevalueid);

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.AssetAttribute);
 
            OnGetAssetAttributeValueByAttributeValueId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAssetAttributeValueGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAssetAttributeValueCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> CreateAssetAttributeValue(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue assetattributevalue)
        {
            OnAssetAttributeValueCreated(assetattributevalue);

            var existingItem = Context.AssetAttributeValues
                              .Where(i => i.AttributeValueId == assetattributevalue.AttributeValueId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AssetAttributeValues.Add(assetattributevalue);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(assetattributevalue).State = EntityState.Detached;
                throw;
            }

            OnAfterAssetAttributeValueCreated(assetattributevalue);

            return assetattributevalue;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> CancelAssetAttributeValueChanges(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAssetAttributeValueUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> UpdateAssetAttributeValue(int attributevalueid, ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue assetattributevalue)
        {
            OnAssetAttributeValueUpdated(assetattributevalue);

            var itemToUpdate = Context.AssetAttributeValues
                              .Where(i => i.AttributeValueId == assetattributevalue.AttributeValueId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(assetattributevalue);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAssetAttributeValueUpdated(assetattributevalue);

            return assetattributevalue;
        }

        partial void OnAssetAttributeValueDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);
        partial void OnAfterAssetAttributeValueDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> DeleteAssetAttributeValue(int attributevalueid)
        {
            var itemToDelete = Context.AssetAttributeValues
                              .Where(i => i.AttributeValueId == attributevalueid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAssetAttributeValueDeleted(itemToDelete);


            Context.AssetAttributeValues.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAssetAttributeValueDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAssetCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetcategories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAssetCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetcategories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAssetCategoriesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>> GetAssetCategories(Query query = null)
        {
            var items = Context.AssetCategories.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAssetCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAssetCategoryGet(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnGetAssetCategoryByCategoryId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> GetAssetCategoryByCategoryId(int categoryid)
        {
            var items = Context.AssetCategories
                              .AsNoTracking()
                              .Where(i => i.CategoryId == categoryid);

 
            OnGetAssetCategoryByCategoryId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAssetCategoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAssetCategoryCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> CreateAssetCategory(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetcategory)
        {
            OnAssetCategoryCreated(assetcategory);

            var existingItem = Context.AssetCategories
                              .Where(i => i.CategoryId == assetcategory.CategoryId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AssetCategories.Add(assetcategory);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(assetcategory).State = EntityState.Detached;
                throw;
            }

            OnAfterAssetCategoryCreated(assetcategory);

            return assetcategory;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> CancelAssetCategoryChanges(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAssetCategoryUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> UpdateAssetCategory(int categoryid, ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory assetcategory)
        {
            OnAssetCategoryUpdated(assetcategory);

            var itemToUpdate = Context.AssetCategories
                              .Where(i => i.CategoryId == assetcategory.CategoryId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(assetcategory);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAssetCategoryUpdated(assetcategory);

            return assetcategory;
        }

        partial void OnAssetCategoryDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);
        partial void OnAfterAssetCategoryDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> DeleteAssetCategory(int categoryid)
        {
            var itemToDelete = Context.AssetCategories
                              .Where(i => i.CategoryId == categoryid)
                              .Include(i => i.AssetAttributes)
                              .Include(i => i.FixedAssets)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAssetCategoryDeleted(itemToDelete);


            Context.AssetCategories.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAssetCategoryDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAssetInsurancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetinsurances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetinsurances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAssetInsurancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/assetinsurances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/assetinsurances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAssetInsurancesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>> GetAssetInsurances(Query query = null)
        {
            var items = Context.AssetInsurances.AsQueryable();

            items = items.Include(i => i.FixedAsset);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAssetInsurancesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAssetInsuranceGet(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnGetAssetInsuranceByInsuranceId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> GetAssetInsuranceByInsuranceId(int insuranceid)
        {
            var items = Context.AssetInsurances
                              .AsNoTracking()
                              .Where(i => i.InsuranceId == insuranceid);

            items = items.Include(i => i.FixedAsset);
 
            OnGetAssetInsuranceByInsuranceId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAssetInsuranceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAssetInsuranceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> CreateAssetInsurance(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance assetinsurance)
        {
            OnAssetInsuranceCreated(assetinsurance);

            var existingItem = Context.AssetInsurances
                              .Where(i => i.InsuranceId == assetinsurance.InsuranceId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AssetInsurances.Add(assetinsurance);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(assetinsurance).State = EntityState.Detached;
                throw;
            }

            OnAfterAssetInsuranceCreated(assetinsurance);

            return assetinsurance;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> CancelAssetInsuranceChanges(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAssetInsuranceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> UpdateAssetInsurance(int insuranceid, ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance assetinsurance)
        {
            OnAssetInsuranceUpdated(assetinsurance);

            var itemToUpdate = Context.AssetInsurances
                              .Where(i => i.InsuranceId == assetinsurance.InsuranceId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(assetinsurance);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAssetInsuranceUpdated(assetinsurance);

            return assetinsurance;
        }

        partial void OnAssetInsuranceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);
        partial void OnAfterAssetInsuranceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> DeleteAssetInsurance(int insuranceid)
        {
            var itemToDelete = Context.AssetInsurances
                              .Where(i => i.InsuranceId == insuranceid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAssetInsuranceDeleted(itemToDelete);


            Context.AssetInsurances.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAssetInsuranceDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepreciationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/depreciations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/depreciations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepreciationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/depreciations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/depreciations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepreciationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>> GetDepreciations(Query query = null)
        {
            var items = Context.Depreciations.AsQueryable();

            items = items.Include(i => i.FixedAsset);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDepreciationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepreciationGet(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnGetDepreciationByDepreciationId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> GetDepreciationByDepreciationId(int depreciationid)
        {
            var items = Context.Depreciations
                              .AsNoTracking()
                              .Where(i => i.DepreciationId == depreciationid);

            items = items.Include(i => i.FixedAsset);
 
            OnGetDepreciationByDepreciationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepreciationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepreciationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> CreateDepreciation(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation depreciation)
        {
            OnDepreciationCreated(depreciation);

            var existingItem = Context.Depreciations
                              .Where(i => i.DepreciationId == depreciation.DepreciationId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Depreciations.Add(depreciation);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(depreciation).State = EntityState.Detached;
                throw;
            }

            OnAfterDepreciationCreated(depreciation);

            return depreciation;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> CancelDepreciationChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepreciationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> UpdateDepreciation(int depreciationid, ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation depreciation)
        {
            OnDepreciationUpdated(depreciation);

            var itemToUpdate = Context.Depreciations
                              .Where(i => i.DepreciationId == depreciation.DepreciationId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(depreciation);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepreciationUpdated(depreciation);

            return depreciation;
        }

        partial void OnDepreciationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);
        partial void OnAfterDepreciationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> DeleteDepreciation(int depreciationid)
        {
            var itemToDelete = Context.Depreciations
                              .Where(i => i.DepreciationId == depreciationid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepreciationDeleted(itemToDelete);


            Context.Depreciations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepreciationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDisposalRecordsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/disposalrecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/disposalrecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDisposalRecordsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/disposalrecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/disposalrecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDisposalRecordsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>> GetDisposalRecords(Query query = null)
        {
            var items = Context.DisposalRecords.AsQueryable();

            items = items.Include(i => i.FixedAsset);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDisposalRecordsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDisposalRecordGet(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnGetDisposalRecordByDisposalId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> GetDisposalRecordByDisposalId(int disposalid)
        {
            var items = Context.DisposalRecords
                              .AsNoTracking()
                              .Where(i => i.DisposalId == disposalid);

            items = items.Include(i => i.FixedAsset);
 
            OnGetDisposalRecordByDisposalId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDisposalRecordGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDisposalRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> CreateDisposalRecord(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord disposalrecord)
        {
            OnDisposalRecordCreated(disposalrecord);

            var existingItem = Context.DisposalRecords
                              .Where(i => i.DisposalId == disposalrecord.DisposalId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DisposalRecords.Add(disposalrecord);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(disposalrecord).State = EntityState.Detached;
                throw;
            }

            OnAfterDisposalRecordCreated(disposalrecord);

            return disposalrecord;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> CancelDisposalRecordChanges(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDisposalRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> UpdateDisposalRecord(int disposalid, ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord disposalrecord)
        {
            OnDisposalRecordUpdated(disposalrecord);

            var itemToUpdate = Context.DisposalRecords
                              .Where(i => i.DisposalId == disposalrecord.DisposalId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(disposalrecord);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDisposalRecordUpdated(disposalrecord);

            return disposalrecord;
        }

        partial void OnDisposalRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);
        partial void OnAfterDisposalRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> DeleteDisposalRecord(int disposalid)
        {
            var itemToDelete = Context.DisposalRecords
                              .Where(i => i.DisposalId == disposalid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDisposalRecordDeleted(itemToDelete);


            Context.DisposalRecords.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDisposalRecordDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDocumentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/documents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDocumentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/documents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDocumentsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Document> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Document>> GetDocuments(Query query = null)
        {
            var items = Context.Documents.AsQueryable();

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDocumentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDocumentGet(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnGetDocumentByDocumentId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Document> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> GetDocumentByDocumentId(int documentid)
        {
            var items = Context.Documents
                              .AsNoTracking()
                              .Where(i => i.DocumentId == documentid);

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);
 
            OnGetDocumentByDocumentId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDocumentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDocumentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> CreateDocument(ActivosFiljos.Server.Models.FixedAssetsDB.Document document)
        {
            OnDocumentCreated(document);

            var existingItem = Context.Documents
                              .Where(i => i.DocumentId == document.DocumentId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Documents.Add(document);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(document).State = EntityState.Detached;
                throw;
            }

            OnAfterDocumentCreated(document);

            return document;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> CancelDocumentChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Document item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDocumentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> UpdateDocument(int documentid, ActivosFiljos.Server.Models.FixedAssetsDB.Document document)
        {
            OnDocumentUpdated(document);

            var itemToUpdate = Context.Documents
                              .Where(i => i.DocumentId == document.DocumentId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(document);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDocumentUpdated(document);

            return document;
        }

        partial void OnDocumentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Document> DeleteDocument(int documentid)
        {
            var itemToDelete = Context.Documents
                              .Where(i => i.DocumentId == documentid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDocumentDeleted(itemToDelete);


            Context.Documents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDocumentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFixedAssetsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/fixedassets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/fixedassets/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFixedAssetsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/fixedassets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/fixedassets/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFixedAssetsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>> GetFixedAssets(Query query = null)
        {
            var items = Context.FixedAssets.AsQueryable();

            items = items.Include(i => i.AssetCategory);
            items = items.Include(i => i.Location);
            items = items.Include(i => i.Status);
            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFixedAssetsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFixedAssetGet(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnGetFixedAssetByAssetId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> GetFixedAssetByAssetId(int assetid)
        {
            var items = Context.FixedAssets
                              .AsNoTracking()
                              .Where(i => i.AssetId == assetid);

            items = items.Include(i => i.AssetCategory);
            items = items.Include(i => i.Location);
            items = items.Include(i => i.Status);
            items = items.Include(i => i.User);
 
            OnGetFixedAssetByAssetId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFixedAssetGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFixedAssetCreated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetCreated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> CreateFixedAsset(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedasset)
        {
            OnFixedAssetCreated(fixedasset);

            var existingItem = Context.FixedAssets
                              .Where(i => i.AssetId == fixedasset.AssetId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.FixedAssets.Add(fixedasset);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(fixedasset).State = EntityState.Detached;
                throw;
            }

            OnAfterFixedAssetCreated(fixedasset);

            return fixedasset;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> CancelFixedAssetChanges(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFixedAssetUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> UpdateFixedAsset(int assetid, ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset fixedasset)
        {
            OnFixedAssetUpdated(fixedasset);

            var itemToUpdate = Context.FixedAssets
                              .Where(i => i.AssetId == fixedasset.AssetId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(fixedasset);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFixedAssetUpdated(fixedasset);

            return fixedasset;
        }

        partial void OnFixedAssetDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);
        partial void OnAfterFixedAssetDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> DeleteFixedAsset(int assetid)
        {
            var itemToDelete = Context.FixedAssets
                              .Where(i => i.AssetId == assetid)
                              .Include(i => i.AssetAssignments)
                              .Include(i => i.AssetAttributeValues)
                              .Include(i => i.AssetInsurances)
                              .Include(i => i.Depreciations)
                              .Include(i => i.DisposalRecords)
                              .Include(i => i.Documents)
                              .Include(i => i.MaintenanceRecords)
                              .Include(i => i.Notifications)
                              .Include(i => i.ScheduledMaintenances)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFixedAssetDeleted(itemToDelete);


            Context.FixedAssets.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFixedAssetDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLocationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLocationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLocationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Location> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Location>> GetLocations(Query query = null)
        {
            var items = Context.Locations.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLocationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLocationGet(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnGetLocationByLocationId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Location> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> GetLocationByLocationId(int locationid)
        {
            var items = Context.Locations
                              .AsNoTracking()
                              .Where(i => i.LocationId == locationid);

 
            OnGetLocationByLocationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLocationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLocationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> CreateLocation(ActivosFiljos.Server.Models.FixedAssetsDB.Location location)
        {
            OnLocationCreated(location);

            var existingItem = Context.Locations
                              .Where(i => i.LocationId == location.LocationId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Locations.Add(location);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(location).State = EntityState.Detached;
                throw;
            }

            OnAfterLocationCreated(location);

            return location;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> CancelLocationChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Location item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLocationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> UpdateLocation(int locationid, ActivosFiljos.Server.Models.FixedAssetsDB.Location location)
        {
            OnLocationUpdated(location);

            var itemToUpdate = Context.Locations
                              .Where(i => i.LocationId == location.LocationId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(location);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLocationUpdated(location);

            return location;
        }

        partial void OnLocationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);
        partial void OnAfterLocationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Location item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Location> DeleteLocation(int locationid)
        {
            var itemToDelete = Context.Locations
                              .Where(i => i.LocationId == locationid)
                              .Include(i => i.FixedAssets)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLocationDeleted(itemToDelete);


            Context.Locations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLocationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMaintenanceRecordsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/maintenancerecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/maintenancerecords/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMaintenanceRecordsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/maintenancerecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/maintenancerecords/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMaintenanceRecordsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>> GetMaintenanceRecords(Query query = null)
        {
            var items = Context.MaintenanceRecords.AsQueryable();

            items = items.Include(i => i.FixedAsset);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMaintenanceRecordsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMaintenanceRecordGet(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnGetMaintenanceRecordByMaintenanceId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> GetMaintenanceRecordByMaintenanceId(int maintenanceid)
        {
            var items = Context.MaintenanceRecords
                              .AsNoTracking()
                              .Where(i => i.MaintenanceId == maintenanceid);

            items = items.Include(i => i.FixedAsset);
 
            OnGetMaintenanceRecordByMaintenanceId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMaintenanceRecordGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMaintenanceRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordCreated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> CreateMaintenanceRecord(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord maintenancerecord)
        {
            OnMaintenanceRecordCreated(maintenancerecord);

            var existingItem = Context.MaintenanceRecords
                              .Where(i => i.MaintenanceId == maintenancerecord.MaintenanceId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MaintenanceRecords.Add(maintenancerecord);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(maintenancerecord).State = EntityState.Detached;
                throw;
            }

            OnAfterMaintenanceRecordCreated(maintenancerecord);

            return maintenancerecord;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> CancelMaintenanceRecordChanges(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMaintenanceRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> UpdateMaintenanceRecord(int maintenanceid, ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord maintenancerecord)
        {
            OnMaintenanceRecordUpdated(maintenancerecord);

            var itemToUpdate = Context.MaintenanceRecords
                              .Where(i => i.MaintenanceId == maintenancerecord.MaintenanceId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(maintenancerecord);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMaintenanceRecordUpdated(maintenancerecord);

            return maintenancerecord;
        }

        partial void OnMaintenanceRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);
        partial void OnAfterMaintenanceRecordDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> DeleteMaintenanceRecord(int maintenanceid)
        {
            var itemToDelete = Context.MaintenanceRecords
                              .Where(i => i.MaintenanceId == maintenanceid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMaintenanceRecordDeleted(itemToDelete);


            Context.MaintenanceRecords.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMaintenanceRecordDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportNotificationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/notifications/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/notifications/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNotificationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/notifications/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/notifications/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNotificationsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>> GetNotifications(Query query = null)
        {
            var items = Context.Notifications.AsQueryable();

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnNotificationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNotificationGet(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnGetNotificationByNotificationId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> GetNotificationByNotificationId(int notificationid)
        {
            var items = Context.Notifications
                              .AsNoTracking()
                              .Where(i => i.NotificationId == notificationid);

            items = items.Include(i => i.FixedAsset);
            items = items.Include(i => i.User);
 
            OnGetNotificationByNotificationId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnNotificationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnNotificationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> CreateNotification(ActivosFiljos.Server.Models.FixedAssetsDB.Notification notification)
        {
            OnNotificationCreated(notification);

            var existingItem = Context.Notifications
                              .Where(i => i.NotificationId == notification.NotificationId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Notifications.Add(notification);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(notification).State = EntityState.Detached;
                throw;
            }

            OnAfterNotificationCreated(notification);

            return notification;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> CancelNotificationChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNotificationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> UpdateNotification(int notificationid, ActivosFiljos.Server.Models.FixedAssetsDB.Notification notification)
        {
            OnNotificationUpdated(notification);

            var itemToUpdate = Context.Notifications
                              .Where(i => i.NotificationId == notification.NotificationId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(notification);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterNotificationUpdated(notification);

            return notification;
        }

        partial void OnNotificationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);
        partial void OnAfterNotificationDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Notification item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> DeleteNotification(int notificationid)
        {
            var itemToDelete = Context.Notifications
                              .Where(i => i.NotificationId == notificationid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNotificationDeleted(itemToDelete);


            Context.Notifications.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNotificationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Role> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Role>> GetRoles(Query query = null)
        {
            var items = Context.Roles.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoleGet(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnGetRoleByRoleId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Role> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> GetRoleByRoleId(int roleid)
        {
            var items = Context.Roles
                              .AsNoTracking()
                              .Where(i => i.RoleId == roleid);

 
            OnGetRoleByRoleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> CreateRole(ActivosFiljos.Server.Models.FixedAssetsDB.Role role)
        {
            OnRoleCreated(role);

            var existingItem = Context.Roles
                              .Where(i => i.RoleId == role.RoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Roles.Add(role);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(role).State = EntityState.Detached;
                throw;
            }

            OnAfterRoleCreated(role);

            return role;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> CancelRoleChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Role item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> UpdateRole(int roleid, ActivosFiljos.Server.Models.FixedAssetsDB.Role role)
        {
            OnRoleUpdated(role);

            var itemToUpdate = Context.Roles
                              .Where(i => i.RoleId == role.RoleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(role);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRoleUpdated(role);

            return role;
        }

        partial void OnRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);
        partial void OnAfterRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Role item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Role> DeleteRole(int roleid)
        {
            var itemToDelete = Context.Roles
                              .Where(i => i.RoleId == roleid)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoleDeleted(itemToDelete);


            Context.Roles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportScheduledMaintenancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/scheduledmaintenances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/scheduledmaintenances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportScheduledMaintenancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/scheduledmaintenances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/scheduledmaintenances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnScheduledMaintenancesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>> GetScheduledMaintenances(Query query = null)
        {
            var items = Context.ScheduledMaintenances.AsQueryable();

            items = items.Include(i => i.FixedAsset);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnScheduledMaintenancesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnScheduledMaintenanceGet(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnGetScheduledMaintenanceByScheduleId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> GetScheduledMaintenanceByScheduleId(int scheduleid)
        {
            var items = Context.ScheduledMaintenances
                              .AsNoTracking()
                              .Where(i => i.ScheduleId == scheduleid);

            items = items.Include(i => i.FixedAsset);
 
            OnGetScheduledMaintenanceByScheduleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnScheduledMaintenanceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnScheduledMaintenanceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceCreated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> CreateScheduledMaintenance(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance scheduledmaintenance)
        {
            OnScheduledMaintenanceCreated(scheduledmaintenance);

            var existingItem = Context.ScheduledMaintenances
                              .Where(i => i.ScheduleId == scheduledmaintenance.ScheduleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ScheduledMaintenances.Add(scheduledmaintenance);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(scheduledmaintenance).State = EntityState.Detached;
                throw;
            }

            OnAfterScheduledMaintenanceCreated(scheduledmaintenance);

            return scheduledmaintenance;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> CancelScheduledMaintenanceChanges(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnScheduledMaintenanceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> UpdateScheduledMaintenance(int scheduleid, ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance scheduledmaintenance)
        {
            OnScheduledMaintenanceUpdated(scheduledmaintenance);

            var itemToUpdate = Context.ScheduledMaintenances
                              .Where(i => i.ScheduleId == scheduledmaintenance.ScheduleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(scheduledmaintenance);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterScheduledMaintenanceUpdated(scheduledmaintenance);

            return scheduledmaintenance;
        }

        partial void OnScheduledMaintenanceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);
        partial void OnAfterScheduledMaintenanceDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> DeleteScheduledMaintenance(int scheduleid)
        {
            var itemToDelete = Context.ScheduledMaintenances
                              .Where(i => i.ScheduleId == scheduleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnScheduledMaintenanceDeleted(itemToDelete);


            Context.ScheduledMaintenances.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterScheduledMaintenanceDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUserRolesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>> GetUserRoles(Query query = null)
        {
            var items = Context.UserRoles.AsQueryable();

            items = items.Include(i => i.Role);
            items = items.Include(i => i.User);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserRoleGet(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnGetUserRoleByUserRoleId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> GetUserRoleByUserRoleId(int userroleid)
        {
            var items = Context.UserRoles
                              .AsNoTracking()
                              .Where(i => i.UserRoleId == userroleid);

            items = items.Include(i => i.Role);
            items = items.Include(i => i.User);
 
            OnGetUserRoleByUserRoleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleCreated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> CreateUserRole(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole userrole)
        {
            OnUserRoleCreated(userrole);

            var existingItem = Context.UserRoles
                              .Where(i => i.UserRoleId == userrole.UserRoleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UserRoles.Add(userrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(userrole).State = EntityState.Detached;
                throw;
            }

            OnAfterUserRoleCreated(userrole);

            return userrole;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> CancelUserRoleChanges(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> UpdateUserRole(int userroleid, ActivosFiljos.Server.Models.FixedAssetsDB.UserRole userrole)
        {
            OnUserRoleUpdated(userrole);

            var itemToUpdate = Context.UserRoles
                              .Where(i => i.UserRoleId == userrole.UserRoleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(userrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUserRoleUpdated(userrole);

            return userrole;
        }

        partial void OnUserRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);
        partial void OnAfterUserRoleDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.UserRole item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> DeleteUserRole(int userroleid)
        {
            var itemToDelete = Context.UserRoles
                              .Where(i => i.UserRoleId == userroleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserRoleDeleted(itemToDelete);


            Context.UserRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.User> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserGet(ActivosFiljos.Server.Models.FixedAssetsDB.User item);
        partial void OnGetUserByUserId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.User> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> GetUserByUserId(int userid)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.UserId == userid);

 
            OnGetUserByUserId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserCreated(ActivosFiljos.Server.Models.FixedAssetsDB.User item);
        partial void OnAfterUserCreated(ActivosFiljos.Server.Models.FixedAssetsDB.User item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> CreateUser(ActivosFiljos.Server.Models.FixedAssetsDB.User user)
        {
            OnUserCreated(user);

            var existingItem = Context.Users
                              .Where(i => i.UserId == user.UserId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Users.Add(user);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(user).State = EntityState.Detached;
                throw;
            }

            OnAfterUserCreated(user);

            return user;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> CancelUserChanges(ActivosFiljos.Server.Models.FixedAssetsDB.User item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.User item);
        partial void OnAfterUserUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.User item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> UpdateUser(int userid, ActivosFiljos.Server.Models.FixedAssetsDB.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.UserId == user.UserId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUserUpdated(user);

            return user;
        }

        partial void OnUserDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.User item);
        partial void OnAfterUserDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.User item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.User> DeleteUser(int userid)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.UserId == userid)
                              .Include(i => i.AssetAssignments)
                              .Include(i => i.Documents)
                              .Include(i => i.FixedAssets)
                              .Include(i => i.Notifications)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);


            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/statuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/statuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/fixedassetsdb/statuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/fixedassetsdb/statuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnStatusesRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Status> items);

        public async Task<IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Status>> GetStatuses(Query query = null)
        {
            var items = Context.Statuses.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStatusGet(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnGetStatusByStatusId(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Status> items);


        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> GetStatusByStatusId(int statusid)
        {
            var items = Context.Statuses
                              .AsNoTracking()
                              .Where(i => i.StatusId == statusid);

 
            OnGetStatusByStatusId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnStatusCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> CreateStatus(ActivosFiljos.Server.Models.FixedAssetsDB.Status status)
        {
            OnStatusCreated(status);

            var existingItem = Context.Statuses
                              .Where(i => i.StatusId == status.StatusId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Statuses.Add(status);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(status).State = EntityState.Detached;
                throw;
            }

            OnAfterStatusCreated(status);

            return status;
        }

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> CancelStatusChanges(ActivosFiljos.Server.Models.FixedAssetsDB.Status item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnStatusUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> UpdateStatus(int statusid, ActivosFiljos.Server.Models.FixedAssetsDB.Status status)
        {
            OnStatusUpdated(status);

            var itemToUpdate = Context.Statuses
                              .Where(i => i.StatusId == status.StatusId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(status);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterStatusUpdated(status);

            return status;
        }

        partial void OnStatusDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);
        partial void OnAfterStatusDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Status item);

        public async Task<ActivosFiljos.Server.Models.FixedAssetsDB.Status> DeleteStatus(int statusid)
        {
            var itemToDelete = Context.Statuses
                              .Where(i => i.StatusId == statusid)
                              .Include(i => i.FixedAssets)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnStatusDeleted(itemToDelete);


            Context.Statuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterStatusDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}