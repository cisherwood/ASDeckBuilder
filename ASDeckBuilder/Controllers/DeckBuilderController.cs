using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASDeckBuilder.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASDeckBuilder.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ASDeckBuilder.Controllers
{
    public class DeckBuilderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeckBuilderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DeckBuilder
        public ActionResult Index()
        {

            DeckBuilderViewModel vm = new DeckBuilderViewModel();

            Decks deck = _context.Decks.FirstOrDefault();
            List<CardDecks> cardDecks = _context.CardDecks.Include(z=>z.Card).Where(x => x.DeckId == deck.DeckId).ToList();
            List<Card> cards = cardDecks.Select(x => x.Card).ToList();
            vm.DeckId = 1; // TODO


            return View(vm);
        }

        // GET: DeckBuilder/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeckBuilder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeckBuilder/Create
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

        // GET: DeckBuilder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeckBuilder/Edit/5
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

        // GET: DeckBuilder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeckBuilder/Delete/5
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