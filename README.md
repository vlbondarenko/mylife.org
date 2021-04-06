# mylife.org

Welcome to the source code of my pet project, which I will someday launch in production 🙂

## Briefly about this portal.

Yes, it will be a portal, something between an online diary and a social network, but with its own principles.

And in the form in which I conceived it, this project is of course interesting to me and I plan to develop it further. And for this reason, I would like to hear criticism from experienced specialists about the approaches and solutions that I use here. I do not expect anyone to spend time and understand this project, but I would like to hear about any obvious shortcomings in the code and architecture of the project.

## What is under the hood?

It is interesting to me not only as a final product, but also as a platform for experiments and improvement of technical skills. 

Here I can use completely different tools, the most modern stack, and a variety of approaches. The plans are huge in this regard. 

### What skills have I already gained working on this project?

When working with the backend:

- **ASP.Net Core 5**

- **EF Core, PostgeSQL**

- **AspNetCore.Identity and JWT-based authentication in particular**

- **Unit and Integration tests, Xunit framework**

- **Clean architecture, CQRS approach, MediatR, a little DDD**

- **and of course REST and HTTP.**

When working with the frontend:

- **Vue3 (Composition API)**

- **JavaScript/Typescript basic knowledge**

- **Vuex/Vue Router**

- **HTML/CSS basics**

This is certainly not all that I was able to learn useful when working on these projects. I also became familiar with the principles of SOLID, became better versed in OOP, in general, in the platform .Net. But I think we'll talk about this at the interview 😉

## Installation instructions

At the moment, the frontend is not adapted to the new version of the backend, I plan to do this in the near future. But you can run each project separately, press the buttons and touch the frontend 🙂, or test the backend with ***Postman***.

### To start the `serverapp`, you need to:

1. Start the project in Visual Studio.

3. Ensure the tool EF was already installed. You can find some help here

       dotnet tool install --global dotnet-ef
   
4. First, you need to restore the packages and tools by running the following commands in the NuGet package manager console:

       dotnet restore
       dotnet tool restore
       
5. Next, you need to connect the database to the project. If you use `PostgreSQL`, then you don't need to change anything in the code, go straight to the next step.

  ***If you want to use SQL Server or InMemory database:***

- change the `AddPersistance` method in the `Persistence.DependencyInjection` class according to the code comments:
   
    ```csharp
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("ApplicationDefaultDatabase")));         // <- uncomment this line for SQL Server
            //options.UseInMemoryDatabase("ApplicationDatabase");                                             // <- or this fof InMemory database
            options.UseNpgsql(configuration.GetConnectionString("ApplicationDatabase")));                     // <- comment this line
            
            // ... other code
    }
    ```

- change the `AddIdentity` method in the `Infrastructure.Identity.DependencyInjection` class according to the code comments:

    ```csharp
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("IdentityDefaultDatabase")));            // <- uncomment this line for SQL Server
            //options.UseInMemoryDatabase("IdentityDatabase");                                                // <- or this fof InMemory database
            options.UseNpgsql(configuration.GetConnectionString("IdentityDatabase")));                        // <- comment this line
            
            // ... other code
     }
     ```
- if you want to use the database in memory, **then you don't need to perform any more actions, the project is ready to run.** In the case of real databases - go to the next point

- if necessary, change the connection strings. The connection strings for SQL Server are already set by default, but you can change them in the `appsettings.Infrastructure.json` (string `ConnectionStrings.IdentityDefaultDatabase`) and `appsettings.Persistence.json` (string `ConnectionStrings.ApplicationDefaultDatabase`) files if necessary in the root folder of the WebAPI project.

6. Open a command prompt in the `WebApi` folder and execute the following commands:
      
       dotnet ef migrations add Init -c identitydbcontext -p ../Infrastructure.Identity/Infrastructure.Identity.csproj -s WebApi.csproj -o Data/Migrations
       dotnet ef database update -c identitydbcontext -p ../Infrastructure.Identity/Infrastructure.Identity.csproj -s WebApi.csproj

       dotnet ef migrations add Init -c applicationdbcontext -p ../Persistence/Persistence.csproj -s WebApi.csproj
       dotnet ef database update -c applicationdbcontext -p ../Persistence/Persistence.csproj -s WebApi.csproj
       
7. That's all, the server project is ready to launch!

### To start the `clientapp`, you need to:

1. Open a command prompt in the `clientapp` folder and execute the following commands:

       npm install
       npm run serve

2. Go to the address in the browser http://localhost:8080/

***Here is what the client application looks like (sorry for the quality and.. for the technical text 🙂)***

<p align="center">
  <img width="720" height="408" src="https://user-images.githubusercontent.com/35936321/113702509-43d8f780-96e2-11eb-8076-047f69933634.gif">
</p>

Well, that's probably all.. Thanks for your attention! 😉
