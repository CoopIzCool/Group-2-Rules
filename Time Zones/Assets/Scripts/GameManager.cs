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
    private static GameObject spawnEngine;

    private static bool inLavaLevel = false;
    public GameObject levelsContainer;
    private static GameObject[] levels;
    public GameObject coldSnappersContainer;
    private GameObject[] coldSnappers;
    public GameObject furnaceFliesContainer;
    private GameObject[] furnaceFlies;

    // UI
    public static GameObject pauseMenu;

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
            if (i == 1)
            {
                spawnEngine = levels[1].transform.GetChild(1).gameObject; // Get Spawn
            }
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
        pauseMenu.SetActive(false);

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
                        furnaceFly.GetComponent<FurnaceFly>().Updatee();
                    }    
                }
                else
                {
                    foreach (GameObject coldSnapper in coldSnappers)
                    {
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
        playerScript.TeleportTo(spawnEngine.transform);
        inLavaLevel = true;
        state = GameState.PLAY;
    }
    private static void PausePlay()
    {
        if(state == GameState.PLAY)
        {
            state = GameState.PAUSE;
            pauseMenu.SetActive(true);
        }
        else if(state == GameState.PAUSE)
        {
            state = GameState.PLAY;
            pauseMenu.SetActive(false);
        }

        //Time.timeScale = 0.0f; // Older jank method
    }
    public static void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // THis will restart the whole scene from menu
        playerScript.TeleportTo(spawnEngine.transform);
        levels[0].SetActive(false);
        levels[1].SetActive(true);
        levels[2].SetActive(false);
        pauseMenu.SetActive(false);
        inLavaLevel = true;
    }
    public static void Resume()
    {
        PausePlay();
    }
    public static void ToMenu()
    {
        levels[0].SetActive(true);
        levels[1].SetActive(false);
        pauseMenu.SetActive(false);
        playerScript.TeleportTo(menuPos);
        inLavaLevel = false;
        state = GameState.MAIN_MENU;
    }
}
