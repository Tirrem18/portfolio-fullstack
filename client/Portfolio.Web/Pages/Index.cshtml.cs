using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Web.Models;
using Portfolio.Web.Pages.Models;
using Supabase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Supabase.Client _supabase;

        public List<DevLog> Logs { get; set; } = new();
        public List<Project> Projects { get; set; } = new();

        public IndexModel(Supabase.Client supabase)
        {
            _supabase = supabase;
        }

        public async Task OnGetAsync()
        {
            // ✅ Fetch latest DevLog
            var response = await _supabase
                .From<DevLog>()
                .Order(x => x.Date, Supabase.Postgrest.Constants.Ordering.Descending)
                .Limit(1)
                .Get();

            Logs = response.Models;
            Console.WriteLine($"[DEVLOG] Fetched latest log for date: {Logs.FirstOrDefault()?.Date}");

       
            Projects = new List<Project>
            {
                new Project
                {
                    Title = "Pet Guardian (IoT)",
                    Description = "AI-Powered Smart Collar with Real-Time Multi-Sensor Fusion for Outdoor Cat Safety",
                    Technologies = new[] { "Python", "Streamlit", "Azure", "MQTT", "WebSocket" },
                    ImagePath = "/images/IoT.jpg",
                    Link = "/projects/PetGuardian"
                },
                new Project
               {
                    Title = "GlucoHub (Mobile)",
                    Description = "Gamified Mobile App for Diabetes Management with Habit Tracking and Glucose Visualization",
                    Technologies = new[] { "Kotlin", "Firebase", "MVVM", "XML", "Android Studio" },
                    ImagePath = "/images/Diabitiesdarker.jpg",
                    Link = "/projects/glucohub"
                },
                new Project
                {
                    Title = "ThAmCo Cloud System (DevOps)",
                    Description = "Azure-Hosted Distributed System with Secure APIs, Fault Tolerance, and CI/CD Pipeline for Online Product Ordering",
                    Technologies = new[] { "ASP.NET Core", "EF", "JWT", "Azure", "GitHub Actions"},
                    ImagePath = "/images/ThAmCo.jpg",
                    Link = "/projects/thamco"
                },
            };
        }
    }
}
