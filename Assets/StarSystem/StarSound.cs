using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSound : MonoBehaviour
{
    [SerializeField] AudioSource ad;

    public void Sound(){
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }
}
