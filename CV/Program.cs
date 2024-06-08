using CV.Components;
using CV.Interfaces;
using CV.Services;
using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ToastService>();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddDbContext<CVContext>(options =>
    options.UseSqlServer(
        "Server=sql_server;Database=CVDb5;User Id=sa;Password=Samba12345!;TrustServerCertificate=True;Connection Timeout=30;MultipleActiveResultSets=true"
            ?? "Data Source=../DataAccess/CV.db",
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
        }
    )
);

builder
    .Services.AddDefaultIdentity<CVUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CVContext>();

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();

builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

builder.Services.AddHttpClient<IResumeClient, ResumeClient>();
builder.Services.AddHttpClient<IItemClient, ItemClient>();
builder.Services.AddHttpClient<ISkillClient, SkillClient>();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddTransient<IResumeClient, ResumeClient>();

builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CVContext>();
    db.Database.Migrate();
}

app.MapIdentityApi<CVUser>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
