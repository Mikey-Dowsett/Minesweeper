using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Sprite doneSprite;
    [SerializeField] SpriteRenderer overlay;
    [SerializeField] Sprite exlamation;
    [SerializeField] Sprite bomb;
    [SerializeField] Sprite[] number;

    [SerializeField] LayerMask bombLayer;
    [SerializeField] LayerMask normalLayer;
    [SerializeField] ParticleSystem bombPart;
    [SerializeField] AudioSource ad;
    [SerializeField] AudioClip bombSound;

    private SpriteRenderer sr;
    private Collider2D[] bombs;
    private Generator gen;

    public bool selected;
    public bool isBomb;
    public float count;

    private bool friends;
    private bool hasBombed;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gen = FindObjectOfType<Generator>();
        ad = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bombs = Physics2D.OverlapCircleAll(transform.position, 1f, bombLayer);
        if(selected && !isBomb) overlay.sprite = number[bombs.Length];

        // if(isBomb && gen.tilesLeft == 0){
        //     overlay.sprite = bomb;
        //     if(bombPart && !bombPart.isPlaying) bombPart.Play();
        // }
    }

    void OnMouseOver(){
        if(!FindObjectOfType<Math>().ui.activeSelf && !selected){
            if(!selected && sr) sr.color = new Color(0.9f, 0.9f, 0.9f);
            if(Input.GetMouseButtonDown(0) && overlay.sprite != exlamation){
                if(isBomb){
                    FindObjectOfType<Math>().Equation(this);
                } else {
                    Selection();
                }
                
            }

            if(Input.GetMouseButtonDown(1)){
                if(overlay.sprite == exlamation){
                    overlay.sprite = null;
                } else {
                    overlay.sprite = exlamation;
                }
            }
        }
    }

    void OnMouseExit(){
        if(!selected) sr.color = new Color(1, 1, 1);
    }

    public void Selection(){
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
        sr.sprite = doneSprite;
        sr.color = new Color(1f, 1f, 1f);
        selected = true;
        if(gen.started == false){
            gen.Bombs(transform.position);
        }
        if(isBomb){
            BeBomb();
        }
        bombs = Physics2D.OverlapCircleAll(transform.position, 1f, bombLayer);
        if(bombs.Length == 0 && !friends){
            friends = true;
            StartCoroutine("Surronding");
        }
        if(!isBomb){ gen.tilesLeft--;}
    }

    private IEnumerator Surronding(){
        foreach(Collider2D coll in Physics2D.OverlapCircleAll(transform.position, 1f, normalLayer)){
            Block b = coll.GetComponent<Block>();
            if(!b.isBomb && !b.selected){
                b.Selection();
            }
            yield return new WaitForSeconds(0.04f);
        }
    }

    public void BeBomb(){
        if(!hasBombed){
            ad.clip = bombSound;
            ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
            if(!ad.isPlaying) ad.Play();
            overlay.sprite = bomb;
            if(bombPart && !bombPart.isPlaying) bombPart.Play();
            hasBombed = true;
        }
    }

    public void IsBomb(){
        isBomb = true;
        gameObject.layer = 6;
    }
}
