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
    // Update is called once per frame
    protected override void Update()
    {
        //Debug.Log("uhhh");
        base.Update();
    }

    public override void ActivateEngine()
    {
        base.ActivateEngine();
        Debug.Log("Going to " + _sceneName);
        SceneManager.LoadScene(_sceneName);
    }
}
