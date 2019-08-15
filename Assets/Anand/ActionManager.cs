using System;

public class ActionManager
{
    public class GameStateActions
    {
        public static Action<GAME_STATE> GameStateChange; 
    }

    public class GameLoadActions
    {
        public static Action<EnemySaveModel> EnemySpawn;
    }

    public class GameplayActions
    {
        public static Action EnemyKilled;
        public static Action ScoreUpdate;
        public static Action PlayerDeath;
        public static Action BulletsFired;
    }
}
