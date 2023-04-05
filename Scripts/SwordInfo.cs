using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordInfo : MonoBehaviour
{

    public int damage =3;
    public BoxCollider col;



    private void Awake()
    {
        
       col = GetComponent<BoxCollider>();
    }

    public void isAttacking()
    {
        col.enabled = true;
    }
    public void isNotAttacking()
    {
        col.enabled = false;
    }

    //this onTrigger is not used for now
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DungeonSkeleton_demo")
        {
            Debug.Log("Hurray");
        }
    }
}
