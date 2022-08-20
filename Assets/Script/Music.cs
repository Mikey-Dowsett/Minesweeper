using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private static Music instance;
    public static Music Instance
    {
        get { return instance; }
    }
    void Start(){
        DontDestroyOnLoad(gameObject);

        if(instance == null){
            instance = this;
        } else {
            GameObject.Destroy(gameObject);
        }
    }
}
