namespace Geo_radio.Services.UserService;

public class UserService {
    
    private TaskCompletionSource<bool>? tcs;

    public Task<bool> WaitForUserAction() {
        if (tcs is not null) {
            return tcs.Task;
        }

        tcs = new TaskCompletionSource<bool>();
        return tcs.Task;
    }

    public void OnUserActionCompleted() {
        TaskCompletionSource<bool>? localTcs = tcs;
        tcs = null;

        localTcs?.TrySetResult(true);
    }
}