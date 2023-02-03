using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEngine : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private EngineActivationRange _activationRange;
    
    #endregion Fields
    protected virtual void Update()
    {
        if(_activationRange.isPlayerActivating && Input.GetKeyDown(KeyCode.E))
        {
            ActivateEngine();
        }
    }
    public virtual void ActivateEngine()
    {
        Debug.Log("Activated " + gameObject.name);
    }

}
