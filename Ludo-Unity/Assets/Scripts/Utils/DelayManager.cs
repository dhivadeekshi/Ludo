using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayManager : MonoBehaviour
{
    public void WaitForDuration(float sec, UnityAction onElapsed)
    {
        DelayWaitForSec delayWaitForSec = gameObject.AddComponent<DelayWaitForSec>();
        delayWaitForSec.Execute(sec, ()=> {
            Destroy(delayWaitForSec);
            if (onElapsed != null) onElapsed.Invoke();
        });
    }

}
