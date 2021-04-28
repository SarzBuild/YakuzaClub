using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_PlayerDeath : MonoBehaviour
{
    public GameObject UI_GameEndPanel;
    private float timeBeforeEndGame;
    private Sc_PlayerInputs inputManager;
    
    
    private void Start() 
    {
        Time.timeScale = 1;
        inputManager = Sc_PlayerInputs.Instance;
    }
    private void Update() 
    {
        if(Sc_PlayerController.isDead)
        {
            inputManager.SetLockPlayer();
            //animator.SetBool(isDead, true);
            timeBeforeEndGame += Time.deltaTime;
            if(timeBeforeEndGame >= 1.25)
            {
                Time.timeScale = 0;
                if(Time.timeScale < 0.01)
                {
                    UI_GameEndPanel.SetActive(true);
                }
            }
        }    
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Replay()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
