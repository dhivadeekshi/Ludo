using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static void CreateDebugger() {
        GameObject go = new GameObject("Debugger");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<Debugger>();
    }

    public static void Log(string message)
    {
        if (instance == null) return;
        Debug.Log(message);
    }

    public static void LogError(string message)
    {
        if (instance == null) return;
        Debug.LogError(message);
    }


    private static Debugger instance { get; set; }
    private Debugger() { }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
