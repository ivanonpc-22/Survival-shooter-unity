
public struct PlayerStatModel
{
    public int kills;
    public int hiScore;
    public int deaths;
    public long bullets;

    public void IncrementKills()
    {
        ++kills;
    }

    public void UpdateHiScore(int score)
    {
        hiScore = score;
    }

    public void IncrementDeaths()
    {
        ++deaths;
    }

    public void IncrementBullets()
    {
        ++bullets;
    }
}
