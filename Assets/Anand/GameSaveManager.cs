using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameSaveManager : MonoBehaviour
{
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
            QuickLoadGame();

            Invoke("LoadGame",0.1f);
        }
    }

    void LoadGame()
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
            else if (Input.GetKeyDown(KeyCode.L))
            {
                QuickLoadGame();
            }
        }
    }

    private void QuickLoadGame()
    {
        string filePath = Application.persistentDataPath + STATIC_DATA_FILE_NAME;

        string savedData = null;
        if (File.Exists(filePath))
        {
            savedData = File.ReadAllText(filePath);

            GameSaveState gameSave = JsonConvert.DeserializeObject<GameSaveState>(savedData);

            for (int i = 0; i < gameSave.enemiesSave.Count; i++)
            {
                if(ActionManager.GameLoadActions.EnemySpawn != null)
                    ActionManager.GameLoadActions.EnemySpawn(gameSave.enemiesSave[i]);
            }

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = gameSave.playerSave.position;
            player.GetComponent<PlayerHealth>().Load(gameSave.playerSave);
        }
    }

    public const string STATIC_DATA_FILE_NAME = "/chfsk";

    private void QuickSaveGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemySaveState> enemiesSavedState = new List<EnemySaveState>();
        PlayerSaveState playerSaveState = new PlayerSaveState();


        for (int i = 0; i < enemies.Length; i++)
        {
            if(!enemies[i].GetComponent<EnemyHealth>().isDead)
            {
                EnemySaveState state = new EnemySaveState();
                state.position = enemies[i].transform.position;
                state.health = enemies[i].GetComponent<EnemyHealth>().CurrentHealth;
                state.timer = enemies[i].GetComponent<EnemyAttack>().timer;
                state.enemy = enemies[i].GetComponent<EnemyAttack>().enemyType;
                enemiesSavedState.Add(state);
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerSaveState.position = player.transform.position;
        playerSaveState.score = ScoreManager.score;
        playerSaveState.health = player.GetComponent<PlayerHealth>().CurrentHealth;

        GameSaveState gameSave = new GameSaveState();
        gameSave.enemiesSave = enemiesSavedState;
        gameSave.playerSave = playerSaveState;

        string str = JsonConvert.SerializeObject(gameSave);

        string filePath = Application.persistentDataPath + STATIC_DATA_FILE_NAME;
        System.IO.File.WriteAllText(filePath, str);

        Debug.LogError("saved string " + str);

    }
}

public struct PlayerSaveState
{
    public Vector3 position;
    public int health;
    public int score;
}

public struct EnemySaveState
{
    public string enemy;
    public Vector3 position;
    public int health;
    public float timer;
}

public struct GameSaveState
{
    public PlayerSaveState playerSave;
    public List<EnemySaveState> enemiesSave;
}
