using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System.Reflection.Metadata.Ecma335;
using System.Collections.ObjectModel;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbContext = builder.Services.AddDbContext<LocationDBContext>(
        options => options.UseInMemoryDatabase("locationHistory")
        );


var app = builder.Build();

GenerateTestData();

app.MapGet("/", () => "Hello World!");
app.MapGet("/users", async ([FromServices] LocationDBContext context, HttpContext httpContext) =>await context.Users.ToListAsync())
    .WithName("GetUsers");

app.MapPost("/user", async ([FromBody] User user, [FromServices] LocationDBContext db, HttpContext httpContext) =>
{
    // post is for adding a user, but for safety reasons will add an update if the user id is not null
 
  Geolocator geo = new Geolocator();
  geo.AddGeoLocationToUser(httpContext);

    // post is for adding a user, but for safety reasons will add an update if the user id is not null
    if (user.UserID !=0)
    {
        var userToUpdate = db.Users.Where(p => p.UserID == user.UserID).FirstOrDefault();
        if (userToUpdate!=null)
        {
            userToUpdate.Latitude = user.Latitude;
            userToUpdate.Longitude = user.Longitude;
        }
        else
        {
            user.UserID=null;
               db.Users.Add(user);
        }
    }
    else
    {
        db.Users.Add(user);
    }
   
    await db.SaveChangesAsync();
    // add location history for this user
    LocationHistory loc = new LocationHistory(){UserID = user.UserID, Latitude = user.Latitude, Longitude = user.Longitude, timestamp = DateTime.UtcNow};
    db.LocationHistory.Add(loc);
    await db.SaveChangesAsync();



    return Results.Created($"/users/{user.UserId}", user);
}).WithName("createUser");


app.MapPatch("/user", async (User user, LocationDBContext db, HttpContext httpContext) =>
{
    if ((user.Latitude  == null) || (user.latitude = ""))
    {
        Geolocator geo  = new Geolocator();
        geo.AddGeoLocationToUser(httpContext, user);
    }
    LocationHistory h = new LocationHistory();
    h.UserID = user.UserId??0;
    h.Timestamp = DateTime.UtcNow;
    h.Latitude = user.Latitude;
    h.Longitude = user.Longitude ;

    db.LocationHistory.Add(h);
   
    await db.SaveChangesAsync();

    return Results.Created($"/users/{user.UserId}", user); // todo replace this with something more appropriate to a patch.
}).WithName("updateUser");


// complete location history for a specific user
app.MapGet("/locationHistory/{userID}", async (Int64 userID, [FromServices] LocationDBContext context) => {
    await context.LocationHistory.Where(p =>p.UserID == userID).OrderBy(p => p.Timestamp).ToListAsync();
    }).WithName("getLocationHistory");

// complete most recent locationa history for all users
app.MapGet("/locationHistory", async ([FromServices] LocationDBContext context) => {
    await context.LocationHistory.GroupBy(p =>p.UserID == userID)
        .OrderByDescending(p =>p.Timestamp)
        .Select(p => p.FirstOrDefault()).ToListAsync();

    }).WithName("getRecentLocations");


app.MapGet("/user/{userID}"), async (Int64 userID, [FromServices] LocationDBContext dbContext) => {
    var user = dbContext.Users.Where(p =>p.UserID == userID).FirstOrDefault();
    if (user != null)
    {
        return user; //contains latest latitude and longitude. Or we could use the max location history but the way I have written it this is unnecessay overhead.
    }
    else
    {
        return Results.NotFound;
    }

    }).WithName("getUserById");



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();



void GenerateTestData()
{
    using var scope = app.Services.CreateScope();
    LocationDBContext context = scope.ServiceProvider.GetRequiredService<LocationDBContext>();
    var user=  new User() {UserId=1, Latitude= 20, Longitude= 125};
    context.Users.Add(user);

    var loc=new LocationHistory(){UserID=1, Latitude=75, Longitude= 75};
    user.LocationHistory = new Collection<LocationHistory>();
    user.LocationHistory.Append(loc);
    user.LocationHistory.Append(new LocationHistory(){UserID=1, Latitude=-4, Longitude= 95});
  
    context.LocationHistory.Add(new LocationHistory(){UserID=1, Latitude=70, Longitude= 85});
    
    var user2 = new User() {UserId=2, Latitude= 20, Longitude= 125};
    context.Users.Add(user);

    var loc2=new LocationHistory(){UserID=1, Latitude=-43, Longitude= 75};
    user.LocationHistory = new Collection<LocationHistory>();
    user.LocationHistory.Append(loc2);
    user.LocationHistory.Append(new LocationHistory(){UserID=1, Latitude=95, Longitude= 98});
  
    
    
    context.SaveChanges();

}

