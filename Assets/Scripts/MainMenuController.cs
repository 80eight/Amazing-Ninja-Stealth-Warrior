using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayButton()
    {
        Application.LoadLevel("Scene1");
        PlayerPrefs.SetInt("Mode", 0);
    }

    public void PlaySneakyButton()
    {
        Application.LoadLevel("Scene1");
        PlayerPrefs.SetInt("Mode", 1);
    }

    public void PlayInfinteNinjas()
    {
        Application.LoadLevel("Scene2");
        PlayerPrefs.SetInt("Mode", 2);
    }
}
