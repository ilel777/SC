using UnityEngine;

public class Level1 : LevelManager
{
    new void Start()
    {
        base.Start();
        Instantiate(Resources.Load<GameObject>("Prefabs/" + ConfigurationUtils.LevelConfig.name + "/Background"));
    }

    public override bool GameOver()
    {
        return LevelStatistics.PlayerLives < 2;
    }

    public override bool MissionComplete()
    {
        return LevelStatistics.AsteroidsDestroyed > 3;
    }

}
