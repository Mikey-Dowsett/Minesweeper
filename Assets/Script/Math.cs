using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Math : MonoBehaviour
{
    public GameObject ui;
    [SerializeField] TMP_Text equation;
    [SerializeField] TMP_Text answerText;
    [SerializeField] Image timer;

    private Block block;
    private bool countDown;
    private string ans = "";
    private float answer;
    public float time = 5;

    private int maxNum;

    [SerializeField] string oper;
    [SerializeField] GridSettings sets;

    private void Awake(){
        if(FindObjectOfType<GridSettings>()) {sets = FindObjectOfType<GridSettings>(); oper = sets.maths;}

        if(sets.bombPercentage == 0.05f){
            if(sets.maths == "*"){
                maxNum = 10;
            } else maxNum = 20;
        } else if(sets.bombPercentage == 0.1f){
            if(sets.maths == "*"){
                maxNum = 12;
            } else maxNum = 50;
        } else if(sets.bombPercentage == 0.15f){
            if(sets.maths == "*"){
                maxNum = 15;
            } else maxNum = 100;
        } else if(sets.bombPercentage == 0.2f){
            if(sets.maths == "*"){
                maxNum = 20;
            } else maxNum = 1000;
        }
    }

    public void Equation(Block _block){
        ui.SetActive(true);
        ans = "";
        MakeEquation();

        block = _block;

        answerText.text = "";
        time = 5;
        countDown = true;
    }

    private void Update(){
        if(countDown){
            time -= Time.deltaTime;
            timer.fillAmount = time/5;
            
            int num;
            if(Input.anyKeyDown && int.TryParse(Input.inputString, out num)) ans += Input.inputString;
            if(Input.GetKeyDown(KeyCode.Backspace)){
                ans = answerText.text.ToString();
                ans = ans.Remove(ans.Length - 1);
            }
            answerText.text = ans.ToString();

            if(time <= 0){
                FindObjectOfType<Generator>().lost = true;
                countDown = false;
                ui.SetActive(false);
                FindObjectOfType<Generator>().tilesLeft = 0;
            }

            if(ans == answer.ToString()){
                FindObjectOfType<Generator>().bombsHit++;
                block.Selection();
                countDown = false;
                answerText.text = "";
                ans = null;
                ui.SetActive(false);
            }
        }
    }

    private void MakeEquation(){
        int one = Random.Range(1, maxNum);
        int two = Random.Range(1, maxNum);
        if(oper == "-" && two > one){
            MakeEquation();
        } else if(oper == "/" && (two > one || one % two != 0)){
            MakeEquation();
        } else {
            equation.text = one.ToString() + " " + oper + " " + two.ToString();
            switch(oper){
                case "+" : answer = one + two; break;
                case "-" : answer = one - two; break;
                case "*" : answer = one * two; break;
                case "/" : answer = one / two; break;
            }
        }
    }
}
