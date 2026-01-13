using Geo_radio.Models;
using Microsoft.AspNetCore.Components;

namespace Geo_radio.Components.PageComponents.EndPopup;

public partial class EndPopup {
    
    [Parameter]
    public int TotalScore {get; set;}

    private String? name;

    public async Task ReturnHome() {
        if (TotalScore > 0) {
            LeaderboardEntry entry = new LeaderboardEntry {Name = name, Score = TotalScore};

            db.Leaderboard.Add(entry);
            await db.SaveChangesAsync();
        }

        gameState.Reset();
        navigation.NavigateTo("/Home");
    }
}