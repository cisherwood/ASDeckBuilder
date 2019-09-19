using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASDeckBuilder.Controllers
{
    public class ModifyDeckController : Controller
    {
        public class ModifyDeckModel
        {
            public string cardId  { get; set; }
            public string modifyAmount { get; set; }
        }

        [HttpPost]
        public ActionResult ModifyDeck([FromBody] ModifyDeckModel modifyDeckModel)
        {


            return View();
        }

        // GET: ModifyDeck
        public ActionResult Index()
        {
            return View();
        }

        // GET: ModifyDeck/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModifyDeck/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModifyDeck/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModifyDeck/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModifyDeck/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModifyDeck/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModifyDeck/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}