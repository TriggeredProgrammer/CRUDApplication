using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CRUDApplication.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    [Required]
    [MaxLength(10, ErrorMessage = "DomainName must be 10 characters long.")]
    [RegularExpression(@"^[a-zA-Z0-9_]{1,10}$", ErrorMessage = "DomainName must be alphanumeric and can only contain underscores.")]
    public string DomainName { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

}
