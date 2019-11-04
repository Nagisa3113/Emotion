using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    public virtual void OnEnter(object args = null) { }

    public virtual void OnPause() { }
 
    public virtual void OnResume() { }
 
    public virtual void OnExit() { }
}
