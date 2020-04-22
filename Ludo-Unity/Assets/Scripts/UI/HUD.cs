using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{

    #region USER INTERFACE
    public void SetOnBackListener(UnityAction onBackClick)
    {
        this.onBackClick = onBackClick;
    }

    public void ShowSelectPawnPopup(UnityAction<LudoType> onPawnSelected)
    {
        selectPawnColorPopup.SetListeners(onPawnSelected:(pawnSelected) => {
            HideSelectPawnPopup();
            if (onPawnSelected != null)
                onPawnSelected.Invoke(pawnSelected);
        });
        ShowPopup(selectPawnColorPopup);
    }

    public void HideSelectPawnPopup()
    {
        selectPawnColorPopup.ClearListeners();
        ClosePopup(selectPawnColorPopup);
    }

    public void ShowGameOverPopup(string player1Name, string player2Name, UnityAction onPopupClose)
    {
        gameOverPopup.Init(player1Name, player2Name, onOkayPressed:()=> {
            HideGameOverPopup();
            if (onPopupClose != null)
                onPopupClose.Invoke();
        });
        ShowPopup(gameOverPopup);
    }

    public void HideGameOverPopup()
    {
        gameOverPopup.ClearListeners();
        gameOverPopup.Reset();
        ClosePopup(gameQuitPopup);
    }

    public void ShowGameQuitPopup(UnityAction onConfirm)
    {
        gameQuitPopup.SetListeners(onYesPressed:() =>
        {
            HideGameQuitPopup();
            if (onConfirm != null) 
                onConfirm.Invoke();
        },onNoPressed:HideGameQuitPopup);
        ShowPopup(gameQuitPopup);
    }

    public void HideGameQuitPopup()
    {
        gameQuitPopup.ClearListeners();
        ClosePopup(gameQuitPopup);
    }

    public bool IsPopupActive()
    {
        return activePopup != null;   
    }
    #endregion

    #region INTERNAL
    [SerializeField]
    private GameObject transparentLayer = null;

    [SerializeField]
    private Button backButton = null;

    [SerializeField]
    private SelectPawnColorPopup selectPawnColorPopup = null;
    [SerializeField]
    private GameOverPopup gameOverPopup = null;
    [SerializeField]
    private GameQuitPopup gameQuitPopup = null;

    private UnityAction onBackClick = null;

    private PopupBase activePopup = null;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(back);
        AndroidBackKeyManager.SetListener(back);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void back()
    {
        if(IsPopupActive() && activePopup.CanHandleBack)
        {
            activePopup.OnBackPressed();
        }
        else
        {
            if (onBackClick != null)
                onBackClick.Invoke();
        }
    }

    private void EnableTransparentLayer()
    {
        transparentLayer.SetActive(true);
    }

    private void DisableTransparentLayer()
    {
        transparentLayer.SetActive(false);
    }

    private void ShowPopup(PopupBase popup)
    {
        EnableTransparentLayer();
        popup.gameObject.SetActive(true);
        SetActivePopup(popup);
    }

    private void ClosePopup(PopupBase popup)
    {
        popup.gameObject.SetActive(false);
        ClearActivePopup();
        DisableTransparentLayer();
    }

    private void SetActivePopup(PopupBase popup)
    {
        activePopup = popup;
    }

    private void ClearActivePopup()
    {
        activePopup = null;
    }
    #endregion
}
