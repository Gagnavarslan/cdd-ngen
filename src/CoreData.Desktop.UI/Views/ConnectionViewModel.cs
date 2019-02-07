using CoreData.Desktop.Server.Settings;
using System.ComponentModel.DataAnnotations;

namespace CoreData.Desktop.UI.Views
{
    public class ConnectionViewModel
    {
        public ConnectionViewModel() { }
        public ConnectionViewModel(Server.Settings.Connection connection)
        {
            Server = connection.Server.AbsoluteUri;
            if (connection is BasicConnection basicConnection)
            {
                UseSso = false;
                User = basicConnection.User;
                Password = basicConnection.Password;
            }
            else
            {
                UseSso = true;
            }
            RememberIt = connection.RememberIt;
        }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = nameof(Server))]
        public string Server { get; set; }

        [Display(Name = "Use SSO")]
        public bool UseSso { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember connection?")]
        public bool RememberIt { get; set; }
    }
}
