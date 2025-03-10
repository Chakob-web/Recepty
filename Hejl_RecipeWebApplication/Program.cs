using Microsoft.EntityFrameworkCore;
using Hejl_RecipeWebApplication.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Hejl_RecipeWebApplication.Entities;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7195;
});

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!dbContext.Recipes.Any())
    {
        dbContext.Recipes.Add(new Recipe
        {
            Title = "Sv��kov� na smetan�",
            Description = "Ned�ln� ob�d jedna b�se� � hezky po �esku! Sv��kov� na smetan� je prost� klasikou �esk� n�rodn� kuchyn�. Jej� p��prava m� sv� pevn� pravidla a vy�aduje trochu �asu. V�sledek ale stoj� za to.",
            Ingredients = "mrkev 6 ks, celer 1 ks, petr�el 6 ks nebo pastin�k, cibule 2 ks na kosti�ky, m�slo 200 g rozpu�t�n�, loupan� plec 1,5 kg hov�z�, slanina 200 g na kl�nky, bobkov� list 5 ks, nov� ko�en� 8 kuli�ek, pep� 8 kuli�ek, s�l 2 PL",
            Instructions = "Nakr�jej ovoce, sm�chej a p�idej cukr.",
            ImageUrl = "https://www.toprecepty.cz/fotky/recepty/0344/89bd3ae43b674aabb96508ba9245a62d-facebook.jpg",
            CreatedAt = DateTime.UtcNow
        });

        dbContext.SaveChanges();
    }
}

app.Run();
