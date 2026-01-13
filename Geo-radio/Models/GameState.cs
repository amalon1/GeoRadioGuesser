namespace Geo_radio.Models;

public class GameState {

    public int Score {get; private set;} = 0;
    public int Round {get; private set;} = 0;

    public void AdvanceRound() {
        Round++;
    }

    public int CalculateRoundScore(TimeSpan time, bool correct) {
        if (!correct) {
            return 0;
        }

        int roundScore = (int)(100 + (1 - (time.TotalSeconds / 1800)) * 900);

        Score += roundScore;

        return roundScore;
    }

    public void Reset() {
        Score = 0;
        Round = 0;
    }
}