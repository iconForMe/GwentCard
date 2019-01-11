using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryOnload : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
}
