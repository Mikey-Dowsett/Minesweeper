using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject frontScreen;
    [SerializeField] GameObject gameSelect;
    [SerializeField] GameObject howScreen;
    [SerializeField] GameObject howOne;
    [SerializeField] GameObject howTwo;
    [SerializeField] AudioSource ad;

    public void Play(){
        Sound();
        SceneManager.LoadScene(1);
    }

    public void ShowSelect(){
        frontScreen.SetActive(false);
        gameSelect.SetActive(true);
        Sound();
    }

    public void Back(){
        gameSelect.SetActive(false);
        howScreen.SetActive(false);
        frontScreen.SetActive(true);
        Sound();
    }

    public void HowTo(){
        frontScreen.SetActive(false);
        howScreen.SetActive(true);
        howOne.SetActive(true);
        howTwo.SetActive(false);
        Sound();
    }
    public void HowToNext(){
        howOne.SetActive(false);
        howTwo.SetActive(true);
        Sound();
    }
    public void Sound(){
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }

}
