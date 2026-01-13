using System.ComponentModel.DataAnnotations;

namespace Geo_radio.Models;

public class LeaderboardEntry {
    
    [Key]
    public String? Name {get; set;}

    public int Score {get; set;}
}