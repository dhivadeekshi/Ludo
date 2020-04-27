using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawnSlotUI : MonoBehaviour
{
    public bool IsSlotEmpty() { return pawnUI == null; }
    public void PutPawnInSlot(PawnUI pawnUI) { this.pawnUI = pawnUI; pawnUI.MoveToPosition(GetPosition(), null); }
    public PawnUI TakeOutThePwan() { PawnUI pawnUI = this.pawnUI; this.pawnUI = null; return pawnUI; }

    /*TEMP*/[SerializeField]
    private PawnUI pawnUI = null;
    private Vector2 GetPosition() { return transform.position; }
}
