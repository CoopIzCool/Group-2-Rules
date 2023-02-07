using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEngine : DefaultEngine
{
    #region Fields
    [SerializeField]
    private string _sceneName;
    #endregion Fields
    public enum ButtonType
    {
        PLAY,
        QUIT,
        SETTINGS
    };

    public ButtonType bType;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //public override void ActivateEngine()
    //{
    //    switch(bType)
    //    {
    //        case ButtonType.PLAY:
    //            base.ActivateEngine();
    //            Debug.Log("Going to " + _sceneName);
    //            //Go to the level selected in the inspector
    //            SceneManager.LoadScene(_sceneName);
    //            break;
    //        case ButtonType.QUIT:
    //            Application.Quit(0);
    //            break;
    //        case ButtonType.SETTINGS:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
