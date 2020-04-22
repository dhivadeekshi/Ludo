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
        // TEMP ---------------
        pawnButtons[0].onClick.AddListener(() => { SelectPawn(0); });
        pawnButtons[1].onClick.AddListener(() => { SelectPawn(1); });
        pawnButtons[2].onClick.AddListener(() => { SelectPawn(2); });
        pawnButtons[3].onClick.AddListener(() => { SelectPawn(3); });
        // --------------------
        /*for (int i = 0; i < pawnButtons.Length; i++)
            pawnButtons[i].onClick.AddListener(() => { SelectPawn(i); });*/
        //pawnButtons[i].onClick.AddListener(SelectPawn);
        //pawnButtons[i].onClick.AddListener(() => { int pawn = i; SelectPawn(pawn); });
    }

    private void SelectPawn(int pawn)
    {
        if (onPawnSelected != null)
            onPawnSelected.Invoke((LudoType)pawn);
    }
    #endregion
}
