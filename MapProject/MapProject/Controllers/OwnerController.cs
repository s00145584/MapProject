using MapProject.DAL;
using MapProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MapProject.Controllers
{
    public class OwnerController : Controller
    {
        private ILocationRepo repo;

        public OwnerController()
        {
            repo = new LocationRepo(new LocationContext());
        }

        // GET: Owner
        public ActionResult Index(string OwnerId)
        {
            return View(repo.GetByOwnerID(OwnerId));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPlace(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    using (connection)
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.addUserRole", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = model.Email;
                            cmd.Parameters.Add("@roleId", SqlDbType.VarChar).Value = model.RoleId;

                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return RedirectToAction("LogOff", "Account");
                    //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}