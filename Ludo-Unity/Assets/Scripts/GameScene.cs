using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        // TODO quit the game and go back properly
        // TEMP ---------
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
        // --------------
    }

}
