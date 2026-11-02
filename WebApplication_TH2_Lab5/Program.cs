using Microsoft.EntityFrameworkCore;
using WebApplication_TH2_Lab5.Data;

namespace WebApplication_TH2_Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký SchoolContext là một DbContext của ứng dụng
            // (chuỗi kết nối "SchoolContext" nằm trong User Secrets / appsettings.json)
            builder.Services.AddDbContext<SchoolContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                // Thông báo lỗi bind dữ liệu bằng tiếng Việt
                var p = options.ModelBindingMessageProvider;
                p.SetValueMustNotBeNullAccessor(_ => "Vui lòng nhập giá trị");
                p.SetMissingBindRequiredValueAccessor(n => $"Thiếu giá trị cho {n}");
                p.SetAttemptedValueIsInvalidAccessor((v, n) => $"Giá trị '{v}' không hợp lệ cho {n}");
                p.SetValueIsInvalidAccessor(v => $"Giá trị '{v}' không hợp lệ");
                p.SetValueMustBeANumberAccessor(n => $"{n} phải là số");
                p.SetUnknownValueIsInvalidAccessor(n => $"Giá trị không hợp lệ cho {n}");
                p.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Giá trị không hợp lệ");
                p.SetNonPropertyValueMustBeANumberAccessor(() => "Giá trị phải là số");
            });

            var app = builder.Build();

            // Tạo CSDL (nếu chưa có) và dữ liệu mẫu
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DbInitializer.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Dùng culture en-US để dấu thập phân là "." (điểm 8.5) và ngày yyyy-MM-dd parse đúng trên mọi máy
            var culture = new System.Globalization.CultureInfo("en-US");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
