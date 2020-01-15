using UnityEngine;

public class Level1 : LevelManager
{

    public override bool GameOver()
    {
        return LevelStatistics.PlayerLives < 2;
    }

    public override bool MissionComplete()
    {
        return LevelStatistics.AsteroidsDestroyed > 3;
    }

}
