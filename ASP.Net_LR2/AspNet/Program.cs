using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
//1
var builder = WebApplication.CreateBuilder();
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("config.json")
    .AddXmlFile("config.xml")
    .AddIniFile("config.ini")
    .AddJsonFile("myname.json");
var app = builder.Build();

app.Services.GetService<IConfiguration>();


var result = app.Map("/", async (HttpContext context) =>
{
    var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
    var companies = new List<Company>
    {
        new Company { Name = configuration["comp1"], Employee = configuration["Employee1"] },
        new Company { Name = configuration["comp2"], Employee = configuration["Employee2"] },
        new Company { Name = configuration["comp3"], Employee = configuration["Employee3"] },
    };
    var companyInfo = string.Join("\n", companies.Select(c => $"{c.Name} - {c.Employee}"));
    var maxEmployees = companies.Max(c => c.Employee);
    var companyWithMostEmployees = companies.FirstOrDefault(c => c.Employee == maxEmployees);
    var tom = new Person();
    configuration.Bind(tom);


    await context.Response.WriteAsync(companyInfo);
    await context.Response.WriteAsync($"\nThe largest number of employees has: {companyWithMostEmployees.Name}\n");

    await context.Response.WriteAsync(
        $"\nName: {tom.Name}\n" +
        $"Surname: {tom.Surname}\n" +
        $"Age: {tom.Age}\n" +
        $"Birthday: {tom.Birthday}\n" +
        $"Residence: {tom.Residence}\n"
        );


});

app.Run();


public class Company
{
    public string Name { get; set; }
    public string Employee { get; set; }
}

public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }

    public string Birthday { get; set; }
    public string Residence { get; set;}
}
