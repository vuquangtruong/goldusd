using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GoldUSD.Data.Models;
using GoldUSD.Data.Services;
using GoldUSD.Models;
using GoldUSD.Utilities.Common;

namespace GoldUSD.Controllers
{
    [AuthorizeEx(new[] { AppConstant.RoleAdministrator })]
    public class AdminController : Controller
    {
        private readonly IPriceTypeService _priceTypeService;

        private readonly IPriceService _priceService;

        private readonly IUserService _userService;

        public AdminController(IPriceTypeService priceTypeService, IPriceService priceService, IUserService userService)
        {
            _priceTypeService = priceTypeService;
            _priceService = priceService;
            _userService = userService;
        }

        public ActionResult PriceManagement()
        {
            var model = new PriceManagementModel
                            {
                                PriceTypes = GetPriceTypes()
                            };
            return View(model);
        }

        public ActionResult PriceGrid()
        {
            var firstPriceType = _priceTypeService.DbSet.FirstOrDefault();
            var model = new PriceGridModel()
            {
                Prices = _priceService.DbSet.Where(p => p.PriceTypeId == firstPriceType.Id).ToList()
            };
            return PartialView("_PriceGridPartial", model);
        }

        [HttpPost]
        public ActionResult PriceGrid(PriceManagementModel model)
        {
            if (model.DeletePriceId != null)
            {
                _priceService.Delete(model.DeletePriceId.Value);
                _priceService.SaveChanges();
            }
            var priceGridModel = new PriceGridModel()
                                     {
                                         Prices =
                                             _priceService.DbSet.Where(p => p.PriceTypeId == model.SelectedPriceType).
                                             ToList()
                                     };
            return PartialView("_PriceGridPartial", priceGridModel);
        }

        public ActionResult EditPrice(int? id)
        {
            var model = new EditPriceModel();
            model.PriceTypes = GetPriceTypes();
            if (id.HasValue)
            {
                var price = _priceService.Find(id.Value);
                model.Id = id.Value;
                model.Symbol = price.Symbol;
                model.BuyingPrice = price.BuyingPrice;
                model.SellingPrice = price.SellingPrice;
                model.PriceTypeId = price.PriceTypeId;
            }
            return PartialView("_EditPricePartial", model);
        }

        [HttpPost]
        public ActionResult EditPrice(EditPriceModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PriceTypes = GetPriceTypes();
                return PartialView("_EditPricePartial", model);
            }
            
            var price = model.Id == null ? new Price() : _priceService.Find(model.Id.Value);
            price.Symbol = model.Symbol.ToUpper();
            price.BuyingPrice = model.BuyingPrice;
            price.SellingPrice = model.SellingPrice;
            price.PriceTypeId = model.PriceTypeId;
            if (model.Id == null)
            {
                if (_priceService.DbSet.Any(p => p.Symbol.ToLower() == model.Symbol.ToLower() && p.PriceTypeId == model.PriceTypeId))
                {
                    ModelState.AddModelError(string.Empty, "Kí hiệu này đã có trong hệ thống.");
                    model.PriceTypes = GetPriceTypes();
                    return PartialView("_EditPricePartial", model);
                }
                _priceService.Insert(price);
                _priceService.SaveChanges();
            }
            else
            {
                if (price.Symbol.ToLower() != model.Symbol.ToLower())
                {
                    if (_priceService.DbSet.Any(p => p.Symbol.ToLower() == model.Symbol.ToLower() && p.PriceTypeId == model.PriceTypeId))
                    {
                        ModelState.AddModelError(string.Empty, "Kí hiệu này đã có trong hệ thống.");
                        model.PriceTypes = GetPriceTypes();
                        return PartialView("_EditPricePartial", model);
                    }
                }
                _priceService.Update(price);
                _priceService.SaveChanges();
            }
            TempData["SaveSuccess"] = true;
            return RedirectToAction("EditPrice");
        }

        public ActionResult UserManagement()
        {
            return View();
        }

        public ActionResult UserList()
        {
            var model = new UserListModel();
            return PartialView("_UserListPartial", model);
        }

        [HttpPost]
        public ActionResult UserList(UserListModel model)
        {
            if (model.DeleteUserId != null)
            {
                _userService.Delete(model.DeleteUserId);
                _userService.SaveChanges();
                Membership.DeleteUser(Membership.GetUser(model.DeleteUserId).UserName, true);
            }
            return RedirectToAction("UserGrid", new { username = model.SeachUsername });
        }

        public ActionResult UserGrid(string username)
        {
            var user = _userService.DbSet.OrderBy(u => u.AspnetUser.UserName).ToList();
            if (!string.IsNullOrEmpty(username))
            {
                user = user.Where(u => u.AspnetUser.UserName.Contains(username)).ToList();
            }
            var model = new UserGridModel()
                            {
                                Users = user
                            };
            return PartialView("_UserGridPartial", model);
        }

        public ActionResult EditUser(Guid? id)
        {
            var model = new EditUserModel();
            if (id.HasValue)
            {
                var user = _userService.Find(id.Value);
                model.UserId = user.Id;
                model.Username = user.AspnetUser.UserName;
                model.ExpireDate = user.ExpireDate;
            }
            return PartialView("_EditUserPartial", model);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_EditUserPartial", model);
            }
            var user = model.UserId == null ? new User() : _userService.Find(model.UserId);
            if (model.UserId == null)
            {
                if (Membership.GetUser(model.Username) != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập này đã có trong hệ thống");
                    return PartialView("_EditUserPartial", model);
                }
                var member = Membership.CreateUser(model.Username, model.Password);
                Roles.AddUserToRole(model.Username, AppConstant.RoleUser);
                user.Id = Guid.Parse(member.ProviderUserKey.ToString());
                user.ExpireDate = DateTime.Now.AddMonths(6);
                _userService.Insert(user);
                _userService.SaveChanges();
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var member = Membership.GetUser(model.Username);
                    var oldPassword = member.ResetPassword();
                    member.ChangePassword(oldPassword, model.Password);
                }
                if (model.IsExtend)
                {
                    //If not yet expire
                    if (user.ExpireDate > DateTime.Now)
                    {
                        user.ExpireDate = user.ExpireDate.AddMonths(6);
                    }
                    else
                    {
                        user.ExpireDate = DateTime.Now.AddMonths(6);
                    }
                    _userService.SaveChanges();
                }
            }
            TempData["SaveSuccess"] = true;
            return RedirectToAction("EditUser");
        }

        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var member = Membership.GetUser(User.Identity.Name);
            member.ChangePassword(member.ResetPassword(), model.Password);
            model = new ChangePasswordModel();
            ViewBag.SaveSuccess = true;
            return View(model);
        }

        private List<SelectListItem> GetPriceTypes()
        {
            var priceTypes = _priceTypeService.DbSet.ToList();
            var selectList = new List<SelectListItem>();
            foreach (var priceType in priceTypes)
            {
                selectList.Add(new SelectListItem { Text = priceType.Name, Value = priceType.Id.ToString() });
            }
            return selectList;
        }
    }
}
