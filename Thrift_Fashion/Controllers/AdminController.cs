using Thrift_Fashion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thrift_Fashion.ExtraClasses;
using System.Data.Entity;
using System.IO;

namespace Thrift_Fashion.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        fashionEntities db = new fashionEntities();

        //Login Workaround
        public ActionResult Login()
        {
            if (Session["AdminLogin"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(admin admins)
        {
            if (ModelState.IsValid)
            {

                var obj = db.admins.Where(a => a.A_Email.Equals(admins.A_Email) && a.A_Password.Equals(admins.A_Password)).FirstOrDefault();
                if (obj != null)
                {

                    if (admins.A_Password == obj.A_Password && admins.A_Email == obj.A_Email)
                    {
                        Session["UsersCount"] = db.users.Count();
                        Session["ProductsCount"] = db.products.Count();
                        Session["AdminsCount"] = db.admins.Count();
                        Session["BillingsCount"] = db.Billings.Count();


                        @Session["AdminImage"] = obj.A_Pic.ToString();
                        Session["AdminLogin"] = obj.A_id.ToString();
                        Session["AdminAccess"] = obj.A_Email.ToString();
                        Session["AdminName"] = obj.A_Name.ToString();

                        Session["Welcome"] = "Welcome Mr " + obj.A_Name.ToString();

                        return RedirectToAction("Index");
                    }
                }

            }
            Session["Wrongpass"] = 1;
            return View(admins);
        }

        // Logout Workaround
        public ActionResult Logout()
        {
            if (Session["AdminLogin"] != null)
            {

                Session["UsersCount"] = null;
                Session["ProductsCount"] = null;
                Session["AdminsCount"] = null;
                Session["AdminAccess"] = null;

                Session["AdminLogin"] = null;
                Session["AdminName"] = null;

            }
            return RedirectToAction("Index");
        }

        //Access Dashboard After Being Successfully Login to Admin Dashboard
        public ActionResult Index()
        {
            if (Session["AdminLogin"] != null)
            {
                Session["Wrongpass"] = 0;

                var mixtabs = new listTables
                {
                    Billings = db.Billings.ToList().Take(5),
                    Products = db.products.ToList().Take(5),
                    Users = db.users.ToList().Take(5),
                    Admins = db.admins.ToList().Take(5)
                };
                return View(mixtabs);
            }
            return RedirectToAction("Login");
        }
        //show Categories
        public ActionResult collections()
        {
            if (Session["AdminLogin"] != null)
            {
                return View(db.Collections.ToList());
            }
            return RedirectToAction("Login");
        }


        public ActionResult AddCollection()
        {
            if (Session["AdminLogin"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]

        public ActionResult AddCollection(Collection usr, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    string defaultImage = "man.jpg";
                    usr.C_Pic = defaultImage;
                    db.Collections.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Collection");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    usr.C_Pic = file.FileName;
                    db.Collections.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Collections");
                }
            }
            return RedirectToAction("Login");
        }
        // Remove collection
        public ActionResult CollectionRemove(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var retur = db.Collections.Where(x => x.C_ID == id).FirstOrDefault();
                db.Collections.Remove(retur);
                db.SaveChanges();
                return RedirectToAction("Collections");
            }
            return RedirectToAction("Login");
        }

        // Show Billings
        public ActionResult Billings()
        {
            if (Session["AdminLogin"] != null)
            {
                return View(db.Billings.ToList());
            }
            return RedirectToAction("Login");
        }

        // Remove Billings
        public ActionResult BillingsRemove(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.Billings.Where(x => x.B_id == id).FirstOrDefault();
                db.Billings.Remove(returnID);
                db.SaveChanges();
                return RedirectToAction("Billings");
            }
            return RedirectToAction("Login");
        }

        //Collection Edit
        public ActionResult CollectionEdit(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.Collections.Where(x => x.C_ID == id).FirstOrDefault();
                return View(returnID);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult CollectionEdit(Collection bills, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    string defaultImage = "man.jpg";
                    bills.C_Pic = defaultImage;
                    db.Collections.Add(bills);
                    db.SaveChanges();
                    return RedirectToAction("Collection");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    bills.C_Pic = file.FileName;
                    db.Collections.Add(bills);
                    db.SaveChanges();
                    return RedirectToAction("Collection");
                }
            }
            return View();
        }
        //Billings Edit
        public ActionResult BillingsEdit(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.Billings.Where(x => x.B_id == id).FirstOrDefault();
                return View(returnID);
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult BillingsEdit(Billing bills)
        {
            if (Session["AdminLogin"] != null)
            {
                db.Entry(bills).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Billings");
            }
            return RedirectToAction("Login");
        }
        public ActionResult Users()
        {
            if (Session["AdminLogin"] != null)
            {

                return View(db.users.ToList());
            }
            return RedirectToAction("Login");
        }


        public ActionResult AddUsers()
        {
            if (Session["AdminLogin"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]

        public ActionResult AddUsers(user usr, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    string defaultImage = "man.jpg";
                    usr.U_Pic = defaultImage;
                    db.users.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    usr.U_Pic = file.FileName;
                    db.users.Add(usr);
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult EditUser(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.users.Where(x => x.U_id == id).FirstOrDefault();
                Session["ImageEmp"] = returnID.U_id;
                return View(returnID);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]

        public ActionResult EditUser(user usr, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    usr.U_Pic = Session["ImageEmp"].ToString();
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    usr.U_Pic = file.FileName;
                    db.Entry(usr).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Remove_User(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.users.Where(x => x.U_id == id).FirstOrDefault();
                db.users.Remove(returnID);
                db.SaveChanges();
                return RedirectToAction("Users");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Products()
        {
            if (Session["AdminLogin"] != null)
            {
                return View(db.products.ToList());
            }

            return RedirectToAction("Login");
        }


        public ActionResult AddProducts()
        {
            if (Session["AdminLogin"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]

        public ActionResult AddProducts(product pds, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    string defaultImage = "man.jpg";
                    pds.P_Pic = defaultImage;
                    db.products.Add(pds);
                    db.SaveChanges();
                    return RedirectToAction("Products");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    pds.P_Pic = file.FileName;
                    db.products.Add(pds);
                    db.SaveChanges();
                    return RedirectToAction("Products");
                }
            }
            return RedirectToAction("Login");
        }


        public ActionResult EditProducts(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.products.Where(x => x.P_ID == id).FirstOrDefault();
                Session["ImageExp"] = returnID.P_Pic;
                return View(returnID);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult EditProducts(product pds, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    pds.P_Pic = Session["ImageExp"].ToString();
                    db.Entry(pds).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Products");
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    pds.P_Pic = file.FileName;
                    db.Entry(pds).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Products");
                }
            }
            return RedirectToAction("Login");
        }


        public ActionResult RemoveProducts(int? id)
        {
            if (Session["AdminLogin"] != null)
            {

                var returnID = db.products.Where(x => x.P_ID == id).FirstOrDefault();
                db.products.Remove(returnID);
                db.SaveChanges();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Login");
        }


        public ActionResult Admins()
        {
            if (Session["AdminLogin"] != null)
            {
                return View(db.admins.ToList());
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddAdmins()
        {
            if (Session["AdminLogin"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        [HttpPost]

        public ActionResult AddAdmins(admin ad, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    string defaultImage = "admin.jpg";
                    ad.A_Pic = defaultImage;
                    db.admins.Add(ad);
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }

                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    ad.A_Pic = file.FileName;
                    db.admins.Add(ad);
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult EditAdmins(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.admins.Where(x => x.A_id == id).FirstOrDefault();
                Session["ImageAdmin"] = returnID.A_id;
                return View(returnID);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]

        public ActionResult EditAdmins(admin ad, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    ad.A_Pic = Session["ImageAdmin"].ToString();
                    db.Entry(ad).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    ad.A_Pic = file.FileName;
                    db.Entry(ad).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult RemoveAdmins(int? id)
        {
            if (Session["AdminLogin"] != null)
            {
                var returnID = db.admins.Where(x => x.A_id == id).FirstOrDefault();
                db.admins.Remove(returnID);
                db.SaveChanges();
                return RedirectToAction("Admins");
            }

            return RedirectToAction("Login");
        }



        public ActionResult Change_Password()
        {
            if (Session["AdminLogin"] != null)
            {
                string LoginEmail = Session["AdminAccess"].ToString();
                var returnID = db.admins.Where(x => x.A_Email == LoginEmail).FirstOrDefault();
                Session["ImageAdminNew"] = returnID.A_Pic;
                return View(returnID);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Change_Password(admin ad, HttpPostedFileBase file)
        {
            if (Session["AdminLogin"] != null)
            {
                if (file == null)
                {
                    ad.A_Pic = Session["ImageAdminNew"].ToString();
                    db.Entry(ad).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/SiteUpload/"), file.FileName);
                    file.SaveAs(path);
                    ad.A_Pic = file.FileName;
                    db.Entry(ad).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Admins");
                }
            }
            return RedirectToAction("Login");
        }




    }
}