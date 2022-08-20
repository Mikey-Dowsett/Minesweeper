using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSettings : MonoBehaviour
{
    private static GridSettings instance;
    public static GridSettings Instance
    {
        get { return instance; }
    }

    public Vector2 size;
    public float bombPercentage;
    public string maths = "+";

    [SerializeField] Image[] grid, difficulty, type;
    [SerializeField] Sprite[] boxes;
    [SerializeField] AudioSource ad;

    private int currentGrid, currentDif, currentType;

    void Start(){
        DontDestroyOnLoad(gameObject);

        if(instance == null){
            instance = this;
        } else if(this != instance) {
            GameObject.Destroy(instance.gameObject);
            instance = this;
        }
    }
    
    public void SetSize(string name){
        var temp = name.Split('x');
        float.TryParse(temp[0], out size.x);
        float.TryParse(temp[1], out size.y);
        Sound();
    }

    public void SetPercente(float count){
        bombPercentage = count;
        Sound();
    }

    public void SetMaths(string math){
        maths = math;
        Sound();
    }

    public void SelectedGrid(int num){
        grid[currentGrid].sprite = boxes[0];
        currentGrid = num;
        grid[currentGrid].sprite = boxes[1];
        Sound();
    }
    public void SelectedDif(int num){
        difficulty[currentDif].sprite = boxes[0];
        currentDif = num;
        difficulty[currentDif].sprite = boxes[1];
        Sound();
    }
    public void SelectedType(int num){
        type[currentType].sprite = boxes[0];
        currentType = num;
        type[currentType].sprite = boxes[1];
        Sound();
    }
    public void Sound(){
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }
}
