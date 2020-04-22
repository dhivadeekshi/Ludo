using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameQuitPopup : MonoBehaviour
{
    [SerializeField]
    private Button noButton = null;
    [SerializeField]
    private Button yesButton = null;

    private UnityAction onYesPressed = null;
    private UnityAction onNoPressed = null;

    #region USER INTERFACE
    public void SetListeners(UnityAction onYesPressed, UnityAction onNoPressed)
    {
        this.onYesPressed = onYesPressed;
        this.onNoPressed = onNoPressed;
    }

    public void ClearListeners()
    {
        onNoPressed = null;
        onYesPressed = null;
    }
    #endregion

    #region INTERNALS 
    // Start is called before the first frame update
    void Start()
    {
        noButton.onClick.AddListener(NoPressed);
        yesButton.onClick.AddListener(YesPressed);
    }

    private void NoPressed()
    {
        if (onNoPressed != null)
            onNoPressed.Invoke();
    }

    private void YesPressed()
    {
        if (onYesPressed != null)
            onYesPressed.Invoke();
    }
    #endregion
}
