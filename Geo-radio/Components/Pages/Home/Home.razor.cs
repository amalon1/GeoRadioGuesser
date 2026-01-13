using Geo_radio.Models;

namespace Geo_radio.Components.Pages.Home;

public partial class Home {

    private int counter = 1;
    private List<LeaderboardEntry>? scores;

    protected override async Task OnInitializedAsync() {
        scores = await ls.GetTop10();
    }

    public void PlayGame() {
        navigation.NavigateTo("/game");
    }
}