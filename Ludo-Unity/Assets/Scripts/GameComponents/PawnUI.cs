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
        public override string ToString() => string.Format("{0}", id);
    }

    public void Init(PawnType pawnType, PawnUIID pawnID)
    {
        UpdateVisuals(pawnType);
        SetButtonListener(pawnType);
        DisableButton();
        DisableImageRaycastTarget();
        this.pawnID = pawnID;
    }

    public void HighlightPawn(UnityAction<PawnUIID> onPawnTapped)
    {
        this.onPawnTapped = onPawnTapped;
        highlight.SetActive(true);
        EnableButton();
        EnableImageRaycastTarget();
        StartHighlightAnimation();
        IsHighlighted = true;
    }

    public void StopHighlight()
    {
        onPawnTapped = null;
        highlight.SetActive(false);
        DisableImageRaycastTarget();
        DisableButton();
        StopHighlightAnimation();
        IsHighlighted = false;
    }

    public void MoveToPosition(Vector2 position, UnityAction<PawnUIID> onMoveCompleted)
    {
        MoveManager.MoveObjectTo(gameObject, position, onMoveCompleted: () =>
         {
             if (onMoveCompleted != null)
                 onMoveCompleted.Invoke(pawnID);
         });
    }

    public void Shrink() => StartShrinkAnimation();
    public void ReturnToNormal() => StartReturnToNormalAnimation();

    public void ShrinkToPosition(Vector2 position)
    {
        StartShrinkAnimation();
        MoveToPosition(position, null);
    }

    public void ReturnToNormal(Vector2 position)
    {
        StartReturnToNormalAnimation();
        MoveToPosition(position, null);
    }

    public void SetPawnPosition(Vector2 position) => transform.position = position;
    public void Resize(Vector3 scale) => transform.localScale = scale;
    public void ResetSize() => transform.localScale = Vector3.one;
    public PawnType pawnType { get; private set; }
    public PawnUIID pawnID { get; private set; }
    public bool IsHighlighted { get; private set; }

    #endregion

    #region INTERNAL

    [SerializeField]
    private GameObject[] images = null;
    [SerializeField]
    private GameObject highlight = null;
    [SerializeField]
    private Animator animator = null;
    private Button button = null;

    private UnityAction<PawnUIID> onPawnTapped = null;

    // Start is called before the first frame update
    void Start()
    {
        StopHighlight();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonTapped()
    {
        Debugger.Log("Pawn Selected : " + pawnType + " : " + pawnID);
        if (onPawnTapped != null)
            onPawnTapped.Invoke(pawnID);
    }

    private void SetButtonListener(PawnType pawnType)
    {
        button = images[(int)pawnType].GetComponent<Button>();
        button.onClick.AddListener(ButtonTapped);
    }

    private void EnableImageRaycastTarget() => button.GetComponent<Image>().raycastTarget = true;
    private void DisableImageRaycastTarget() { if (button != null) button.GetComponent<Image>().raycastTarget = false; }
    private void EnableButton() => button.interactable = true;
    private void DisableButton() { if (button != null) button.interactable = false; }

    private void UpdateVisuals(PawnType pawnType)
    {
        images[(int)this.pawnType].SetActive(false);
        images[(int)pawnType].SetActive(true);
        this.pawnType = pawnType;
    }
    
    private void StopHighlightAnimation() => animator.Play(Constants.Pawn.IdleAnimationName);
    private void StartHighlightAnimation() => animator.Play(Constants.Pawn.HighlightAnimationName);
    private void StartMoveHighlightAnimation() => animator.Play(Constants.Pawn.MoveHighlightAnimationName);
    private void StartShrinkAnimation() => animator.Play(Constants.Pawn.ShrinkAnimationName);
    private void StartReturnToNormalAnimation() => animator.Play(Constants.Pawn.ResetSizeAnimationName);
    #endregion

}
