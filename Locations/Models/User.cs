using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

public class User {

    [Key]
    public Int64? UserId {get;set;}
    public Decimal Latitude {get;set;}
    public Decimal Longitude {get;set;}
    public ICollection<LocationHistory>? LocationHistory {get;set;}

}

   