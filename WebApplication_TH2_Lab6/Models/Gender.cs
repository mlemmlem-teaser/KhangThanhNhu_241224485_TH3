using System.ComponentModel.DataAnnotations;

namespace WebApplication_TH2_Lab6.Models
{
    public enum Gender
    {
        [Display(Name = "Nam")] Male,
        [Display(Name = "Nữ")] Female
    }
}
