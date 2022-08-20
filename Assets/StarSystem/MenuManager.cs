using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject levelButton;
    [SerializeField] bool nextLevel;
    [SerializeField] int levelNum;
    [SerializeField] int homeNum;
    [SerializeField] int currentScene;
    [SerializeField] TMP_Text stateText;

    [SerializeField] Animator starAnim;
    [SerializeField] AudioSource ad;

    private void Awake(){
        if(!nextLevel){
            levelButton.SetActive(false);
        }
    }

    //Replays the level;
    public void Replay(){
        SceneManager.LoadScene(currentScene);
        Sound();
    }

    //Loads the next level;
    public void NextLevel(){
        SceneManager.LoadScene(levelNum);
        Sound();
    }

    //Takes player back to main menu
    public void Home(){
        SceneManager.LoadScene(homeNum);
        Sound();
    }

    //Input a one two three or none for stars. 
    public void ShowStars(string stars, string state){
        stateText.text = state;
        starAnim.SetTrigger(stars);
    }

    private void Sound(){
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }
}
