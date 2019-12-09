using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onRetryClick() {
        SceneManager.LoadScene("Main");
    }

    public void onQuitClick() {
        Application.Quit();
    }

    public void onTitleClick() {
        SceneManager.LoadScene("Title");
    }
}
