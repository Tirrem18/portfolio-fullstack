using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Web.Models;
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

       
            
        }
    }
}
