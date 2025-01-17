﻿using CourseOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using CourseOnline.Global.Setting;
using Newtonsoft.Json.Linq;

namespace CourseOnline.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        [Route("MenuList")]
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return View("/Views/Error_404.cshtml");
            }
            else
            {
                VerifyAccController verifyAccController = new VerifyAccController();
                String result = verifyAccController.Menu(Session["Email"].ToString(), "No Permission");
                if (result.Equals("Student"))
                {
                    return View("/Views/Error_404.cshtml");
                }
                if (result.Equals("Reject"))
                {
                    return RedirectToAction("Home_CMS", "Home");
                }
                else
                {
                    return View("/Views/CMS/Menu/MenuList.cshtml");
                }
            }
        }
        [HttpPost]
        public ActionResult GetAllMenu()
        {
            if (Session["Email"] == null)
            {
                return null;
            }
            else
            {
                VerifyAccController verifyAccController = new VerifyAccController();
                String result = verifyAccController.Menu(Session["Email"].ToString(), "No Permission");
                if (result.Equals("Student"))
                {
                    return null;
                }
                if (result.Equals("Reject"))
                {
                    return null;
                }
                else
                {
                    int start = Convert.ToInt32(Request["start"]);
                    int length = Convert.ToInt32(Request["length"]);
                    string searchValue = Request["search[value]"];
                    string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                    string sortDirection = Request["order[0][dir]"];

                    using (STUDYONLINEEntities db = new STUDYONLINEEntities())
                    {
                        string sql = "select * from Menu";

                        List<Menu> Menus = db.Database.SqlQuery<Menu>(sql).ToList();

                        int totalrows = Menus.Count;
                        int totalrowsafterfiltering = Menus.Count;
                        Menus = Menus.Skip(start).Take(length).ToList();
                        Menus = Menus.OrderBy(sortColumnName + " " + sortDirection).ToList();
                        return Json(new { success = true, data = Menus, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult SearchByName(string type)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            using (STUDYONLINEEntities db = new STUDYONLINEEntities())
            {
                var menuList = (from m in db.Menus
                                   where m.menu_name.Contains(type)
                                   select new
                                   {
                                       m.menu_id,
                                       m.menu_name,
                                       m.menu_link,
                                       m.menu_order,
                                       m.menu_status,
                                       m.menu_description,
                                   }).ToList();
                int totalrows = menuList.Count;
                int totalrowsafterfiltering = menuList.Count;
                menuList = menuList.Skip(start).Take(length).ToList();
                menuList = menuList.OrderBy(sortColumnName + " " + sortDirection).ToList();
                return Json(new { success = true, data = menuList, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }
        }

        //filter by status
        [HttpPost]
        public ActionResult FilterByMenuStatus(string type)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            using (STUDYONLINEEntities db = new STUDYONLINEEntities())
            {
                if (!type.Equals(All.ALL_STATUS)) // filter theo status
                {
                    if (type.Equals(Status.ACTIVE))
                    {
                        var Menus = (from m in db.Menus
                                     where m.menu_status == true
                                     select new
                                     {
                                         m.menu_id,
                                         m.menu_name,
                                         m.menu_link,
                                         m.menu_order,
                                         m.menu_status,
                                         m.menu_description,
                                     }).ToList();

                        int totalrows = Menus.Count;
                        int totalrowsafterfiltering = Menus.Count;
                        Menus = Menus.Skip(start).Take(length).ToList();
                        Menus = Menus.OrderBy(sortColumnName + " " + sortDirection).ToList();
                        return Json(new { success = true, data = Menus, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var Menus = (from m in db.Menus
                                     where m.menu_status == false
                                     select new
                                     {
                                         m.menu_id,
                                         m.menu_name,
                                         m.menu_link,
                                         m.menu_order,
                                         m.menu_status,
                                         m.menu_description,
                                     }).ToList();

                        int totalrows = Menus.Count;
                        int totalrowsafterfiltering = Menus.Count;
                        Menus = Menus.Skip(start).Take(length).ToList();
                        Menus = Menus.OrderBy(sortColumnName + " " + sortDirection).ToList();
                        return Json(new { success = true, data = Menus, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                    }
                }
                else // lay ra tat ca
                {
                    string sql = "select * from Menu";

                    List<Menu> Menus = db.Database.SqlQuery<Menu>(sql).ToList();

                    int totalrows = Menus.Count;
                    int totalrowsafterfiltering = Menus.Count;
                    Menus = Menus.Skip(start).Take(length).ToList();
                    Menus = Menus.OrderBy(sortColumnName + " " + sortDirection).ToList();
                    return Json(new { success = true, data = Menus, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                }

            }
        }

        [HttpPost]
        public ActionResult deleteMenu(int id)
        {
            using (STUDYONLINEEntities db = new STUDYONLINEEntities())
            {
                var menu = db.Menus.Where(m => m.menu_id == id).FirstOrDefault();
                if (menu != null)
                {
                    if (menu.menu_status == true)
                    {
                        var rolemenu = db.RoleMenus.Where(rm => rm.menu_id == id).FirstOrDefault();
                        if (rolemenu != null)
                        {
                            db.RoleMenus.Remove(rolemenu);
                            db.SaveChanges();
                        }
                    }
                    db.Menus.Remove(menu);
                    db.SaveChanges();
                    return Json(new { success = true}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpGet]
        public ActionResult AddMenu()
        {

            using (STUDYONLINEEntities db = new STUDYONLINEEntities())
            {
                return View("/Views/CMS/Menu/AddMenu.cshtml");
            }
        }
        [HttpPost]
        public ActionResult SubmitAddMenu(string postJson)
        {
            try
            {
                using (STUDYONLINEEntities db = new STUDYONLINEEntities())
                {
                    dynamic addmenu = JValue.Parse(postJson);
                    string temp=null;

                    Menu m = new Menu();
                    m.menu_name = addmenu.menuName;
                    m.menu_link = addmenu.menuLink;
                    m.menu_order = addmenu.menuOrder;
                    temp = addmenu.menuStatus;
                    if (temp.Equals("Active"))
                    {
                        m.menu_status = true;
                    } else {
                        m.menu_status = false;
                    }
                    m.menu_description = addmenu.shortDes;
                    db.Menus.Add(m);
                    db.SaveChanges();
                    if(m.menu_status == true)
                    {
                        int id_new = db.Menus.DefaultIfEmpty().Max(me => me == null ? 0 : me.menu_id);
                        RoleMenu r = new RoleMenu();
                        r.role_id = 2;
                        r.menu_id = id_new;
                        r.roll_menu_status = false;
                        db.RoleMenus.Add(r);
                        db.SaveChanges();
                    }
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult MenuDetail(int id)
        {

            using (STUDYONLINEEntities db = new STUDYONLINEEntities())
            {
                Menu menu = db.Menus.Where(m => m.menu_id == id).FirstOrDefault();
                ViewBag.Menu = menu;
                ViewBag.id = id;
                return View("/Views/CMS/Menu/MenuDetail.cshtml");
            }
        }
        [HttpPost]
        public ActionResult SubmitEditMenu(string postJson)
        {
            try
            {
                using (STUDYONLINEEntities db = new STUDYONLINEEntities())
                {
                    string temp = null;
                    dynamic editmenu = JValue.Parse(postJson);
                    int id = editmenu.menuid;
                    Menu m = db.Menus.Where(menu => menu.menu_id == id).FirstOrDefault();
                    if (m != null)
                    {
                        m.menu_name = editmenu.menuName;
                        m.menu_link = editmenu.menuLink;
                        m.menu_order = editmenu.menuOrder;
                        temp = editmenu.menuStatus;
                        bool status = new bool();
                        if (temp.Equals("Active"))
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                        if(m.menu_status != status)
                        {
                            if (status == true)
                            {
                                RoleMenu me = new RoleMenu();
                                me.role_id = 2;
                                me.menu_id = id;
                                me.roll_menu_status = false;
                                db.RoleMenus.Add(me);
                            }
                            else
                            {
                                var rolemenu = db.RoleMenus.Where(rm => rm.menu_id == id).FirstOrDefault();
                                if (rolemenu != null)
                                {
                                    db.RoleMenus.Remove(rolemenu);
                                }
                            }
                        }
                        m.menu_status = status;
                        m.menu_description = editmenu.shortDes;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}