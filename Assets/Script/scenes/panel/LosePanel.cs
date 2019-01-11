using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour {

    // Use this for initialization
    public GameObject lose;
    public GameObject win;
	void Start () {
        lose = GameObject.Find("lose");
        lose.SetActive(false);
        win = GameObject.Find("win");
        win.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
