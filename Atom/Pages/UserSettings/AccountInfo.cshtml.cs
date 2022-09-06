using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Atom.Base;
using Atom.Data.Model.Security;
using Atom.Domain.Attribute;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Atom.Pages.UserSettings
{
    public class AccountInfoModel : PageModelBase
    {
        [BindProperty]
        public UserModel? UserModel { get; set; }

        //[BindProperty]
        //public IEnumerable<UserModel?> ManyUsers { get; set; }

        //[BindProperty]
        //public List<UserModel?> UpdateManyUsers { get; set; }


        [BindProperty]
        //[Manage]
        public UserModel? FoundUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? FoundUserGuid { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [EmailAddress]
        [Required]
        [DataType(dataType: DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [BindProperty]
        public UserModel NewUser { get; set; }


        public AccountInfoModel(IServiceProvider provider) : base(provider) { }
        /*
        public void OnGet()
        {
            NewUser = new();

            if (FoundUserGuid.HasValue)
            {
                FoundUser = SecurityManager.GetUser(FoundUserGuid.Value);
            }
        }
        
        public async Task<IActionResult> OnPostLoginUser()
        {
            //if (ModelState.IsValid)
            //{
            //    FoundUser = UserModel = await Task.Run(() => SecurityManager.LoginUser(emailAddress: EmailAddress, password: Password));
            //}

            FoundUser = UserModel = await Task.Run(() => SecurityManager.LoginUser(emailAddress: EmailAddress, password: Password));
            FoundUserGuid = FoundUser?.UserGuid;
            return Redirect($"/test/?{nameof(FoundUserGuid)}={FoundUserGuid}");
            //return RedirectToAction("/test/", new { FoundUserGuid });
        }

        public async Task<IActionResult> OnPostGetAllUsers()
        {

            ManyUsers = await Task.Run(() => SecurityManager.GetUserModels());
            UpdateManyUsers = (List<UserModel>)ManyUsers;
            ModelState.Clear();
            return Page();
        }
        */

        public async Task<IActionResult> OnPostCreateNewUser()
        {

            await Task.Run(() => SecurityManager.CreateNewUser(NewUser));
            ModelState.Clear();
            return Page();
        }

        /*public async Task<IActionResult> OnPostGetUser()
        {

            UserModel = await Task.Run(() => SecurityManager.GetUser));
            UpdateManyUsers = (List<UserModel>)ManyUsers;
            ModelState.Clear();
            return Page();
        }*/

        
        public async Task<IActionResult> OnPostUpdateUser()
        {
            await Task.Run(() => SecurityManager.SaveUser(FoundUser));
            ModelState.Clear();
            return Page();
        }
        

        /*
        public async Task<IActionResult> OnPostUpdateAllUsers()
        {
            foreach (var u in UpdateManyUsers)
            {
                u.CreatedByUser = 1;
            }
            await Task.Run(() => SecurityManager.UpdateManyUsers(UpdateManyUsers));

            return Redirect("/test");
        }*/
    }

}

