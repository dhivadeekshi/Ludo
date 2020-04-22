using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceUI : MonoBehaviour
{
    #region USER INTERFACE

    public void RollDice()
    {
        HideCurrentFace();
        StartDiceRollAnimation();
    }

    public void DisplayFace(int face)
    {
        if (diceFaces != null && face < diceFaces.Length)
        {
            StopDiceRollAnimation();
            HideCurrentFace();
            SetCurrentFace(face);
        }
    }

    public void Reset()
    {
        DisplayFace(0);
    }

    public void EnableInteraction()
    {
        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].GetComponent<Button>().interactable = true;
    }

    public void DisableInteraction()
    {
        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].GetComponent<Button>().interactable = true;
    }

    public void SetListeners(UnityAction onDiceTapped)
    {
        this.onDiceTapped = onDiceTapped;
    }

    public void ClearListeners()
    {
        onDiceTapped = null;
    }

    #endregion


    #region INTERNALS
    [SerializeField]
    private GameObject[] diceFaces = null;
    [SerializeField]
    private GameObject diceRollAnimation = null;

    private GameObject currentFace = null;
    private UnityAction onDiceTapped = null;

    private void Start()
    {
        HideAllFaces();
        StopDiceRollAnimation();
        currentFace = diceFaces[0];
        DisplayFace(0);

        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].GetComponent<Button>().onClick.AddListener(DiceTapped);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DiceTapped()
    {
        if (onDiceTapped != null)
            onDiceTapped.Invoke();
    }

    private void HideAllFaces()
    {
        for (int i = 0; i < diceFaces.Length; i++)
            diceFaces[i].SetActive(false);
    }

    private void HideCurrentFace()
    {
        currentFace.SetActive(false);
    }

    private void SetCurrentFace(int face)
    {
        diceFaces[face].SetActive(true);
        currentFace = diceFaces[face];
    }

    private void StartDiceRollAnimation()
    {
        diceRollAnimation.SetActive(true);
    }

    private void StopDiceRollAnimation()
    {
        diceRollAnimation.SetActive(false);
    }
    #endregion
}
