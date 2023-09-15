using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(Result);
app.Run();
async Task Result(HttpContext context)
{
    //1
    Company company = new Company();
    company.Name = "Google";
    company.Address = "Kiew,85\n";
    company.Phone = "+0680921172\n";
    //2
    Random random = new Random();
    int randomNumber = random.Next(0, 101);
    //Вывод
    await context.Response.WriteAsync($"Compamy:\n {company.Name} \n {company.Address} {company.Phone}"); 
    await context.Response.WriteAsync($"\nRandom number: {randomNumber}");
}
public class Company
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

}
