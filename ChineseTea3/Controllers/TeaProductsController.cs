using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChineseTea3.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using WebGrease;

namespace ChineseTea3.Controllers
{
    public class TeaProductsController : Controller
    {
        ChineseTeaShopEntities dbContext = new ChineseTeaShopEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(TeaProduct tea, HttpPostedFileBase uploadedImageFile)
        {

            UploadedImage(tea, uploadedImageFile);
            dbContext.TeaProducts.Add(tea);
            dbContext.SaveChanges();

            List<TeaProduct> teaProducts = dbContext.TeaProducts.ToList();

            return RedirectToAction("ViewProducts");
        }

        private void UploadedImage(TeaProduct tea, HttpPostedFileBase uploadedImageFile)
        {
            string fileName = string.Empty;
            if (uploadedImageFile != null)
            {
                fileName = System.IO.Path.GetFileName(uploadedImageFile.FileName);
                tea.ImageFileName = fileName;

                string imageFilePathOnServer = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                uploadedImageFile.SaveAs(imageFilePathOnServer);
            }
        }

        public ActionResult ViewProducts(ProductsViewModel pv, int? page)
        {
            List<string> kindList = new List<string>();
            List<string> priceRangeList = new List<string>();

            pv.TeaList = dbContext.TeaProducts.ToList();

            //if (pv.SortByPrice == "All")
            //{
            //    pv.TeaPagedList = pv.TeaList.ToPagedList(page ?? 1, 4);
            //    return View(pv);
            //}

            //if (pv.TeaList == null || pv.TeaList.Count == 0)
            //{
            //    pv.TeaList = dbContext.TeaProducts.ToList().ToPagedList(page ?? 1, 4);
            //}

            if (pv.IsBlack)
            {
                kindList.Add("Black & Dark");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Black & Dark").ToList();
            }

            if (pv.IsCompressed)
            {
                kindList.Add("Compressed");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Compressed").ToList();
            }

            if (pv.IsGreen)
            {
                kindList.Add("Green");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Green").ToList();
            }

            if (pv.IsOolong)
            {
                kindList.Add("Oolong");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Oolong").ToList();
            }

            if (pv.IsRed)
            {
                kindList.Add("Red");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Red").ToList();
            }

            if (pv.IsScented)
            {
                kindList.Add("Scented");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Scented").ToList();
            }

            if (pv.IsWhite)
            {
                kindList.Add("White");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "White").ToList();
            }

            if (pv.IsYellow)
            {
                kindList.Add("Yellow");
                //pv.TeaList = pv.TeaList.Where(m => m.Kind == "Yellow").ToList();
            }

            List<TeaProduct> temp = new List<TeaProduct>();
            foreach (string kind in kindList)
            {
                temp.AddRange(pv.TeaList.Where(m => m.Kind == kind).ToList());
            }

            if (temp.Count != 0)
            {
                pv.TeaList = temp;
            }

            if (pv.IsLessThan50)
            {
                priceRangeList.Add("lessThan50");
                //pv.TeaList = pv.TeaList.Where(m => m.Price <= 5).ToList();
            }

            if (pv.IsBetween50And100)
            {
                priceRangeList.Add("between50And100");
                //pv.TeaList = pv.TeaList.Where(m => m.Price > 5 && m.Price <= 8).ToList();
            }

            if (pv.IsBetween100And200)
            {
                priceRangeList.Add("between100And200");
                //pv.TeaList = pv.TeaList.Where(m => m.Price > 8 && m.Price <= 10).ToList();
            }

            if (pv.IsOver200)
            {
                priceRangeList.Add("over200");
                //pv.TeaList = pv.TeaList.Where(m => m.Price > 10).ToList();
            }

            temp = new List<TeaProduct>();

            foreach (string range in priceRangeList)
            {
                if (range == "lessThan50")
                {
                    temp.AddRange(pv.TeaList.Where(m => m.Price <= 5).ToList());
                }

                if (range == "between50And100")
                {
                    temp.AddRange(pv.TeaList.Where(m => m.Price > 5 && m.Price <= 8).ToList());
                }

                if (range == "between100And200")
                {
                    temp.AddRange(pv.TeaList.Where(m => m.Price > 8 && m.Price <= 10).ToList());
                }

                if (range == "over200")
                {
                    temp.AddRange(pv.TeaList.Where(m => m.Price > 10).ToList());
                }
            }

            if (temp.Count != 0 || (priceRangeList.Count != 0 && temp.Count == 0))
            {
                pv.TeaList = temp;
            }

            if (pv.SortByPrice == "Lowest to highest")
            {
                pv.TeaList = pv.TeaList.OrderBy(m => m.Price).ToList();
            }
            else if (pv.SortByPrice == "Highest to lowest")
            {
                pv.TeaList = pv.TeaList.OrderByDescending(m => m.Price).ToList();
            }

            pv.TeaPagedList = pv.TeaList.ToPagedList(page ?? 1, 4);

            return View(pv);
        }

