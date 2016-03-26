using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using PuzzleAdv.Backend.Interfaces;
using PuzzleAdv.Backend.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PuzzleAdv.Backend.Components
{
    [ViewComponent(Name = "Navigation")]
    public class Navigation : ViewComponent
    {
        private readonly IAuthorizationService _authz;
        private readonly IShopRepository _shopRepository;

        public Navigation(IAuthorizationService authz, IShopRepository shopRepository)
        {
            _authz = authz;
            _shopRepository = shopRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal user)
        {
            NavigationViewModel navigationViewModel = new NavigationViewModel()
            {
                UserIsAdmin = await _authz.AuthorizeAsync(user, "AdminOnly"),
                UserHasShop = await _shopRepository.UserHasShopAsync(user)
            };

            return View(navigationViewModel);
        }
    }
}
