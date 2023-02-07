using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEngine : MonoBehaviour
{
    public enum EngineType
    {
        DEFAULT,
        TRANSMITTER,
        RECIEVER,
        WIN,
        PLAY,
        SETTINGS,
        QUIT
    }
    public EngineType type = EngineType.DEFAULT;
    public GameObject connectedEngine;
    private GameObject player = null;

    private void Awake()
    {
        player = null;
    }

    protected virtual void Update()
    {
        if (player && Input.GetKeyDown(KeyCode.E))
        {
            switch (type)
            {
                case EngineType.TRANSMITTER:
                    //Debug.Log("Activated " + gameObject.name); // Debug
                    //Activate the portal only if the player is on top of it and they are grounded
                    if (connectedEngine)
                    {
                        player.GetComponent<PlayerMovement>().TeleportTo(connectedEngine.transform);
                        GameManager.ChangeLevel();
                    }
                break;
                case EngineType.WIN:
                    GameManager.Win();
                    break;
                case EngineType.PLAY:
                    //Debug.Log("Play");
                    GameManager.Play();
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
            player = collision.gameObject;
            //Debug.Log("Activated Engine");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            //Debug.Log("Deactivated Engine");
        }
    }
}
