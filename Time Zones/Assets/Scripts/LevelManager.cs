using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Fields
    private bool _layerOneActive = true;
    [SerializeField]
    private GameObject _LayerOne;
    [SerializeField]
    private GameObject _LayerTwo;
    #endregion Fields
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
