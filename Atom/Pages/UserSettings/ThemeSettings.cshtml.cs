using Atom.Base;
using Atom.Data.Model.CSS;
using Atom.Domain.Enum;
using Atom.Domain.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Atom.Pages.UserSettings
{
    [BindProperties]
    [Authorize(Roles = "Standard")]
    public class ThemeSettingsModel : PageModelBase
    {
        [BindProperty]
        public IEnumerable<CSSRuleModel> CSSRuleModel { get; set; }

        [BindProperty]
        [Display(Name = "Background Color")]
        public CSSRuleModel BackgroundColor { get; set; }

        [BindProperty]
        public CSSRuleModel FooterColor { get; set; }

        [BindProperty]
        public CSSRuleModel TextColor { get; set; }

        [BindProperty]
        public CSSRuleModel ButtonColor { get; set; }

        [TempData]
        public bool IsNew
        {
            get;set; 
        }

        public ThemeSettingsModel(IServiceProvider provider) : base(provider)
        {
        }

        public void OnGet()
        {
            int userId;

            if (IsLoggedIn)
            {
                userId = LoggedInUser.UserId;
            }
            else
            {
                userId = 0;
            }

            CSSRuleModel = CSSManager.GetCSS(userId);

            BackgroundColor = CSSRuleModel.First(bgColor => bgColor.CSSVariableId == Domain.Enum.CSSVariableEnum.BackgroundColor);
            FooterColor = CSSRuleModel.First(footerColor => footerColor.CSSVariableId == Domain.Enum.CSSVariableEnum.FooterColor);
            TextColor = CSSRuleModel.First(textColor => textColor.CSSVariableId == Domain.Enum.CSSVariableEnum.TextColor);
            ButtonColor = CSSRuleModel.First(buttonColor => buttonColor.CSSVariableId == Domain.Enum.CSSVariableEnum.ButtonColor);



            if (!(CSSRuleModel.First().UserId == 0)) 
            {
                IsNew = false;

            } else
            {
                IsNew = true;
            }


        }

        public IActionResult OnPost()
        {
            List<CSSRuleModel> list = new List<CSSRuleModel>();

            int userId;

            if (IsLoggedIn)
            {
                userId = LoggedInUser.UserId;
            }
            else
            {
                userId = 0;
            }

            if (!IsNew)
            {
                list.Add(BackgroundColor);
                list.Add(FooterColor);
                list.Add(TextColor);
                list.Add(ButtonColor);

                CSSManager.UpdateManyCSS(list);

            } else
            {

                BackgroundColor.UserId = userId;
                FooterColor.UserId = userId;
                TextColor.UserId = userId;
                ButtonColor.UserId = userId;

                list.Add(BackgroundColor);
                list.Add(FooterColor);
                list.Add(TextColor);
                list.Add(ButtonColor);

                
                CSSManager.CreateCSS(list);
            }

            //BackgroundColor.CSSVariableId = Domain.Enum.CSSVariableEnum.BackgroundColor;
            //BackgroundColor.UserId = 1;

            //FooterColor.CSSVariableId = Domain.Enum.CSSVariableEnum.FooterColor;
            //FooterColor.UserId = 1;

            //TextColor.CSSVariableId = Domain.Enum.CSSVariableEnum.TextColor;
            //TextColor.UserId = 1;

            //ButtonColor.CSSVariableId = Domain.Enum.CSSVariableEnum.ButtonColor;
            //ButtonColor.UserId = 1;


            return Redirect("/ThemeSettings");

        }

        public IActionResult OnPostResetButton()
        {
            int userId = 0;

            if (IsLoggedIn)
            {
                userId = LoggedInUser.UserId;
            }

            List<CSSRuleModel> list = new List<CSSRuleModel>();

            CSSRuleModel ResetBackgroundColor;
            CSSRuleModel ResetFooterColor;
            CSSRuleModel ResetTextColor;
            CSSRuleModel ResetButtonColor;

            var ResetCSSRuleModel = CSSManager.GetCSS(LoggedInUser.UserId);

            ResetBackgroundColor = ResetCSSRuleModel.First(bgColor => bgColor.CSSVariableId == Domain.Enum.CSSVariableEnum.BackgroundColor);
            ResetBackgroundColor.CSSVariableValue = CSSManager.GetCSS(0).First(bgColor => bgColor.CSSVariableId == Domain.Enum.CSSVariableEnum.BackgroundColor).CSSVariableValue;

            ResetFooterColor = ResetCSSRuleModel.First(footerColor => footerColor.CSSVariableId == Domain.Enum.CSSVariableEnum.FooterColor);
            ResetFooterColor.CSSVariableValue = CSSManager.GetCSS(0).First(footerColor => footerColor.CSSVariableId == Domain.Enum.CSSVariableEnum.FooterColor).CSSVariableValue;

            ResetTextColor = ResetCSSRuleModel.First(textColor => textColor.CSSVariableId == Domain.Enum.CSSVariableEnum.TextColor);
            ResetTextColor.CSSVariableValue = CSSManager.GetCSS(0).First(textColor => textColor.CSSVariableId == Domain.Enum.CSSVariableEnum.TextColor).CSSVariableValue;

            ResetButtonColor = ResetCSSRuleModel.First(buttonColor => buttonColor.CSSVariableId == Domain.Enum.CSSVariableEnum.ButtonColor);
            ResetButtonColor.CSSVariableValue = CSSManager.GetCSS(0).First(buttonColor => buttonColor.CSSVariableId == Domain.Enum.CSSVariableEnum.ButtonColor).CSSVariableValue;


            list.Add(ResetBackgroundColor);
            list.Add(ResetFooterColor);
            list.Add(ResetTextColor);
            list.Add(ResetButtonColor);


            CSSManager.UpdateManyCSS(list);

            return Redirect("/ThemeSettings");
        }
    }
}
