using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projekt2_Group3.Models;

namespace Projekt2_Group3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(login lg)
        {
            if (ModelState.IsValid)
            {
                using (UserDBEntities1 ue = new UserDBEntities1())
                {
                    var log = ue.portal_user.Where(t => t.user_name.Equals(lg.username) && t.password.Equals(lg.password)).FirstOrDefault();
                    if (log != null)
                    {
                        if (lg.isEmployee && !log.employee)
                        {
                            Response.Write("<script> alert('Invalid username or password or role')</script>");
                            return View();
                        }

                        if (!lg.isEmployee && log.employee)
                        {
                            Response.Write("<script> alert('Invalid username or password or role')</script>");
                            return View();
                        }

                        if (!log.employee)
                        {
                            Session["username"] = log.user_name;
                            Session["user_id"] = log.user_id;
                            return RedirectToAction("UserHomepage", "Home");
                        }
                        else
                        {
                            Session["username"] = log.user_name;
                            Session["user_id"] = log.user_id;
                            return RedirectToAction("EmployeeHomepage", "Home");
                        }
                    }

                    else
                    {
                        Response.Write("<script> alert('Invalid username or password or role')</script>");
                    }
                }
            }
            return View();
        }

        public ActionResult UserHomepage()
        {
            return View();
        }
        public ActionResult EmployeeHomepage()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Signup()
        {
            return View("Signup");
        }
        [HttpPost]
        public ActionResult Signedup()
        {
            return View("Login");
        }
        public ActionResult FAQ()
        {
            return View("FAQ");
        }
        public ActionResult EditProfile()
        {
            using (UserDBEntities1 ue = new UserDBEntities1())
            {
                var user_name = Session["username"].ToString();
                var user = ue.portal_user.Where(t => t.user_name.Equals(user_name)).FirstOrDefault();
                if (user == null)
                {
                    Response.Write("<script> alert('Invalid user')</script>");
                    return View();
                }    
                return View(user);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(portal_user profileUpdate)
        {
            using (UserDBEntities1 ue = new UserDBEntities1())
            {

                var user_name = Session["username"].ToString();
                if (user_name == null)
                {
                    Response.Write("<script> alert('Invalid user')</script>");
                }
                else
                {
                    var user = ue.portal_user.Where(t => t.user_name.Equals(user_name)).FirstOrDefault();
                    if (user != null)
                    {
                        user.first_name = profileUpdate.first_name;
                        user.last_name = profileUpdate.last_name;
                        user.email = profileUpdate.email;
                        user.adress = profileUpdate.adress;
                        user.mobile = profileUpdate.mobile;
                        ue.SaveChanges();
                        return View();
                    }
                }
                return View();
            }

        }

        [AcceptVerbs("Get", "Post")]
        public ActionResult Invoice(String keyword)
        {

            using (UserDBEntities1 ue = new UserDBEntities1())
            {
                var user_name = Session["username"].ToString();
                var user = ue.portal_user.Where(t => t.user_name.Equals(user_name)).FirstOrDefault();
                if (user == null)
                {
                    Response.Write("<script> alert('Invalid user')</script>");
                    return View();
                }
                else
                {
                    var invoiceList = new List<invoice>(); 
                    if(keyword == null || "".Equals(keyword))
                    {
                        invoiceList = ue.invoices.Where(i => i.user_id == user.user_id).ToList();
                    }   
                    else
                    {
                        invoiceList = ue.invoices
                            .Where(i => i.user_id == user.user_id)
                            .Where(i => i.invoice_id.Contains(keyword))
                            .ToList();
                    }    
                    invoice_model result = new invoice_model();
                    result.invoices = invoiceList;
                    result.keyword = keyword;
                    return View(result);
                }
               
            }
        }

        [AcceptVerbs("Get", "Post")]
        public ActionResult faq(String keyword)
        {

            using (UserDBEntities1 ue = new UserDBEntities1())
            {
                var knowledges = new List<knowledge>();
                if (keyword == null || "".Equals(keyword))
                {
                    knowledges = ue.knowledges.ToList();
                }
                else
                {
                    knowledges = ue.knowledges
                        .Where(i => i.question.Contains(keyword) || i.solution.Contains(keyword))
                        .ToList();
                }
                faq_model result = new faq_model();

                var data = knowledges.GroupBy(k => k.category).ToDictionary( group => group.Key, group => group.ToList());

                result.data = data;
                result.keyword = keyword;
                return View(result);

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("abc");
        }
    }
}