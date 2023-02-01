using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PLAY,
        PAUSE
    }
    private GameState state;
    private static GameManager instance;
    //private static UIManager ui;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>(); // Looks for existing
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        switch (state)
        {
            case GameState.PLAY:
                break;
            case GameState.PAUSE:
                break;
        }
    }
    private void FixedUpdate()
    {
        // Physics   
    }

    public void Resume()
    {
        state = GameState.PLAY;
    }
    public void Pause()
    {
        state = GameState.PAUSE;
    }
}
