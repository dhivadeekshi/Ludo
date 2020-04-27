using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using PawnType = LudoType;

public class PawnUI : MonoBehaviour
{
    #region USER INTERFACE
    public struct PawnUIID
    {
        public int id { get; private set; }
        public PawnUIID(int id) => this.id = id;
        public static PawnUIID nullID { get { return new PawnUIID(-1); } }
        public bool equals(PawnUIID otherID) => this.id == otherID.id;
    }

    public void Init(PawnType pawnType, PawnUIID pawnID)
    {
        UpdateVisuals(pawnType);
        this.pawnID = pawnID;
    }

    public void HighlightPawn(UnityAction<PawnUIID> onPawnTapped)
    {
        this.onPawnTapped = onPawnTapped;
        highlight.SetActive(true);
    }

    public void StopHighlight()
    {
        onPawnTapped = null;
        highlight.SetActive(false);
    }

    public void MoveToPosition(Vector2 position, UnityAction<PawnUIID> onMoveCompleted)
    {
        onPawnMoveEnded = onMoveCompleted;
        // TODO update position
    }

    public void SetPawnPosition(Vector2 position) => transform.position = position;
    public void Resize(Vector3 scale) => transform.localScale = scale;
    public void ResetSize() => transform.localScale = Vector3.one;
    public PawnType pawnType { get; private set; }
    public PawnUIID pawnID { get; private set; }

    #endregion

    #region INTERNAL

    [SerializeField]
    private GameObject[] images = null;
    [SerializeField]
    private GameObject highlight = null;
    [SerializeField]
    private Button button = null;

    private UnityAction<PawnUIID> onPawnMoveEnded = null;
    private UnityAction<PawnUIID> onPawnTapped = null;

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

    private void UpdateVisuals(PawnType pawnType)
    {
        images[(int)this.pawnType].SetActive(false);
        images[(int)pawnType].SetActive(true);
        this.pawnType = pawnType;
    }
    #endregion


}
