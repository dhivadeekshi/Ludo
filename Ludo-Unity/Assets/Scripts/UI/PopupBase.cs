using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBase : MonoBehaviour
{
    public virtual bool CanHandleBack { get { return false; } }
    public virtual void OnBackPressed()
    {

    }
}
