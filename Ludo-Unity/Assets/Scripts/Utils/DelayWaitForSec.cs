using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayWaitForSec : MonoBehaviour
{
    public void Execute(float sec, UnityAction onElapsed)
    {
        StartCoroutine(Wait(sec, onElapsed));
    }

    private IEnumerator Wait(float sec, UnityAction onElapsed)
    {
        yield return new WaitForSeconds(sec);
        if (onElapsed != null)
            onElapsed.Invoke();
    }
}
