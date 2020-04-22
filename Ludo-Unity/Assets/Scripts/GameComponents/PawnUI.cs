using UnityEngine;
using UnityEngine.Events;

public class PawnUI : MonoBehaviour
{
    #region USER INTERFACE

    public void MoveToPosition(Vector2 position, UnityAction onMoveCompleted)
    {
        onPawnMoveEnded = onMoveCompleted;
        // TODO update position
    }

    public void HighlightPawn(UnityAction onPawnTapped)
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

    #endregion

    #region INTERNAL

    [SerializeField]
    private GameObject[] images = null;
    [SerializeField]
    private GameObject highlight = null;

    private UnityAction onPawnMoveEnded = null;
    private UnityAction onPawnTapped = null;

    private LudoType pawnType = LudoType.Red;

    // Start is called before the first frame update
    void Start()
    {
        StopHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
        /*if (Input.GetTouch(0).position == (Vector2)transform.position)
        {
            Debug.Log("Tapped on pawn of type: " + pawnType);
            // Detect interaction
            if (onPawnTapped != null)
                onPawnTapped.Invoke();
        }*/

    }

    private void UpdateVisuals(LudoType pawnType)
    {
        images[(int)this.pawnType].SetActive(false);
        images[(int)pawnType].SetActive(true);
        this.pawnType = pawnType;
    }
    #endregion


}
