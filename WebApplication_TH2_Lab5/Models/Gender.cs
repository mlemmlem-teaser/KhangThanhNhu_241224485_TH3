using System.ComponentModel.DataAnnotations;

namespace WebApplication_TH2_Lab5.Models
{
    public enum Gender
    {
        [Display(Name = "Nam")] Male,
        [Display(Name = "Nữ")] Female
    }
}
