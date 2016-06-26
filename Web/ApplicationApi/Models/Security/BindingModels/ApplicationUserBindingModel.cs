using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInfo.Web.ApplicationApi.Models.Security.BindingModels
{
    public class ApplicationUserBindingModel
    {
        public ApplicationUserBindingModel()
        {

        }

        public ApplicationUserBindingModel(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}