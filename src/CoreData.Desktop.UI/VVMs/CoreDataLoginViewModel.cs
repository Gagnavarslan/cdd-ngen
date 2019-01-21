using System.ComponentModel.DataAnnotations;

namespace CoreData.Desktop.UI.VVMs
{
    public class CoreDataLoginViewModel
    {
        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Host")]
        public string Host { get; set; }

        [Display(Name = "Use SSO")]
        public bool UseSso { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
