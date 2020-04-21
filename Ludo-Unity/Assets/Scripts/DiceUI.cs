using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] diceFaces = null;
    [SerializeField]
    private GameObject diceRollAnimation = null;

    private GameObject currentFace = null;
    private UnityAction onDiceRolled = null;

    private void Start()
    {
        HideAllFaces();
        StopRoll();
        currentFace = diceFaces[0];
        DisplayFace(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnRollDiceListener(UnityAction onDiceRoll)
    {
        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].GetComponent<Button>().onClick.AddListener(onDiceRoll);
    }

    public void SetDiceRollAnimationEndedListener(UnityAction onDiceRolled)
    {
        this.onDiceRolled += onDiceRolled;
    }

    public void RollDice()
    {
        Debug.Log("Roll Dice");
        currentFace.SetActive(false);
        diceRollAnimation.SetActive(true);
        StartCoroutine(DiceRolling());
    }

    private IEnumerator DiceRolling()
    {
        yield return new WaitForSecondsRealtime(Constants.DiceRollAnimationDuration);
        if (onDiceRolled != null)
            onDiceRolled.Invoke();
    }

    public void StopRoll()
    {
        diceRollAnimation.SetActive(false);
    }

    public void DisplayFace(int face)
    {
        if(diceFaces != null && face < diceFaces.Length)
        {
            StopRoll();
            currentFace.SetActive(false);
            diceFaces[face].SetActive(true);
            currentFace = diceFaces[face];
        }
    }

    private void HideAllFaces()
    {
        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].SetActive(false);
    }
}
