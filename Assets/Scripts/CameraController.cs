using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject Player;
    public Color backgroundSneaky;
    Vector3 offset;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Mode") && PlayerPrefs.GetInt("Mode") == 1)
        {
            Camera.main.backgroundColor = backgroundSneaky;
        }
        Player = GameObject.Find("Player");
        offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Player.transform.position + offset;
	}
}
