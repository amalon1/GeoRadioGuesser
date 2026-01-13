using Microsoft.JSInterop;

namespace Geo_radio.Components.PageComponents.Map;

public partial class Map {

    public String Selected {get; private set;} = "notSelected";

    private String? SelectedIso;

    private DotNetObjectReference<Map>? DNOReference;
    
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender) {
            DNOReference = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("leafletInterop.InitMap", DNOReference);
        }
    }

    public void ProcessGuess() {
        if (SelectedIso is not null) {
            Selected = "notSelected";
            countryService.SetIso(SelectedIso);
            userService.OnUserActionCompleted();
        }
    }

    [JSInvokable]
    public void OnCountryClicked(String iso) {
        SelectedIso = iso;
        Selected = "selected";
        StateHasChanged();
    }
}