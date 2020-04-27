using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BoardPlayerUI : MonoBehaviour
{
    #region USER INTERFACE
    public void SetPlayerType(LudoType playerType)
    {
        for (int i = 0; i < pawns.Length; i++)
            pawns[i].SetPawnType(playerType);
        this.playerType = playerType;
    }

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    // DICE UI ----------------------------------------
    public void DisplayDice() => diceUI.gameObject.SetActive(true);
    public void HideDice() => diceUI.gameObject.SetActive(false);
    public void RollDice(UnityAction onAnimationEnded) => diceUI.RollDice(onAnimationEnded);
    public void DisplayDiceFace(int face) => diceUI.DisplayFace(face);
    public void ResetDice() => diceUI.Reset();

    public void EnableDice(UnityAction onDiceTapped)
    {
        diceUI.EnableInteraction();
        diceUI.SetListeners(onDiceTapped);
    }

    public void DisableDice()
    {
        diceUI.DisableInteraction();
        diceUI.ClearListeners();
    }
    // ------------------------------------------------


    // Pawn UI ----------------------------------------
    public void AssociateToPawns(List<Pawn.PawnID> pawnIDs)
    {
        int i = 0;
        foreach (var pawn in pawns)
            pawn.AssociatePawnTo(pawnIDs[i++]);
    }

    public void AssociatePawns(PawnUI pawnUI, Pawn.PawnID pawnID) => pawnUI.AssociatePawnTo(pawnID);
    
    public void HighlightPawnsInStart()
    {
        foreach(int pawnIndex in pawnsInStart)
        {
            pawns[pawnIndex].HighlightPawn(onPawnTapped: (pawnID) => { });
        }
    }

    public void HighlightPawnsInOpen()
    {
        foreach (int pawnIndex in pawnsInOpen)
        {
            pawns[pawnIndex].HighlightPawn(onPawnTapped: (pawnID) => { });
        }
    }

    /*public void MovePawn(int pawnIndex, Vector2 position, UnityAction<Pawn.PawnID> onMoveCompleted)
    {
        pawns[pawnIndex].MoveToPosition(position, onMoveCompleted);
    }*/
    // ------------------------------------------------
    #endregion

    #region INTERNALS
    [SerializeField]
    private Text playerNameText = null;
    [SerializeField]
    private GameObject homeSpot = null;
    [SerializeField]
    private DiceUI diceUI = null;
    [SerializeField]
    private PawnUI[] pawns = null;
    [SerializeField]
    private PlayerPawnSlotUI[] slots = null;

    private List<int> pawnsInStart = new List<int>();
    private List<int> pawnsInOpen = new List<int>();
    private List<int> pawnsInHome = new List<int>();

    public LudoType playerType { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerType = LudoType.Red;
        HideDice();
        InitPawnsInSlots();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitPawnsInSlots()
    {
        int index = 0;
        foreach (var slot in slots)
        {
            pawnsInStart.Add(index);
            slot.PutPawnInSlot(pawns[index++]);
        }
    }
    #endregion
}
