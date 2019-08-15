using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatTracker : MonoBehaviour
{
    public static PlayerStatTracker Instance;
    public PlayerStatModel currentStats { get; private set; }

    public const string STAT_FILE = "/gamestats";

    private void Awake()
    {
        Instance = this;

        LoadStats();
    }

    private void OnEnable()
    {
        ActionManager.GameplayActions.BulletsFired += OnBulletFired;
        ActionManager.GameplayActions.EnemyKilled += OnEnemyKilled;
        ActionManager.GameplayActions.PlayerDeath += OnPlayerDeath;
        ActionManager.GameplayActions.ScoreUpdate += OnScoreUpdate;
    }

    private void OnDisable()
    {
        ActionManager.GameplayActions.BulletsFired -= OnBulletFired;
        ActionManager.GameplayActions.EnemyKilled -= OnEnemyKilled;
        ActionManager.GameplayActions.PlayerDeath -= OnPlayerDeath;
        ActionManager.GameplayActions.ScoreUpdate -= OnScoreUpdate;
    }

    private void OnScoreUpdate()
    {
        if (ScoreManager.score > currentStats.hiScore)
            currentStats.UpdateHiScore(ScoreManager.score);
    }

    private void OnPlayerDeath()
    {
        currentStats.IncrementDeaths();
    }

    private void OnEnemyKilled()
    {
        currentStats.IncrementKills();
    }

    private void OnBulletFired()
    {
        currentStats.IncrementBullets();
    }

    public void LoadStats()
    {
        string savedJSON = Utils.ReadTextFromFile(STAT_FILE);

        if (!string.IsNullOrEmpty(savedJSON))
        {
            PlayerStatModel stat = Utils.DeserializeObject<PlayerStatModel>(savedJSON);
            currentStats = stat;
        }
        else
            currentStats = new PlayerStatModel();
    }

    private void OnDestroy()
    {
        string json = Utils.SerializeObject(currentStats);
        //Debug.LogError(json);
        Utils.WriteTextToFile(STAT_FILE, json);
    }


}
