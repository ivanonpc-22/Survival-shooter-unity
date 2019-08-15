using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        ActionManager.GameStateActions.GameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        ActionManager.GameStateActions.GameStateChange -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GAME_STATE newState)
    {
        if (newState == GAME_STATE.GAME)
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        else if (newState == GAME_STATE.GAME_OVER)
            CancelInvoke("Spawn");
    }


    void Spawn ()
    {
        if(playerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
