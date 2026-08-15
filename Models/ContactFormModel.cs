using System.ComponentModel.DataAnnotations;

namespace SRKFruitsWeb.Models
{
    // Bound model for the Contact page form.
    // Kept deliberately small — only what SRK Fruits actually needs
    // to follow up on a wholesale/retail enquiry.
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Please tell us your name.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.'-]*$", ErrorMessage = "Name can only contain letters, spaces, and . ' -")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a contact email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please provide a phone number.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit mobile number (digits only).")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select what you're enquiring about.")]
        [Display(Name = "Enquiry Type")]
        public string EnquiryType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please add a short message.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message should be between 10 and 1000 characters.")]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}