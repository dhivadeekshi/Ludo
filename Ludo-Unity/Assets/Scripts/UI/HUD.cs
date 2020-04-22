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
        selectPawnColorPopup.SetListeners((pawnSelected) => {
            HideSelectPawnPopup();
            if (onPawnSelected != null)
                onPawnSelected.Invoke(pawnSelected);
        });
        EnableTransparentLayer();
        selectPawnColorPopup.gameObject.SetActive(true);
    }

    public void HideSelectPawnPopup()
    {
        selectPawnColorPopup.ClearListeners();
        selectPawnColorPopup.gameObject.SetActive(false);
        DisableTransparentLayer();
    }

    public void ShowGameOverPopup(string player1Name, string player2Name, UnityAction onPopupClose)
    {
        EnableTransparentLayer();
        gameOverPopup.Init(player1Name, player2Name, ()=> {
            HideGameOverPopup();
            if (onPopupClose != null)
                onPopupClose.Invoke();
        });
        gameOverPopup.gameObject.SetActive(true);
    }

    public void HideGameOverPopup()
    {
        gameOverPopup.gameObject.SetActive(false);
        gameOverPopup.Reset();
        gameQuitPopup.ClearListeners();
        DisableTransparentLayer();
    }

    public void ShowGameQuitPopup(UnityAction onConfirm)
    {
        EnableTransparentLayer();
        gameQuitPopup.SetListeners(() =>
        {
            HideGameQuitPopup();
            if (onConfirm != null) 
                onConfirm.Invoke();
        },HideGameQuitPopup);
        gameQuitPopup.gameObject.SetActive(true);
    }

    public void HideGameQuitPopup()
    {
        gameQuitPopup.ClearListeners();
        gameQuitPopup.gameObject.SetActive(false);
        DisableTransparentLayer();
    }

    public bool IsPopupActive()
    {
        return isPopupActive;   
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

    private bool isPopupActive = false;

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
        if(isPopupActive)
        {
            // TODO close active popup ?
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
    #endregion
}
