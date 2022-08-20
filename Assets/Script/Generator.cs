using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Generator : MonoBehaviour
{
    [SerializeField] Vector2 size;
    [SerializeField] float bombCount;
    [SerializeField] GameObject block;
    [SerializeField] Transform cam;
    [SerializeField] GameObject doneScreen;
    [SerializeField] TMP_Text hitCounter;
    [SerializeField] TMP_Text percentage;

    List<GameObject> blocks = new List<GameObject>();

    public bool started;
    public bool lost;
    public float tilesLeft;
    public float bombsHit;
    float bombPercentage = 0.1f;
    private float maxBombs;

    void Awake()
    {
        if(FindObjectOfType<GridSettings>()){
            size = FindObjectOfType<GridSettings>().size;
            bombPercentage = FindObjectOfType<GridSettings>().bombPercentage;
        }
        for(int x = 0; x < size.x; x++){
            for(int y = 0; y < size.y; y++){
                var temp = Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
                temp.transform.SetParent(transform);
                blocks.Add(temp);
            }
        }
        bombCount = (int)((size.x * size.x)*bombPercentage);
        maxBombs = bombCount;
        tilesLeft = size.x * size.y;
        cam.position = new Vector3((int)size.x/2, (int)size.y/2, -10);
        cam.GetComponent<Camera>().orthographicSize = (size.x+0.1f)/2;
    }

    void Update(){
        if(tilesLeft <= 0){
            FindObjectOfType<CamearController>().ResetCam();
            StartCoroutine("ShowBombs");
        }
    }

    public void Bombs(Vector3 place){
        started = true;
        while(bombCount > 0){
            int rand = Random.Range(0, blocks.Count);
            if(Vector2.Distance(blocks[rand].transform.position, place) > 2 && !blocks[rand].GetComponent<Block>().isBomb) {
                blocks[rand].GetComponent<Block>().IsBomb();
                bombCount--;
                tilesLeft--;
            }
        }
    }

    private IEnumerator ShowBombs(){
        string state = lost ? "Defeat" : "Victory";
        string stars = "None";
        float percent = (int)(((maxBombs - bombsHit)/maxBombs)*100);
        if(percent >= 80 && !lost){
            stars = "Three";
        } else if(percent >= 50 && !lost){
            stars = "Two";
        } else if(percent >= 0 && !lost) {
            stars = "One";
        }
        
        foreach(Block blok in FindObjectsOfType<Block>()){
            if(blok.isBomb){
                blok.BeBomb();
                yield return new WaitForSeconds(0.25f);
            }
        }
        yield return new WaitForSeconds(1f);
        doneScreen.SetActive(true);
        FindObjectOfType<MenuManager>().ShowStars(stars, state);
    }

    public void Replay(){
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        SceneManager.LoadScene(0);
    }
}
