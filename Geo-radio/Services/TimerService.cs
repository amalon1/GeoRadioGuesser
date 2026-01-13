namespace Geo_radio.Services.TimerService;

public class TimerService {

    public event Action? UpdateTime;
    
    private TimeSpan time = TimeSpan.Zero;
    private CancellationTokenSource? cts;
    private Task? task;

    public TimeSpan GetTime() {
        return time;
    }

    public String GetTimeFormatted() {
        return $"{time.Minutes:D2}:{time.Seconds:D2}";
    }

    public void StopTimer() {
        if (cts is null) {
            return;
        }

        cts.Cancel();
        cts.Dispose();
        cts = null;
    }

    public void ResetTimer() {
        StopTimer();

        time = TimeSpan.Zero;
        UpdateTime?.Invoke();
    }

    public void StartTimer() {
        StopTimer();

        cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        task = RunTimerAsync(token);
    }

    private async Task RunTimerAsync(CancellationToken token) {
        try {
            while (time.TotalMinutes < 60) {
                await Task.Delay(1000, token);

                time = time.Add(new TimeSpan(0, 0, 1));

                UpdateTime?.Invoke();
            }
        } catch (TaskCanceledException) {}
    }
}