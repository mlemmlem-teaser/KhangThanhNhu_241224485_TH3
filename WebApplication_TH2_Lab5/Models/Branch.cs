using System.ComponentModel.DataAnnotations;

namespace WebApplication_TH2_Lab5.Models
{
    // Ngành học
    public enum Branch
    {
        [Display(Name = "Công nghệ thông tin")] IT = 1,
        [Display(Name = "Kinh tế")] BE = 2,
        [Display(Name = "Công trình")] CE = 3,
        [Display(Name = "Điện - Điện tử")] EE = 4
    }
}
