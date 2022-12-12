using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    public GameObject mainDriver;
    Manager script;
    // Start is called before the first frame update
    public void set(int param){
        
        script= mainDriver.GetComponent<Manager>();
        script.button=param;
        
    }
}
