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

}