        public ActionResult Details(int id, int? page)
        {
            DetailsViewModel dv = new DetailsViewModel();
            //RelatedContentViewModel rv = new RelatedContentViewModel();
            dv.Rv.Tea = dbContext.TeaProducts.FirstOrDefault(m => m.ID == id);
            dv.ProductId = id;
            dv.Pv.FinalPrice = dv.Rv.Tea.Price;
            //dv.Pv.Weight = dv.Pv.Weight ?? "25";
            //dv.Pv.Amount = dv.Pv.Amount ?? "1";
            List<TeaProduct> temp1 = dbContext.TeaProducts.Where(m => m.Kind == dv.Rv.Tea.Kind).ToList();
            List<TeaProduct> temp2 = new List<TeaProduct>();
            foreach (var ele in temp1)
            {
                if (ele.ID != dv.Rv.Tea.ID)
                {
                    temp2.Add(ele);
                }
            }

            dv.Rv.RelatedContentPagedList = temp2.ToPagedList(page ?? 1, 4);

            dv.CommentsList = dbContext.ProductComments.Where(m => m.ProductId == id).ToList();

            if (Request.IsAuthenticated)
            {
                dv.CurrentComment = new ProductComment();
            }

            return View(dv);
        }

        public ActionResult ShowFinalPrice(PurchaseViewModel pv, string price)
        {
            int weightInt;
            int amountInt;
            if (!int.TryParse(pv.Weight, out weightInt))
            {
                weightInt = 25;
            }

            if (!int.TryParse(pv.Amount, out amountInt))
            {
                amountInt = 1;
            }

            decimal unitPrice = decimal.Parse(price);

            decimal finalPrice = 0;
            if (weightInt == 25)
            {
                finalPrice = unitPrice * amountInt;
            }
            else if (weightInt == 50)
            {
                finalPrice = unitPrice * (decimal)(2 * 0.95 * amountInt);
            }
            else if (weightInt == 100)
            {
                finalPrice = unitPrice * (decimal)(4 * 0.9 * amountInt);
            }
            else if (weightInt == 250)
            {
                finalPrice = unitPrice * (decimal)(10 * 0.85 * amountInt);
            }
            else if (weightInt == 500)
            {
                finalPrice = unitPrice * (decimal)(20 * 0.8 * amountInt);
            }

            pv.FinalPrice = finalPrice;

            return Json(pv);
        }


        public ActionResult Edit(int id)
        {
            TeaProduct tea = dbContext.TeaProducts.FirstOrDefault(m => m.ID == id);
            return View(tea);
        }

        public ActionResult ViewAllComments(int id, int? page)
        {
            DetailsViewModel dv = new DetailsViewModel();
            dv.ProductId = id;
            dv.CommentsList = dbContext.ProductComments.Where(m => m.ProductId == id).OrderByDescending(m=>m.AddTime).ToList();
            dv.CommentsPagedList = dv.CommentsList.ToPagedList(page ?? 1, 4);

            return View("AddComment", dv);
        }


        public ActionResult AddComment(DetailsViewModel dv, int? page) // (int? id) can be used to pass product id
        {
            if (Request.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                //record could be null
                //ShRecord record =
                //    dbContext.ShRecords.FirstOrDefault(m => m.ProductId == dv.Rv.Tea.ID && m.UserId == userId);


                dv.Rv.Tea = dbContext.TeaProducts.FirstOrDefault(m => m.ID == dv.ProductId);
                ProductComment comment = dbContext.ProductComments.FirstOrDefault(m => m.ProductId == dv.ProductId && m.UserId == userId);

                //Check whether comment has been created by current user. If created then will not allow to create again.
                //if (comment == null)
                //{
                    dv.CurrentComment.UserId = userId;
                    dv.CurrentComment.ProductId = dv.ProductId;
                    dv.CurrentComment.AddTime = DateTime.Now;
                    dv.CurrentComment.UserName = dbContext.AspNetUsers.FirstOrDefault(m => m.Id == userId).Nickname;

                    dbContext.ProductComments.Add(dv.CurrentComment);
                    dbContext.SaveChanges();

                    //add rating to database
                    Rating rating = new Rating();
                    rating.RatingValue = dv.SelectedRating;
                    rating.ProductId = dv.ProductId;
                    rating.UserId = userId;
                    rating.CommentId = dv.CurrentComment.Id;
                    dbContext.Ratings.Add(rating);
                    dbContext.SaveChanges();
                //}

                List<Rating> ratingList = dbContext.Ratings.Where(m => m.ProductId == dv.ProductId).ToList();
                int finalRating = (int)Math.Round(((double)ratingList.Sum(m=>m.RatingValue)) /ratingList.Count);
                dv.Rv.Tea.Ratings = finalRating;
                dbContext.Entry(dv.Rv.Tea).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();

                dv.CommentsList = dbContext.ProductComments.Where(m => m.ProductId == dv.ProductId).OrderByDescending(m => m.AddTime).ToList();
                dv.CommentsPagedList = dv.CommentsList.ToPagedList(page ?? 1, 4);

                return View(dv);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}