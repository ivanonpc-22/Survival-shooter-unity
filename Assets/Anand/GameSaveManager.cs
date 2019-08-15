using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameSaveManager : MonoBehaviour
{
    public const string SAVE_FILE_NAME = "/gamesave";

    static GameSaveManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        Instance = this;
    }

    private void OnEnable()
    {
        ActionManager.GameStateActions.GameStateChange += OnGameStateChange;
    }

    private void OnDisable()
    {
        ActionManager.GameStateActions.GameStateChange -= OnGameStateChange;
    }
    
    void OnGameStateChange(GAME_STATE state)
    {
        if(state == GAME_STATE.LOAD_GAME)
        {
            LoadGame();

            Invoke("UpdateGameState", 0.1f);
        }
    }

    void UpdateGameState()
    {
        GameStateManager.Instance.UpdateState(GAME_STATE.GAME);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.CurrentGameState == GAME_STATE.GAME)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                QuickSaveGame();
            }
        }
    }


    private void QuickSaveGame()
    {
        GameSaveModel save = SaveGameState();
        string serialized = Utils.SerializeObject(save);
        Utils.WriteTextToFile(SAVE_FILE_NAME, serialized);
    }

    List<EnemySaveModel> SaveEnemyState()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemySaveModel> enemiesSavedState = new List<EnemySaveModel>();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].GetComponent<EnemyHealth>().isDead)
            {
                EnemySaveModel state = new EnemySaveModel();
                state.position = enemies[i].transform.position;
                state.health = enemies[i].GetComponent<EnemyHealth>().CurrentHealth;
                state.timer = enemies[i].GetComponent<EnemyAttack>().timer;
                state.enemy = enemies[i].GetComponent<EnemyAttack>().enemyType;
                enemiesSavedState.Add(state);
            }
        }

        return enemiesSavedState;
    }

    PlayerSaveModel SavePlayerState()
    {
        PlayerSaveModel playerSaveState = new PlayerSaveModel();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerSaveState.position = player.transform.position;
        playerSaveState.score = ScoreManager.score;
        playerSaveState.health = player.GetComponent<PlayerHealth>().CurrentHealth;

        return playerSaveState;
    }

    GameSaveModel SaveGameState()
    {
        GameSaveModel gameSave = new GameSaveModel();
        gameSave.enemiesSave = SaveEnemyState();
        gameSave.playerSave = SavePlayerState();

        return gameSave;
    }

    private void LoadGame()
    {
        string savedJs = Utils.ReadTextFromFile(SAVE_FILE_NAME);

        if (!string.IsNullOrEmpty(savedJs))
        {
            GameSaveModel gameSave = Utils.DeserializeObject<GameSaveModel>(savedJs);

            LoadEnemies(gameSave.enemiesSave);
            LoadPlayer(gameSave.playerSave);
        }
    }

    void LoadEnemies(List<EnemySaveModel> enemiesSaveData)
    {
        for (int i = 0; i < enemiesSaveData.Count; i++)
        {
            if (ActionManager.GameLoadActions.EnemySpawn != null)
                ActionManager.GameLoadActions.EnemySpawn(enemiesSaveData[i]);
        }
    }

    void LoadPlayer(PlayerSaveModel playerSaveData)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSaveData.position;
        player.GetComponent<PlayerHealth>().Load(playerSaveData);
    }


}
