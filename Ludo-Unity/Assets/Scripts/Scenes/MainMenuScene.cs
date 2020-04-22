using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneNames.GameSceneName);
    }
}
