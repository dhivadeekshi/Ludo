using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using PawnUIID = PawnUI.PawnUIID;
using PlayerType = LudoType;

public class BoardPlayerUI : MonoBehaviour
{
    #region USER INTERFACE
    public void Init(PlayerType playerType)
    {
        InitPawns(playerType);
        this.playerType = playerType;
    }

    public void SetPlayerName(string playerName)=> playerNameText.text = playerName;

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
    public void HighlightPawns(List<PawnUIID> pawnUIIDs, UnityAction<PawnUIID> onPawnTapped)
    {
        foreach(var pawnUIID in pawnUIIDs)
        {
            var pawn = GetPawn(pawnUIID);
            if (pawn != null) pawn.HighlightPawn(onPawnTapped);
        }
    }

    public void StopAllHighlights()
    {
        foreach (var pawn in pawns)
            pawn.StopHighlight();
    }

    public void ReturnPawnToEmptySlot(PawnUIID pawnUIID)
    {
        var pawnUI = GetPawn(pawnUIID);
        var slot = GetEmptySlot();
        slot.PutPawnInSlot(pawnUI);
    }

    public void TakePawnOutOfSlot(PawnUIID pawnUIID)
    {
        var slot = GetSlotContains(pawnUIID);
        slot.TakePawnOutOfSlot();
    }

    public void MovePawn(PawnUIID pawnUIID, Vector2 position, UnityAction<PawnUIID> onMoveCompleted)
    {
        GetPawn(pawnUIID).MoveToPosition(position, onMoveCompleted);
    }

    public List<PawnUIID> GetAllPawns()
    {
        var pawnUIIDs = new List<PawnUIID>();
        foreach (var pawn in pawns)
            pawnUIIDs.Add(pawn.pawnID);
        return pawnUIIDs;
    }

    public void MovePawnToHome(PawnUIID pawnUIID, UnityAction<PawnUIID> onMoveCompleted)
    {
        MovePawn(pawnUIID, GetHomePosition(), onMoveCompleted);
    }

    public void ResizePawn(PawnUIID pawnUIID, Vector3 scale)
    {
        var pawn = GetPawn(pawnUIID);
        pawn.Resize(scale);
    }
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

    public PlayerType playerType { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerType = PlayerType.Red;
        HideDice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitPawns(PlayerType playerType)
    {
        for (int i = 0; i < pawns.Length; i++)
        {
            pawns[i].Init(playerType, new PawnUIID(i));
            slots[i].PutPawnInSlot(pawns[i]);
        }
    }

    private PlayerPawnSlotUI GetEmptySlot()
    {
        foreach(var slot in slots)
        {
            if (slot.IsEmpty()) return slot;
        }
        return null;
    }

    private PlayerPawnSlotUI GetSlotContains(PawnUIID pawnUIID)
    {
        var pawnUI = GetPawn(pawnUIID);
        foreach (var slot in slots)
        {
            if (slot.Contains(pawnUI)) return slot;
        }
        return null;
    }

    private PawnUI GetPawn(PawnUIID pawnUIID)
    {
        foreach(var pawn in pawns)
        {
            if (pawn.pawnID.equals(pawnUIID))
                return pawn;
        }
        return null;
    }

    private Vector2 GetHomePosition() => homeSpot.transform.position;
    #endregion
}
