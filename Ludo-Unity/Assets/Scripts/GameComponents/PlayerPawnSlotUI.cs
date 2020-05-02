using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawnSlotUI : MonoBehaviour
{
    public bool IsEmpty() { return pawnUI == null; }
    public void PutPawnInSlot(PawnUI pawnUI) { this.pawnUI = pawnUI; pawnUI.MoveToPosition(GetPosition(), null); }
    public PawnUI TakePawnOutOfSlot() { PawnUI pawnUI = this.pawnUI; this.pawnUI = null; return pawnUI; }
    public bool Contains(PawnUI pawnUI) { return !IsEmpty() && this.pawnUI.pawnID.equals(pawnUI.pawnID); }
    private Vector2 GetPosition() { return transform.position; }
    private PawnUI pawnUI = null;
}
