var builder = WebApplication.CreateBuilder(args);

// Razor Pages is enough here – the brief is a static/informational site
// with a single interactive piece (the Contact form), so we don't need
// MVC controllers or an API layer.
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
