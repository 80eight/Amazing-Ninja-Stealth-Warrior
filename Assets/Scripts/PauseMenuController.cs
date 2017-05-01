using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void QuitClick()
    {
        Application.Quit();
    }
}
