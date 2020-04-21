using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Button backButton = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnBackListener(UnityAction onBackClick)
    {
        backButton.onClick.AddListener(onBackClick);
    }
}
