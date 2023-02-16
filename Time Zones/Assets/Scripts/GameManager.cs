using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MAIN_MENU,
        PLAY,
        PAUSE
    }
    private static GameState state;
    private static GameManager instance;
    private static PlayerMovement playerScript;
    private static Vector3 menuPos;
    //private static UIManager ui;
    private static GameObject spawn;
    private static GameObject mainCamera;

    private static bool inLavaLevel = false;
    public GameObject levelsContainer;
    private static GameObject[] levels;
    public GameObject coldSnappersContainer;
    private static GameObject[] coldSnappers;
    public GameObject furnaceFliesContainer;
    private static GameObject[] furnaceFlies;

    // UI
    public static GameObject pauseMenu;
    public static GameObject settingsMenu;

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
            //DontDestroyOnLoad(gameObject); // Will likely need this to store data bw deaths
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Spawn");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (player)
        {
            playerScript = player.GetComponent<PlayerMovement>();
        }
        menuPos = new Vector3(8.0f, 0.0f, 0.0f);
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Get levels
        int lChildCount = levelsContainer.transform.childCount;
        levels = new GameObject[lChildCount];
        for (int i = 0; i < lChildCount; i++)
        {
            levels[i] = levelsContainer.transform.GetChild(i).gameObject;
            if (i > 0) { levels[i].SetActive(false); } // Start with menu
        }

        // Get Cold Snappers
        int csChildCount = coldSnappersContainer.transform.childCount;
        coldSnappers = new GameObject[csChildCount];
        for (int i = 0; i < csChildCount; i++)
        {
            coldSnappers[i] = coldSnappersContainer.transform.GetChild(i).gameObject;
        }

        // Get Furnace Flies
        int ffChildCount = furnaceFliesContainer.transform.childCount;
        furnaceFlies = new GameObject[ffChildCount];
        for (int i = 0; i < ffChildCount; i++)
        {
            furnaceFlies[i] = furnaceFliesContainer.transform.GetChild(i).gameObject;
        }

        pauseMenu = GameObject.Find("PauseMenu");
        settingsMenu = GameObject.Find("SettingsMenu");
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        state = GameState.MAIN_MENU;
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
                playerScript.Updatee();
                break;
            case GameState.PLAY:
                playerScript.Updatee();

                if(inLavaLevel)
                {
                    foreach (GameObject furnaceFly in furnaceFlies)
                    {
                        if(furnaceFly.activeInHierarchy)
                        furnaceFly.GetComponent<FurnaceFly>().Updatee();
                    }    
                }
                else
                {
                    foreach (GameObject coldSnapper in coldSnappers)
                    {
                        if(coldSnapper.activeInHierarchy)
                        coldSnapper.GetComponent<ColdSnapper>().Updatee();
                    }
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
                playerScript.FixedUpdatee();
                break;
            case GameState.PLAY:
                playerScript.FixedUpdatee();
                if(!inLavaLevel)
                {
                    foreach (GameObject coldSnapper in coldSnappers)
                    {
                        coldSnapper.GetComponent<ColdSnapper>().FixedUpdatee();
                    }
                }
                break;
            case GameState.PAUSE:
                break;
        }
    }
    public static void ChangeLevel()
    {
        if (inLavaLevel)
        {
            levels[2].SetActive(true);
            levels[1].SetActive(false);
        }
        else
        {
            levels[1].SetActive(true);
            levels[2].SetActive(false);
        }

        inLavaLevel = !inLavaLevel;
    }
    public static void Win()
    {
        Debug.Log("Win");
    }
    public static void Play()
    {
        levels[0].SetActive(false);
        levels[1].SetActive(true);
        playerScript.TeleportTo(spawn.transform);
        MoveCamera(spawn.transform);
        inLavaLevel = true;
        state = GameState.PLAY;
    }
    private static void PausePlay()
    {
        if(state == GameState.PLAY)
        {
            state = GameState.PAUSE;
            pauseMenu.SetActive(true);
            playerScript.gameObject.GetComponent<Animator>().speed = 0;
        }
        else if(state == GameState.PAUSE)
        {
            state = GameState.PLAY;
            pauseMenu.SetActive(false);
            playerScript.gameObject.GetComponent<Animator>().speed = 1;
        }

        //Time.timeScale = 0.0f; // Older jank method
    }
    public static void OpenCloseSettings()
    {
        if (state == GameState.MAIN_MENU)
        {
            state = GameState.PAUSE;
            settingsMenu.SetActive(true);
            playerScript.gameObject.GetComponent<Animator>().speed = 0;
        }
        else if (state == GameState.PAUSE)
        {
            state = GameState.MAIN_MENU;
            settingsMenu.SetActive(false);
            playerScript.gameObject.GetComponent<Animator>().speed = 1;
        }
    }
    public static void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // THis will restart the whole scene from menu
        playerScript.TeleportTo(spawn.transform);
        MoveCamera(spawn.transform);
        levels[0].SetActive(false);
        levels[1].SetActive(true);
        levels[2].SetActive(false);
        pauseMenu.SetActive(false);
        inLavaLevel = true;
        ResetEnemies();
        //Camera.main.transform.position = playerScript.gameObject.transform.position;
    }
    public static void Resume()
    {
        PausePlay();
    }
    public static void ToMenu()
    {
        levels[0].SetActive(true);
        levels[1].SetActive(false);
        levels[2].SetActive(false);
        pauseMenu.SetActive(false);
        mainCamera.transform.position = menuPos;
        playerScript.TeleportTo(menuPos);
        inLavaLevel = false;
        state = GameState.MAIN_MENU;
        playerScript.gameObject.GetComponent<Animator>().speed = 1;
    }

    public static void ResetEnemies()
    {
        foreach(GameObject snapper in coldSnappers)
        {
            snapper.SetActive(true);
        }

        foreach(GameObject fly in furnaceFlies)
        {
            fly.SetActive(true);
        }
    }

    internal static void MoveCamera(Transform transform)
    {
        Vector3 move = transform.position;
        move.y += 0.5f;
        mainCamera.transform.position = move;
    }

}
