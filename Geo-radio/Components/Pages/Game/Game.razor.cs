using Geo_radio.Models;

namespace Geo_radio.Components.Pages.Game;

public partial class Game {

    private List<Station> stations = new List<Station>();

    private Station activeStation = null!;

    private bool hasStarted = false;
    private bool isLoading = false;
    private bool isPopupVisible = false;
    private bool hasEnded = false;
    private String text = "Next Round";

    private String? timeElapsed;
    private int? roundScore;
    private String? userGuess;
    private String? correctGuess;

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender) {
            await StartGame();
        }
    }

    public async Task LoadStation() {
        isLoading = true;

        stations = await radioBrowserService.GetStationsAsync();

        isLoading = false;
    }

    public async Task StartGame() {
        if (hasStarted) {
            return;
        }

        await LoadStation();

        timerService.UpdateTime += HandleChange;

        hasStarted = true;

        for (int round = 0; round < 3; round++) {
            isPopupVisible = false;

            activeStation = stations[round];
            gameState.AdvanceRound();
            StateHasChanged();

            timerService.ResetTimer();
            timerService.StartTimer();

            Task<bool> guessedTask = userService.WaitForUserAction();
            bool guessed = await guessedTask;

            timerService.StopTimer();

            timeElapsed = timerService.GetTimeFormatted();
            userGuess = countryService.GetCountry();

            countryService.SetIso(activeStation.Countrycode);
            correctGuess = countryService.GetCountry();

            roundScore = gameState.CalculateRoundScore(timerService.GetTime(), userGuess == correctGuess);

            if (round == 2) {
                text = "Finish Game";
            }

            isPopupVisible = true;
            StateHasChanged();
            
            Task<bool> confirmedTask = userService.WaitForUserAction();
            bool confirmed = await confirmedTask;            
        }

        timerService.UpdateTime -= HandleChange;

        isPopupVisible = false;
        hasEnded = true;
        StateHasChanged();
    }

    private async void HandleChange() {
        await InvokeAsync(StateHasChanged);
    }
}