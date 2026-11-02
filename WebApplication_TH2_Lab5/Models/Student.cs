using System.ComponentModel.DataAnnotations;

namespace WebApplication_TH2_Lab5.Models
{
    public class Student
    {
        public int Id { get; set; } // Mã sinh viên

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string? Name { get; set; } // Họ tên

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bắt buộc phải nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi @gmail.com")]
        public string? Email { get; set; } // Email

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 100 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, chữ số và ký tự đặc biệt")]
        [DataType(DataType.Password)]
        public string? Password { get; set; } // Mật khẩu

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Vui lòng chọn ngành học")]
        public Branch? Branch { get; set; } // Ngành học

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public Gender? Gender { get; set; } // Giới tính

        [Display(Name = "Hệ chính quy")]
        public bool IsRegular { get; set; } // Hệ: true - chính quy, false - phi chính quy

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; } // Địa chỉ

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1963-01-01", "2005-12-31",
            ErrorMessage = "Ngày sinh phải từ 01/01/1963 đến 31/12/2005",
            ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
        public DateTime? DateOfBirth { get; set; } // Ngày sinh

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải là số thực từ 0.0 đến 10.0")]
        public double? Score { get; set; } // Điểm
    }
}
