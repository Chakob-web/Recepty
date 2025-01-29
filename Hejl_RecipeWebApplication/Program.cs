using Microsoft.EntityFrameworkCore;
using Hejl_RecipeWebApplication.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Hejl_RecipeWebApplication.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!dbContext.Recipes.Any())
    {
        dbContext.Recipes.Add(new Recipe
        {
            Title = "Svíèková na smetanì",
            Description = "Nedìlní obìd jedna báseò – hezky po èesku! Svíèková na smetanì je prostì klasikou èeské národní kuchynì. Její pøíprava má své pevná pravidla a vyžaduje trochu èasu. Výsledek ale stojí za to. ",
            Ingredients = "mrkev 6 ks, celer 1 ks, petržel 6 ks nebo pastinák, cibule 2 ks na kostièky, máslo 200 g rozpuštìné, loupaná plec 1,5 kg hovìzí, slanina 200 g na klínky, bobkový list 5 ks, nové koøení 8 kulièek, pepø 8 kulièek, sùl 2 PL",
            Instructions = "Nakrájej ovoce, smíchej a pøidej cukr.",
            ImageUrl = "https://www.toprecepty.cz/fotky/recepty/0344/89bd3ae43b674aabb96508ba9245a62d-facebook.jpg",
            CreatedAt = DateTime.UtcNow
        });

        dbContext.SaveChanges();
    }
}

app.Run();
