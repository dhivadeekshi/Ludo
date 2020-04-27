using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveManager : MonoBehaviour
{
    public static void MoveObject(GameObject gameObject, Vector3 position, UnityAction onMoveCompleted)
    {
        gameObject.transform.position = position;
        if (onMoveCompleted != null)
            onMoveCompleted.Invoke();
    }

    public static void MoveObjectTo(GameObject gameObject, Vector2 position, UnityAction onMoveCompleted)
    {
        gameObject.transform.position = position;
        if (onMoveCompleted != null)
            onMoveCompleted.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
