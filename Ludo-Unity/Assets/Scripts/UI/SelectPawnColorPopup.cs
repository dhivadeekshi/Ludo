using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectPawnColorPopup : PopupBase
{
    [SerializeField]
    private Button[] pawnButtons = null;

    private UnityAction<LudoType> onPawnSelected = null;

    #region USER INTERFACE
    public void SetListeners(UnityAction<LudoType> onPawnSelected)
    {
        this.onPawnSelected += onPawnSelected;
    }

    public void RemoveListener(UnityAction<LudoType> onPawnSelected)
    {
        this.onPawnSelected -= onPawnSelected;
    }

    public void ClearListeners()
    {
        onPawnSelected = null;
    }
    #endregion

    #region INTERNALS
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < pawnButtons.Length; i++)
            pawnButtons[i].onClick.AddListener(() => { SelectPawn(i + 1); });
    }

    private void SelectPawn(int pawn)
    {
        if (onPawnSelected != null)
            onPawnSelected.Invoke((LudoType)pawn);
    }
    #endregion
}
