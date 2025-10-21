using Etic.Business.ComponentHandler;
using Etic.Business.ControllerHandler;
using Etic.Business.Helpers;
using Etic.Business.Services;
using Etic.Data.Abstract;
using Etic.Data.Concrete;

namespace Etic.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Yeni bir Interface yazarsanýz onu buraya eklemeniz lazým yoksa çalýþmaz.
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //AddScoped nedir ? bir nesneyi request geldiði anda oluþturur response verildiði anda onu öldürür
            //6. adým DI
            builder.Services.AddScoped<IUserDal, UserDal>();
            builder.Services.AddScoped<ICategoryDal, CategoryDal>(); //
            builder.Services.AddScoped<ISliderDal, SliderDal>(); //BUUUUU


            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();//
            builder.Services.AddScoped<ISliderService, SliderService>();//BUUUUU

            builder.Services.AddScoped<ICookieHelper, CookieHelper>();

            builder.Services.AddScoped<IProductDal, ProductDal>();
            builder.Services.AddScoped<IProductImageDal, ProductImageDal>();
            builder.Services.AddScoped<IProductCategoryDal, ProductCategoryDal>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductImageService, ProductImageService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IBasketProductService, BasketProductService>();
            builder.Services.AddScoped<IBasketDal, BasketDal>();
            builder.Services.AddScoped<IBasketProductDal, BasketProductDal>();



            builder.Services.AddScoped<IAuthenticationControllerHandler, AuthenticationControllerHandler>();
            builder.Services.AddScoped<IHeaderComponentHandler, HeaderComponentHandler>();
            builder.Services.AddScoped<ILeftMenuComponentHandler, LeftMenuComponentHandler>();
            builder.Services.AddScoped<IElectroProductWrapperComponentHandler, ElectroProductWrapperComponentHandler>();

            builder.Services.AddScoped<IProductControllerHandler, ProductControllerHandler>();
            builder.Services.AddScoped<IApiControllerHandler, ApiControllerhandler>();

            builder.Services.AddScoped<IVwCategoryProductDal, VwCategoryProductDal>();
            builder.Services.AddScoped<IVwCategoryProductService, VwCategoryProductService>();

            builder.Services.AddScoped<IProductListControllerHandler, ProductListControllerHandler>();

            builder.Services.AddScoped<IVwBasketProductListDal, VwBasketProductListDal>();

            builder.Services.AddScoped<ICheckOutControllerHandler, CheckOutControllerHandler>();


            builder.Services.AddScoped<IMenuSliderWrapperComponentHandler, MenuSliderWrapperComponentHandler>();//BUUUUU
                                                                                                                //todo:rasim:DI


            builder.Services.AddScoped<ISettingDal, SettingDal>();
            builder.Services.AddScoped<ISettingService, SettingService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            // login url þöyle olacak
            // /login
            // 7login þeklinde gelen tüm istekleriAuthencation controller içerisindeki index action üzerine yönlendirir.
            app.MapControllerRoute(
                name: "Register",
                pattern: "/login/",
                defaults: new { controller = "Authencation", action = "Index" });

            app.MapControllerRoute(
              name: "CheckOut",
              pattern: "/checkout/",
              defaults: new { controller = "Checkout", action = "Index" });

            app.MapControllerRoute(
                name: "ProductDetail",
                pattern: "/urun/{name}",
                defaults: new { controller = "Product", action = "Index" });

            app.MapControllerRoute(
                name: "ProductList",
                pattern: "/kategori/{name}/{page?}",
                defaults: new { controller = "ProductList", action = "Index" });

            app.MapControllerRoute(
              name: "ProductList",
              pattern: "/arama/{page?}",
              defaults: new { controller = "ProductList", action = "Search" });

            app.MapControllerRoute(
                name: "Exit",
                pattern: "/exit/",
                defaults: new { controller = "Authencation", action = "Exit" });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}