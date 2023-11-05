using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thrift_Fashion.ExtraClasses;
using Thrift_Fashion.Models;

namespace Thrift_Fashion.Controllers
{
    public class HomeController : Controller
    {
        fashionEntities db = new fashionEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Women = db.products.ToList().Where(x => x.P_Type == "Women");
            ViewBag.Men = db.products.ToList().Where(x => x.P_Type == "Men");
            ViewBag.Shoes = db.products.ToList().Where(x => x.P_Type == "Shoes");
            ViewBag.Collection = db.products.ToList().Take(5);
            var sixproducts = db.products.ToList().Take(6);
            return View(sixproducts);
        }

        public ActionResult Shop()
        {
            ViewBag.options = db.products.ToList().Take(4);
            Session["total"] = db.products.Count();
            return View(db.products.ToList());
        }

        public ActionResult Collection()
        {
            return View(db.Collections.ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Singlepage(int id)
        {
            var mixtabs = new Class2
            {
                Product = db.products.Where(x => x.P_ID == id).FirstOrDefault(),
               Reviews = db.Reviews.ToList().Where(x => x.R_ProID == id),
               WOMENPRODUCT = db.products.ToList().Where(x => x.P_Type == "Women").Take(5),
               MENPRODUCT = db.products.ToList().Where(x => x.P_Type == "Men").Take(5)
        };
            Session["two"] = db.Reviews.Where(x => x.R_ProID == id).Count();
            return View(mixtabs);

        }
        public ActionResult review(int id)
        {

            return View();
        }
        [HttpPost]
        public  ActionResult review(Review rev)
        {
            db.Reviews.Add(rev);
            db.SaveChanges();
            return RedirectToAction("Shop");
        }
        public ActionResult SingleCollection(string name)
        {
            ViewBag.Women = db.products.ToList().Where(x => x.P_Type == "Womens").Take(5);
            ViewBag.Men = db.products.ToList().Where(x => x.P_Type == "Mens").Take(5);
            Session["total"] = db.products.Where(x => x.P_Type == name).Count();
            return View(db.products.Where(x => x.P_Type == name).ToList());
        }
        public ActionResult Cart()
        {
            if (Session["cart"] != null)
            {
                List<collect> mainlist = (List<collect>)Session["cart"];
                List<cartapp> viewlist = new List<cartapp>();
                foreach (var item in mainlist)
                {
                    cartapp obj = new cartapp();
                    product prop = db.products.Where(x => x.P_ID == item.P_ID).FirstOrDefault();
                    obj.P_ID = prop.P_ID;
                    obj.P_Name = prop.P_Name;
                    obj.qty = item.P_Qty;
                    obj.P_Pic = prop.P_Pic;
                    obj.P_Price = Convert.ToInt32(prop.P_Discount);
                    obj.P_Totalprice = item.P_Qty * prop.P_Discount ?? 0;
                    viewlist.Add(obj);
                }
                Session["num"] = mainlist.Count();
                return View(viewlist);
            }
            else
            {

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public JsonResult addtocart(int id)
        {
            if (Session["cart"] != null)
            {
                List<collect> mainlist =( List<collect>) Session["cart"];
                int check = 0;
                foreach (var item in mainlist)
                {
                    if(item.P_ID == id)
                    {
                        item.P_Qty = item.P_Qty + 1;
                        check = 0;
                        break;
                    }
                    else
                    {
                        check = 1;
                    }
                }
                if (check == 1)
                {
                    collect obj = new collect();
                    obj.P_ID = id;
                    obj.P_Qty = 1;
                    mainlist.Add(obj);
                }
                Session["cart"] = (List<collect>)mainlist; 
            }
            else
            {
                List<collect> firstlist = new List<collect>();
                collect obj = new collect();
                obj.P_ID = id;
                obj.P_Qty = 1;
                firstlist.Add(obj);
                Session["cart"] = firstlist;
            }
            return Json(Session["cart"], JsonRequestBehavior.AllowGet);
        }
        public ActionResult cartincrese(int id)
        {
            List<collect> mainlist = (List<collect>)Session["cart"];
            int qty = 0;
            for (int i = 0; i < mainlist.Count; i++)
            {
                if (mainlist[i].P_ID == id)
                {
                    mainlist[i].P_Qty = mainlist[i].P_Qty + 1;
                    qty = mainlist[i].P_Qty;
                    break;
                }
            }
            Session["cart"] = (List<collect>) mainlist;
            return Json(qty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cartdcs(int id)
        {
            List<collect> mainlist = (List<collect>)Session["cart"];
            int qty = 0;
            for (int i = 0; i < mainlist.Count; i++)
            {
                if (mainlist[i].P_ID == id)
                {    if (mainlist[i].P_Qty > 0)
                    {
                        mainlist[i].P_Qty = mainlist[i].P_Qty - 1;
                        qty = mainlist[i].P_Qty;
                        break;
                    }
                    else
                    {
                        qty = 1;
                        break;
                    }

                }
            }
            Session["cart"] = (List<collect>)mainlist;
            return Json(qty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Checkout()
        {
            List<collect> mainlist = (List<collect>)Session["cart"];
            List<cartapp> viewlist = new List<cartapp>();
            foreach (var item in mainlist)
            {
                cartapp obj = new cartapp();
                product prop = db.products.Where(x => x.P_ID == item.P_ID).FirstOrDefault();
                obj.P_ID = prop.P_ID;
                obj.P_Name = prop.P_Name;
                obj.qty = item.P_Qty;
                obj.P_Pic = prop.P_Pic;
                obj.P_Price = Convert.ToInt32(prop.P_Discount);
                obj.P_Totalprice = item.P_Qty * prop.P_Discount ?? 0;
                viewlist.Add(obj);
            }
            return View(viewlist);
        }

        public ActionResult MyAccount()
        {
            var finalID = Session["Email"];
            var account = db.Billings.Where(x => x.B_email == finalID).ToList();
            return View(account);
        }
        public ActionResult BillRemove(int? id)
        {

                var retur = db.Billings.Where(x => x.B_id == id).FirstOrDefault();
                db.Billings.Remove(retur);
            db.SaveChanges();
            
            return RedirectToAction("MyAccount");
        }
        public ActionResult additional()
        {

            return View();
        }
        [HttpPost]
        public ActionResult additional(Billing bil)
        {
            
            db.Billings.Add(bil);
            db.SaveChanges();
            Session["num"] = null;
            Session["cart"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            if (Session["UserLogin"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(user usr)
        {
            if (ModelState.IsValid)
            {

                var obj = db.users.Where(a => a.U_Email.Equals(usr.U_Email) && a.U_Password.Equals(usr.U_Password)).FirstOrDefault();
                if (obj != null)
                {

                    if (usr.U_Password == obj.U_Password && usr.U_Email == obj.U_Email)
                    {
                        Session["UserLogin"] = obj.U_id.ToString();
                        Session["Name"] = obj.U_Name.ToString();
                        Session["Email"] = obj.U_Email.ToString();
                        Session["u_id"] = obj.U_id.ToString();
                        return RedirectToAction("Index");
                    }
                }

            }
            Session["Wrongpass"] = 1;
            return View();
        }


        public ActionResult Logout()
        {
            if (Session["UserLogin"] != null)
            {
                Session["UserLogin"] = null;
                Session["num"] = null;
            }
            return RedirectToAction("Index");
        }


        public ActionResult Register()
        {
            if (Session["UserLogin"] == null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public ActionResult Register(user usr, HttpPostedFileBase file)
        {
            if (Session["UserLogin"] == null)
            {
                if (file == null)
                {
                    string defaultImage = "man.jpg";
                    usr.U_Pic = defaultImage;
                    db.users.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    usr.U_Pic = file.FileName;
                    db.users.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Index");
        }


    }
}