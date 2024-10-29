using ArabityAuth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<IdentityOptions>(o => {
    o.Password.RequiredUniqueChars = 0;
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthorization(options =>
    options.AddPolicy("AdminRole", op => op.RequireClaim("Admin", "Admin"))
    );

builder.Services.AddAuthorization(options =>
    options.AddPolicy("CustomerRole", op => op.RequireClaim("Customer", "Customer"))
    );

builder.Services.AddAuthorization(options =>
    options.AddPolicy("StoreRole", op => op.RequireClaim("Store", "Store"))
    );

builder.Services.AddAuthorization(options =>
    options.AddPolicy("WorkshopRole", op => op.RequireClaim("Workshop", "Workshop"))
    );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<IEmailSender, ArabityAuth.Code.EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
