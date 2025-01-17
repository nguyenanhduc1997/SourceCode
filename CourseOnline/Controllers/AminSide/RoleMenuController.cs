﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using CourseOnline.Models;
using Newtonsoft.Json.Linq;

namespace CourseOnline.Controllers
{
    public class RoleMenuController : Controller
    {
        // GET: RoleMenu
        [Route("RoleMenuList")]
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
                    return View("/Views/CMS/RoleMenuList.cshtml");
                }
            }
        }
        [HttpPost]
        public ActionResult GetAllRoleMenu()
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
                        string sql = " select rm.role_menu_id, m.menu_name, m.menu_link, rm.roll_menu_status " +
                                        "from RoleMenu rm join Menu m " +
                                        "on rm.menu_id = m.menu_id";

                        List<RoleMenuModel> RoleMenus = db.Database.SqlQuery<RoleMenuModel>(sql).ToList();

                        int totalrows = RoleMenus.Count;
                        int totalrowsafterfiltering = RoleMenus.Count;
                        RoleMenus = RoleMenus.Skip(start).Take(length).ToList();
                        RoleMenus = RoleMenus.OrderBy(sortColumnName + " " + sortDirection).ToList();
                        return Json(new { success = true, data = RoleMenus, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult ChangeMenuStatus(string postJson)
        {
            try
            {
                using (STUDYONLINEEntities db = new STUDYONLINEEntities())
                {
                    dynamic changeStatus = JValue.Parse(postJson);
                    int id = changeStatus.roleMenuId;

                    RoleMenu r = db.RoleMenus.Where(ro => ro.role_menu_id == id).FirstOrDefault();
                    if (r != null)
                    {
                        r.roll_menu_status = changeStatus.RoleMenuStatus;
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