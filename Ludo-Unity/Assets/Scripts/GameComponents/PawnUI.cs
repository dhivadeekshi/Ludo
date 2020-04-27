using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PawnUI : MonoBehaviour
{
    #region USER INTERFACE

    public void MoveToPosition(Vector2 position, UnityAction<Pawn.PawnID> onMoveCompleted)
    {
        onPawnMoveEnded = onMoveCompleted;
        // TODO update position
    }

    public void SetPawnPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void HighlightPawn(UnityAction<Pawn.PawnID> onPawnTapped)
    {
        this.onPawnTapped = onPawnTapped;
        highlight.SetActive(true);
    }

    public void StopHighlight()
    {
        onPawnTapped = null;
        highlight.SetActive(false);
    }

    public void SetPawnType(LudoType pawnType)
    {
        UpdateVisuals(pawnType);
    }

    public LudoType GetPawnType()
    {
        return pawnType;
    }

    public void AssociatePawnTo(Pawn.PawnID pawnID) { this.pawnID = pawnID; }
    public Pawn.PawnID GetAssociatedPawnID() { return pawnID; }
    public void ClearAssociatedPawnID() { pawnID = Pawn.PawnID.nullID; }

    #endregion

    #region INTERNAL

    [SerializeField]
    private GameObject[] images = null;
    [SerializeField]
    private GameObject highlight = null;
    [SerializeField]
    private Button button = null;

    private UnityAction<Pawn.PawnID> onPawnMoveEnded = null;
    private UnityAction<Pawn.PawnID> onPawnTapped = null;

    private LudoType pawnType = LudoType.Red;
    private Pawn.PawnID pawnID = Pawn.PawnID.nullID;

    // Start is called before the first frame update
    void Start()
    {
        StopHighlight();
        button.onClick.AddListener(ButtonTapped);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonTapped()
    {
        if (onPawnTapped != null)
            onPawnTapped.Invoke(pawnID);
    }

    private void UpdateVisuals(LudoType pawnType)
    {
        images[(int)this.pawnType].SetActive(false);
        images[(int)pawnType].SetActive(true);
        this.pawnType = pawnType;
    }
    #endregion


}
