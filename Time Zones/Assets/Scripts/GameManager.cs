using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAIN_MENU,
        PLAY,
        PAUSE
    }
    private GameState state;
    private static GameManager instance;
    private GameObject player;
    private PlayerMovement playerScript;
    //private static UIManager ui;

    public GameObject coldSnappersContainer;
    private GameObject[] coldSnappers;

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

        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerScript = player.GetComponent<PlayerMovement>();
        }
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int childCount = coldSnappersContainer.transform.childCount;
        coldSnappers = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            coldSnappers[i] = coldSnappersContainer.transform.GetChild(i).gameObject;
        }

        state = GameState.PLAY;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausePlay();
        }

        switch (state)
        {
            case GameState.MAIN_MENU:
                //playerScript.Update();
                break;
            case GameState.PLAY:
                //playerScript.Update();
                foreach (GameObject coldSnapper in coldSnappers)
                {
                    coldSnapper.GetComponent<ColdSnapper>().Updatee();
                }
                break;
            case GameState.PAUSE:
                break;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case GameState.MAIN_MENU:
                break;
            case GameState.PLAY:
                foreach (GameObject coldSnapper in coldSnappers)
                {
                    coldSnapper.GetComponent<ColdSnapper>().FixedUpdatee();
                }
                break;
            case GameState.PAUSE:
                break;
        }
    }

    private void PausePlay()
    {
        if(state == GameState.PLAY)
        {
            state = GameState.PAUSE;
        }
        else if(state == GameState.PAUSE)
        {
            state = GameState.PLAY;
        }

        //Time.timeScale = 0.0f; // Older jank method
    }
}
