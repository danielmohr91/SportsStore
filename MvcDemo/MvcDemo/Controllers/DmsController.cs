using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDemo.Core;
using MvcDemo.Core.Interfaces;

namespace MvcDemo.Controllers
{
    public class DmsController : Controller
    {
        private IDocumentService _documentService;
        private IFileService _fileService;
        public DmsController()
            : this(new DocumentService(), new FileService())
        {
        }

        public DmsController(IDocumentService documentService, IFileService fileService)
        {
            _documentService = documentService;
            _fileService = fileService;
        }

        //
        // GET: /Document/
        //[OutputCache(CacheProfile = "Cache1Hour")]
        public ActionResult Index()
        {
            return View(_documentService.GetDocumentList(10, 1)); 
        }

        public ActionResult DocFileList(int id)
        {
            return PartialView("ControlsDms/DocumentFileListPopup", _documentService.GetDocumentFileList(id));
        }

        //
        // GET: /Document/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Document/PopDetails/5

        public ActionResult PopDetails(int id)
        {
            return PartialView("PopDetails", _documentService.GetDocumentFileList(id));
        }

        //
        // GET: /Document/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Document/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Document/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Document/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
