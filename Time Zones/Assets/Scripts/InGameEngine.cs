using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEngine : DefaultEngine
{
    #region Fields
    [SerializeField]
    private LevelManager _levelManager;
    #endregion

    protected override void Update()
    {
        base.Update();
    }
    public override void ActivateEngine()
    {
        base.ActivateEngine();
        Debug.Log("But now its fancy");
        //Switch to the other level layer
        _levelManager.SwitchWorldLayer();
    }
}
