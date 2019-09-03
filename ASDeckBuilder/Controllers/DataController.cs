using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ASDeckBuilder.Data;
using ASDeckBuilder.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASDeckBuilder.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DataController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        // GET: Data/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public async Task<IActionResult> GetCardDetail()
        {


            HttpClient client = new HttpClient();

            // get all cards from database
            var cards = _context.Cards.ToList();


            // Loop through all cards in database
            foreach (Card c in cards)
            {
                try
                {
                    // Get card url
                    string cardUrl = c.Url;

                    // Access argent saga website
                    using (var response = await client.GetAsync(cardUrl))
                    {
                        using (var content = response.Content)
                        {
                            // read aregent sage website card detail page in non-blocking way
                            var result = await content.ReadAsStringAsync();
                            var document = new HtmlDocument();
                            document.LoadHtml(result);

                            // Get current card information 
                            Card card = _context.Cards.Where(x => x.Name == c.Name).FirstOrDefault();


                            // *** Card Categories ***

                            // Get card categories from argent saaga website
                            var cardCategories = document.DocumentNode.SelectNodes("/html/body/div[3]/div/div/section[2]/div/div/div[2]/div/div/div[4]/div/div/span[1]/span[2]");
                            // Select category text
                            string nodes = cardCategories[0].InnerText;
                            // Clean Category
                            char[] charsToTrim = { ' ' };
                            // Remove whitespace
                            string[] nodeList = nodes.Split(",").Select(x => x.Trim(charsToTrim)).ToArray();


                            // Loop through each categories
                            foreach (string nodeCategory in nodeList)
                            {
                                // Ensure category exists
                                if (_context.Categories.Any(o => o.Name == nodeCategory))
                                {
                                    // if  category already exists, do nothing
                                }
                                else
                                {
                                    // Create category entry
                                    Categories category = new Categories();
                                    category.Name = nodeCategory;

                                    // add category to database
                                    _context.Add(category);
                                }

                                await _context.SaveChangesAsync();

                                // Check if card category entry exists
                                if (_context.CardCategories.Any(o => o.Categories.Name == nodeCategory && o.Card.Name == c.Name))
                                {
                                    // Card Category entry exists, do nothing
                                }
                                else
                                {
                                    CardCategories cardCategory = new CardCategories();
                                    cardCategory.CardId = c.CardId;
                                    cardCategory.CategoryId = _context.Categories.Where(x => x.Name == nodeCategory).FirstOrDefault().CategoryId;

                                    // add card category to database
                                    _context.Add(cardCategory);

                                }

                                await _context.SaveChangesAsync();
                            }


                            // *** Card Tags ***


                            // Get card tag from argent saaga website
                            var cardTags = document.DocumentNode.SelectNodes("/html/body/div[3]/div/div/section[2]/div/div/div[2]/div/div/div[4]/div/div/span[2]/span[2]");
                            // Select category text
                            string tagNodes = cardTags[0].InnerText;
                            // Remove whitespace
                            string[] tagNodeList = tagNodes.Split(",").Select(x => x.Trim(charsToTrim)).ToArray();

                            // Loop through all tags from agrent saga website for this card
                            foreach (string nodeTag in tagNodeList)
                            {
                                // Ensure tag exists
                                if (_context.Tags.Any(o => o.Name == nodeTag))
                                {
                                    // if  tag already exists, do nothing
                                }
                                else
                                {
                                    // Create tag entry
                                    Tags tag = new Tags();
                                    tag.Name = nodeTag;

                                    // add tag to database
                                    _context.Add(tag);
                                }

                                await _context.SaveChangesAsync();

                                // Check if card tag entry exists
                                if (_context.CardTags.Any(o => o.Tag.Name == nodeTag && o.Card.Name == c.Name))
                                {
                                    // Card tag entry exists, do nothing
                                }
                                else
                                {
                                    CardTags cardTag = new CardTags();
                                    cardTag.CardId = c.CardId;
                                    cardTag.TagId = _context.Tags.Where(x => x.Name == nodeTag).FirstOrDefault().TagId;

                                    // add card category to database
                                    _context.Add(cardTag);

                                }

                                await _context.SaveChangesAsync();
                            }


                            // *** Card Image ***
                            var cardImage = document.DocumentNode.SelectNodes("/html/body/div[3]/div/div/section[2]/div/div/div[1]/div/div/div[2]/div/div/figure/div/a/img").FirstOrDefault();
                            // Get card img link
                            var src = cardImage.Attributes["src"].Value;
                            // Create file location
                            string downloadLocation = _hostingEnvironment.ContentRootPath + "/wwwroot/img/cards/" + c.CardId + ".jpg";

                            var wc = new System.Net.WebClient();
                            wc.DownloadFile(src, downloadLocation);

                            Console.WriteLine("");


                            


                        }
                    }
                }
                catch
                {

                }
                


            }


            await _context.SaveChangesAsync();


            return View();



        }

        public async Task<IActionResult> FixCardNames()
        {
            IList<Card> cards = _context.Cards.ToList();

            foreach(Card c in cards)
            {
                StringWriter stringWriter = new StringWriter();
                string newName;

                HttpUtility.HtmlDecode(c.Name, stringWriter);
                newName = stringWriter.ToString();

                c.Name = newName;
                _context.Update(c);

                await _context.SaveChangesAsync();
            }


            return View();

        }

        public async Task<IActionResult> GetCardNames()
        {

            int numberOfPages = 6;
            int maxCardsPerPage = 40;

            HttpClient client = new HttpClient();
            
            // Loop through pages on AS card list website
            for(int p = 1; p <= numberOfPages; p++)
            {
                // AS Card list static url
                string cardListUrl = "https://argentsaga.com/product-category/cards/page/" + p;

                using (var response = await client.GetAsync(cardListUrl))
                {
                    using (var content = response.Content)
                    {
                        // read AS card page in non-blocking way
                        var result = await content.ReadAsStringAsync();
                        var document = new HtmlDocument();
                        document.LoadHtml(result);

                        // Loops through each card on page
                        for (int c = 1; c <= maxCardsPerPage; c++)
                        {
                            // Build individual card nodes from list of cards on page
                            string cardNameNode = "/html/body/div[2]/div/div/section/div/div/div/div/div/div[6]/div/div/div[2]/ul/li[" + c + "]/div/a[1]/h2";
                            string cardUrlNode = "/html/body/div[2]/div/div/section/div/div/div/div/div/div[6]/div/div/div[2]/ul/li[" + c + "]/div/a[2]";

                            // Attempt to get card data from AG card list
                            try
                            {
                                var cardName = document.DocumentNode.SelectNodes(cardNameNode);
                                var cardUrl = document.DocumentNode.SelectNodes(cardUrlNode);

                                Card card = new Card();
                                card.Name = cardName[0].InnerText;
                                card.Url = cardUrl[0].GetAttributeValue("href", string.Empty);

                                // Add card to database
                                try
                                {
                                    if(_context.Cards.Any(o=>o.Name == card.Name))
                                    {
                                        // if card already exists, do nothing
                                    }
                                    else
                                    {
                                        // add card to database
                                        _context.Add(card);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                                catch
                                {
       

                                }


                            }
                            catch
                            {

                                // Will fail if card does not exist on page

                            }
                        }

                    }
                }
            }

            

            return View();

        }

        // GET: Data/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Data/Create
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

        // GET: Data/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Data/Edit/5
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

        // GET: Data/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Data/Delete/5
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