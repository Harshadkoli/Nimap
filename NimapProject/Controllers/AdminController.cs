using NimapProject.Dal;
using NimapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace NimapProject.Controllers
{
    public class AdminController : Controller
    {
        AdminDal Objadmin = new AdminDal();
        // GET: Admin
        [HttpGet]
        public ActionResult Insertcat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insertcat(CategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model = Objadmin.Register(model);
                    if (model.message == "Success")
                    {
                        TempData["Class"] = "success";
                        TempData["Message"] = model.message;
                        //ModelState.Clear();
                        return RedirectToAction("GetCategory");
                    }
                    else
                    {
                        TempData["Class"] = "primary";
                        TempData["Message"] = model.message;
                        return View(model);
                    }
                }
                else
                {
                    return View();
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult Insetpro()
        {
            try
            {
                ProductModel model = new ProductModel();
                List<CategoryModel> CatList = new List<CategoryModel>();

                if (ModelState.IsValid)
                {
                    CatList = Objadmin.GetAllCategory();
                    List<Category> CategoryList = new List<Category>();

                    foreach(var item in CatList)
                    {
                        Category Cat = new Category();
                        Cat.CategoryName = item.CategoryName;
                        Cat.CategoryId = item.CategoryId;
                        CategoryList.Add(Cat);
                    }
                    model.CategoryList = CategoryList;
                    model.CatList = model.CategoryList;
                    return View(model);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Insetpro(ProductModel model)
        {
            try
            {
                //List<CategoryModel> CatList = new List<CategoryModel>();
                //List<Category> CategoryList = new List<Category>();
                //CatList = Objadmin.GetAllCategory();

                //foreach (var item in CatList)
                //{
                //    Category Cat = new Category();
                //    Cat.CategoryName = item.CategoryName;
                //    Cat.CategoryId = item.CategoryId;
                //    CategoryList.Add(Cat);
                //}
                //model.CategoryList = CategoryList;
                //model.CatList = model.CategoryList;

                if (ModelState.IsValid)
                {
                    //model.hdncategoryvalues = string.Join(",", Array.ConvertAll<object, string>(model.Product_CatName.ToArray(), Convert.ToString));
                    //model.hdncategoryidvalues = int.Join(",", Array.ConvertAll<object, int>(model.Product_CatId., Convert.ToString));
                    model = Objadmin.CreateProduct(model);

                    if (model.message=="Success")
                    {
                        ViewBag.Message = "";
                        ViewBag.Class = "";
                        TempData["Message"] = model.message;
                        TempData["Class"] = "success";
                        return RedirectToAction("GetAllProduct");
                    }
                    else
                    {
                        ViewBag.Class = "primary";
                        ViewBag.Message = model.message;

                        return View(model);
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult GetCategory()
        {
            List<CategoryModel> getlist = new List<CategoryModel>();
            
            getlist = Objadmin.ShowData(getlist);
            ViewBag.getlist = getlist;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryModel GetData = new CategoryModel();
            GetData = Objadmin.GetDatabyid(id);
            return View(GetData);
        }
        [HttpPost]
        public ActionResult Edit(CategoryModel model)
        {
            CategoryModel getdata = new CategoryModel();
            getdata = Objadmin.UpdateCategory(model);
            if (getdata.message =="Success")
            {
                TempData["Message"] = model.message;
                TempData["Class"] = "success";
                ModelState.Clear();
                return RedirectToAction("GetCategory");
            }
            else
            {
                TempData["Message"] = model.message;
                TempData["Class"] = "primary";
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = string.Empty;
            message = Objadmin.DeleteCategory(id);
            if (message == "Successfully Deleted")
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "success";
                return RedirectToAction("GetCategory");
            }
            else
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "Primary";
                return RedirectToAction("GetCategory");
            }
            }
        [HttpGet]
        public ActionResult GetAllProduct(int? page)
        {
            int pagesize = 3;
            //int sqlpage;
            List<ProductModel> getprolist = new List<ProductModel>();
            //if (page == null)
            //{
            //    sqlpage = 0;
            //}
            //else
            //{
            //    sqlpage = Convert.ToInt32(page);
            //}   
            //getprolist = Objadmin.ShowProData(getprolist, Convert.ToInt32(sqlpage), pagesize);
            getprolist = Objadmin.ShowProData(getprolist);
            //for (int i = 0; i < 4; i++)
            //{
            //    ProductModel m = new ProductModel();
            //    getprolist.Add(m);
            //}


            return View(getprolist.ToList().ToPagedList(page ?? 1, pagesize));
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            ProductModel GetProData = new ProductModel();
            List<CategoryModel> CatList = new List<CategoryModel>();
            GetProData = Objadmin.GetProDatabyid(id); 

            CatList = Objadmin.GetAllCategory();
            List<Category> CategoryList = new List<Category>();

            foreach (var item in CatList)
            {
                Category Cat = new Category();
                Cat.CategoryName = item.CategoryName;
                Cat.CategoryId = item.CategoryId;
                CategoryList.Add(Cat);
            }
            GetProData.CategoryList = CategoryList; 

            return View(GetProData);
        }
        [HttpPost]
        public ActionResult EditProduct(ProductModel model)
        {
            try
            {
                model = Objadmin.UpdateData(model);

                if (model.message == "Success")
                {
                    ViewBag.Message = "";
                    ViewBag.Class = "";
                    TempData["Message"] = model.message;
                    TempData["Class"] = "success";
                    return RedirectToAction("GetAllProduct");
                }
                else
                {
                    ViewBag.Class = "primary";
                    ViewBag.Message = model.message;

                    return View(model);
                }
           
            
        }
            catch (Exception ex)
            {

                throw;
    }
}
            

        [HttpGet]
        public ActionResult DeletePro(int id)
        {
            string message = string.Empty;
            message = Objadmin.DeleteProcedure(id);
            if (message == "Successfully Deleted")
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "success";
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                TempData["DeleteMessage"] = message;
                TempData["Class"] = "Primary";
                return RedirectToAction("GetAllProduct");
            }
        }

        [HttpGet]
        public ActionResult paging()
        {
            List<ProductModel> getprolist = new List<ProductModel>();

           // getprolist = Objadmin.ShowProData(getprolist);
            ViewBag.getprolist = getprolist;
            return View(getprolist);
        }

        [HttpGet]
        public ActionResult GetAllProductList()
        {
            List<ProductModel> getprolist = new List<ProductModel>();

            //getprolist = Objadmin.ShowProData(getprolist);
            ViewBag.getprolist = getprolist;
            return View(getprolist);
        }

        [HttpGet]
        public ActionResult Dashboard()
           {

                return View();
            }
        
        }
    
}