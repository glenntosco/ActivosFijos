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
    [Route("odata/FixedAssetsDB/Documents")]
    public partial class DocumentsController : ODataController
    {
        private ActivosFiljos.Server.Data.FixedAssetsDBContext context;

        public DocumentsController(ActivosFiljos.Server.Data.FixedAssetsDBContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<ActivosFiljos.Server.Models.FixedAssetsDB.Document> GetDocuments()
        {
            var items = this.context.Documents.AsQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Document>();
            this.OnDocumentsRead(ref items);

            return items;
        }

        partial void OnDocumentsRead(ref IQueryable<ActivosFiljos.Server.Models.FixedAssetsDB.Document> items);

        partial void OnDocumentGet(ref SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Document> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/FixedAssetsDB/Documents(DocumentId={DocumentId})")]
        public SingleResult<ActivosFiljos.Server.Models.FixedAssetsDB.Document> GetDocument(int key)
        {
            var items = this.context.Documents.Where(i => i.DocumentId == key);
            var result = SingleResult.Create(items);

            OnDocumentGet(ref result);

            return result;
        }
        partial void OnDocumentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentDeleted(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        [HttpDelete("/odata/FixedAssetsDB/Documents(DocumentId={DocumentId})")]
        public IActionResult DeleteDocument(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Documents
                    .Where(i => i.DocumentId == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnDocumentDeleted(item);
                this.context.Documents.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDocumentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentUpdated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        [HttpPut("/odata/FixedAssetsDB/Documents(DocumentId={DocumentId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDocument(int key, [FromBody]ActivosFiljos.Server.Models.FixedAssetsDB.Document item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.DocumentId != key))
                {
                    return BadRequest();
                }
                this.OnDocumentUpdated(item);
                this.context.Documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documents.Where(i => i.DocumentId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/FixedAssetsDB/Documents(DocumentId={DocumentId})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDocument(int key, [FromBody]Delta<ActivosFiljos.Server.Models.FixedAssetsDB.Document> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Documents.Where(i => i.DocumentId == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnDocumentUpdated(item);
                this.context.Documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documents.Where(i => i.DocumentId == key);
                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");
                this.OnAfterDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);
        partial void OnAfterDocumentCreated(ActivosFiljos.Server.Models.FixedAssetsDB.Document item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] ActivosFiljos.Server.Models.FixedAssetsDB.Document item)
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

                this.OnDocumentCreated(item);
                this.context.Documents.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documents.Where(i => i.DocumentId == item.DocumentId);

                Request.QueryString = Request.QueryString.Add("$expand", "FixedAsset,User");

                this.OnAfterDocumentCreated(item);

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
