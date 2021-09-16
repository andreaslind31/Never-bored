using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Never_bored.API;
using Never_bored.Models;
using Newtonsoft.Json;

namespace Never_bored.Pages.Bored
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ActivityModel Activity { get; set; }
        [BindProperty]
        public bool isFavorite { get; set; }
        public List<ActivityModel> CookieActivities { get; set; } = new List<ActivityModel>();
        public async Task OnGet()
        {
            Activity = await DataProcessor.LoadData();

            string stringActivities = HttpContext.Session.GetString("Activities");
            if (!String.IsNullOrEmpty(stringActivities))
            {
                CookieActivities = JsonConvert.DeserializeObject<List<ActivityModel>>(stringActivities);
            }
        }
        public IActionResult OnPost()
        {

            List<ActivityModel> activities = new List<ActivityModel>();

            string stringActivities = HttpContext.Session.GetString("Activities");

            if (!String.IsNullOrEmpty(stringActivities))
            {
                activities = JsonConvert.DeserializeObject<List<ActivityModel>>(stringActivities);
            }

            activities.Add(Activity);

            Data.ActivityData.ListOfActivities.Add(Activity);

            stringActivities = JsonConvert.SerializeObject(activities);

            HttpContext.Session.SetString("Activities", stringActivities);

            return RedirectToPage("/Bored/Index");

        }
    }
}
