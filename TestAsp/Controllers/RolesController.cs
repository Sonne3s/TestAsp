using System.Web;
using TestAsp.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TestAsp.ViewModels;
using TestAsp.DAL;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestAsp.Controllers
{
    [Authorize(Roles="Admin")]
    public class RolesController : Controller
    {
        // GET: Roles
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        [HttpPost]
        public ActionResult Index(UsersWithRoles model, string id)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new EFDbContext()));
            userManager.AddToRole(id, model.Role);

            return View(new UsersWithRoles(new EFDbContext()));
        }
        public ActionResult Untie(string role, string id)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new EFDbContext()));
            userManager.RemoveFromRole(id,role);

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            return View(new UsersWithRoles(new EFDbContext()));
            //return View(RoleManager.Roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                    {
                        Name = model.Name,
                        Description = model.Description
                    });
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            return View(model);
        }

        // GET: Roles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new EditRoleModel { Id=role.Id, Name=role.Name, Description=role.Description});
            }
            return RedirectToAction("Index");
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
            }
            return View(model);
        }

        // GET: Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if(role!=null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }        
    }
}
