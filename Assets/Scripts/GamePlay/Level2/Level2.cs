public class Level2 : LevelManager
{

    public override bool GameOver()
    {
        return LevelStatistics.PlayerLives < 2;
    }

    public override bool MissionComplete()
    {
        return LevelStatistics.EnemyShipsDestroyed > 2;
    }

}
