﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance;  }
        set { _instance = value;  }
    }

    int _score= 0;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Current Score is " +_score);
        }
    }

    public int maxLives = 3;
    int _lives ;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {

                Respawn();
            }
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives <0)
            {
                // Game over
            }
            Debug.Log("Current Lives Are" + _lives);
        }
    }

    public GameObject playerInstance;
    public GameObject playerPrefab;
    public LevelManager currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if ( instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("TitleScreen");
            }
            else if( SceneManager.GetActiveScene().name == "TitleScreen")
            {
                SceneManager.LoadScene("Level");
            }
           
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            QuitGame();
        }
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        CameraFollow mainCamera = FindObjectOfType<CameraFollow>();
        EnemyTurret[] turretEnemy = FindObjectsOfType<EnemyTurret>();

        if (mainCamera)
        {
            mainCamera.player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
            playerInstance = mainCamera.player;
        }
        else
        {
            SpawnPlayer(spawnLocation);
        }
    }

    public void Respawn()
    {
        playerInstance.transform.position = currentLevel.spawnLocation.position;
    }
   
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
    EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
