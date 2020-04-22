using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOverPopup : PopupBase
{
    [SerializeField]
    private Button okayButton = null;
    [SerializeField]
    private Text player1stName = null;
    [SerializeField]
    private Text player2ndName = null;

    private UnityAction onOkayPressed = null;

    #region USER INTERFACE
    public void SetListeners(UnityAction onOkayPressed)
    {
        this.onOkayPressed = onOkayPressed;
    }

    public void ClearListeners()
    {
        onOkayPressed = null;
    }

    public void Init(string player1Name, string player2Name, UnityAction onOkayPressed)
    {
        this.onOkayPressed = onOkayPressed;
        player1stName.text = player1Name;
        player2ndName.text = player2Name;
    }

    public void Reset()
    {
        ClearListeners();
        player1stName.text = "Player 1";
        player2ndName.text = "Player 2";
    }
    #endregion

    #region INTERNAL
    public override bool CanHandleBack { get { return true; } }

    // Start is called before the first frame update
    void Start()
    {
        okayButton.onClick.AddListener(OkayPressed);
    }

    private void OkayPressed()
    {
        if (onOkayPressed != null)
            onOkayPressed.Invoke();
    }

    public override void OnBackPressed()
    {
        OkayPressed();
    }
    #endregion
}
