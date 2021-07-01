using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chaser : MonoBehaviour
{   
    

    // Start is called before the first frame update
    
    // private GameObject Virus;
    
    private Transform p;
    
     
     
    void Start()
    {
        p = GameObject.Find("Sarbi").GetComponent<Transform>();

    }
    
    // Update is called once per frame
    void Update()
    {
         
        Vector3 extra;
        extra.x = p.position.x;
        extra.y = p.position.y+2;
        extra.z = p.position.z - 3;
        transform.position = Vector3.MoveTowards(transform.position, extra, 2);
        
    }
}
