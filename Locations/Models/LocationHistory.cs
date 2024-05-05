using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

public class LocationHistory{

    [Key]
    public Int32? RecordID {get;set;}
    public Int64 UserID {get;set;}
    public DateTime Timestamp {get;set;}
    public decimal Latitude {get;set;}
    public decimal Longitude {get;set;}

}