using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Fields
    private bool _layerOneActive = true;
    [SerializeField]
    private GameObject _LayerOne;
    [SerializeField]
    private GameObject _LayerTwo;

    private static LevelManager instance;
    #endregion Fields

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>(); // Looks for existing
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(LevelManager).Name;
                    instance = obj.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    //Switches which world the player is in
    public void SwitchWorldLayer()
    {
        
        _layerOneActive = !_layerOneActive;
        if(_layerOneActive)
        {
            _LayerOne.SetActive(true);
            _LayerTwo.SetActive(false);
        }
        else
        {
            _LayerOne.SetActive(false);
            _LayerTwo.SetActive(true);
        }
    }

    //restarts the level by calling the scene name
    public void RestartLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
