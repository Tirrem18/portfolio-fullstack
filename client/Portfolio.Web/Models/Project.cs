namespace Portfolio.Web.Models
{
    // Models/Project.cs
    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Technologies { get; set; }
        public string ImagePath { get; set; }
        public string Link { get; set; }
    }
}