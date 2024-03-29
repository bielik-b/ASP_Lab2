using asplab2;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("config.json")
    .AddXmlFile("config.xml")
    .AddIniFile("config.ini")
    .AddJsonFile("personalinfo.json");
var app = builder.Build();

app.Map("/", async (HttpContext context, IConfiguration appConfig) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    Company microsoft = new Company(appConfig["Microsoft:name"], Convert.ToInt32(appConfig["Microsoft:employees"]));
    Company apple = new Company();
    app.Configuration.Bind(apple);
    Company google = new Company(appConfig["Google:name"], Convert.ToInt32(appConfig["Google:employees"]));
    if (microsoft.Employees > apple.Employees && microsoft.Employees > google.Employees)
    {
        await context.Response.WriteAsync($"<div style=\"color:green\"><b>{microsoft.Name}</b> has the most employees: " +
            $"<b>{microsoft.Employees}</b></div>");
    }
    else if (apple.Employees > microsoft.Employees && apple.Employees > google.Employees)
    {
        await context.Response.WriteAsync($"<div style=\"color:green\"><b>{apple.Name}</b> has the most employees: " +
            $"<b>{apple.Employees}</b></div>");
    }
    else
    {
        await context.Response.WriteAsync($"<div style=\"color:green\"><b>{google.Name}</b> has the most employees: " +
            $"<b>{google.Employees}</b></div>");
    }

});
app.Map("/personal-info", async (HttpContext context, IConfiguration appConfig) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync($"<h style=\"color:black; font-size:20px\"><b>Information about me</b></h>" +
        $"<div>Name: <b>{appConfig["Person:name"]}</b></div>" +
        $"<div>Age: <b>{appConfig["Person:age"]}</b></div>" +
        $"<div>University: <b>{appConfig["Person:university"]}</b></div>");
});

app.Run();
