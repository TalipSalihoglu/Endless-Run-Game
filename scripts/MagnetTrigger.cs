using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private Transform p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Sarbi").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    { 

        if(Player.isMagnet==true)
        {
            StartCoroutine(A());
        }

        
    }
    IEnumerator A() 
    {
        if (Vector3.Distance(transform.position, p.position) < 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, p.position, 2);
        }
        yield return new WaitForSeconds(10f);
        Player.isMagnet = false;
    }

    
}
