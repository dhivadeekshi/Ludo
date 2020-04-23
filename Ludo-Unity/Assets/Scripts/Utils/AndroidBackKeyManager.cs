using UnityEngine;
using UnityEngine.Events;

public class AndroidBackKeyManager : MonoBehaviour
{

    #region USER INTERFACE
    public static void SetListener(UnityAction onBackPressed)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            CreateManager();
            instance.SetLocalListener(onBackPressed);
        }
    }

    public static void RemoveListener(UnityAction onBackPressed)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            CreateManager();
            instance.RemoveLocalListener(onBackPressed);
        }
    }
    #endregion

    #region INTERNAL
    private static AndroidBackKeyManager instance = null;

    private UnityAction onBackPressed = null;

    // Update is called once per frame
    void Update()
    {
        if (onBackPressed != null && Input.GetKeyDown(KeyCode.Escape))
            onBackPressed.Invoke();
    }

    private void SetLocalListener(UnityAction onBackPressed)
    {
        this.onBackPressed += onBackPressed;
    }

    private void RemoveLocalListener(UnityAction onBackPressed)
    {
        this.onBackPressed -= onBackPressed;
    }

    private static void CreateManager()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("AndroidBackKeyManager");
            instance = go.AddComponent<AndroidBackKeyManager>();
        }
    }
    #endregion
}
