using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public enum EngineType
    {
        DEFAULT,
        WIN,
        PLAY,
        SETTINGS,
        QUIT
    }
    public EngineType type = EngineType.DEFAULT;
    public GameObject connectedEngine;
    private GameObject player = null;
    public GameObject interactIMG = null;

    private void Awake()
    {
        player = null;
        if(interactIMG && (type == EngineType.PLAY ||
            type == EngineType.SETTINGS ||
            type == EngineType.QUIT))
        {
            //interactIMG = GameObject.Find("IMG-ToInteract");
            interactIMG.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            switch (type)
            {
                case EngineType.DEFAULT:
                    //Debug.Log("Activated " + gameObject.name); // Debug
                    //Activate the portal only if the player is on top of it and they are grounded
                    if (connectedEngine)
                    {
                        player.GetComponent<PlayerMovement>().TeleportTo(connectedEngine.transform);
                        GameManager.ChangeLevel();
                        GameManager.MoveCamera(connectedEngine.transform);
                    }
                break;
                case EngineType.WIN:
                    GameManager.Win();
                    break;
                case EngineType.PLAY:
                    //Debug.Log("Play");
                    GameManager.Play();
                    break;
                case EngineType.SETTINGS:
                    //Debug.Log("Settings");
                    GameManager.OpenCloseSettings();
                    break;
                case EngineType.QUIT:
                    //Debug.Log("Quit");
                    Application.Quit(0);
                    break;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if player is within range
        if (collision.tag == "Player")
        {
            //Debug.Log("Activated Engine");
            player = collision.gameObject;

            if (type == EngineType.PLAY ||
                type == EngineType.SETTINGS ||
                type == EngineType.QUIT)
            {
                interactIMG.SetActive(true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Deactivated Engine");
            player = null;

            if (type == EngineType.PLAY ||
                type == EngineType.SETTINGS ||
                type == EngineType.QUIT)
            {
                interactIMG.SetActive(false);
            }
        }
    }
}
