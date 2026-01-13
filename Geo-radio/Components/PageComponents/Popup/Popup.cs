using Microsoft.AspNetCore.Components;

namespace Geo_radio.Components.PageComponents.Popup;

public partial class Popup {
    
    [Parameter]
    public bool Visible {get; set;}

    [Parameter]
    public String? ButtonText {get; set;}

    [Parameter]
    public String? GuessedCountry {get; set;}

    [Parameter]
    public String? CorrectCountry {get; set;}

    [Parameter]
    public String? StationName {get; set;}

    [Parameter]
    public String? TimeElapsed {get; set;}

    [Parameter]
    public int? RoundScore {get; set;}

    private void Close() {
        Visible = false;
        userService.OnUserActionCompleted();
    }
}