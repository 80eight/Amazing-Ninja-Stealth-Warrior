using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Transform PlatformGenerator;
    public GameObject cube;
    public float distanceBetween;

    private float Height;
    private Vector3 spawningPosition;
    private GameObject cubeClone;
    private int Scale;
    private int x;

	// Use this for initialization
	void Start () {
        Height = cube.GetComponent<BoxCollider2D>().size.y;
        distanceBetween = 2.85f;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < PlatformGenerator.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Height + distanceBetween, transform.position.z);
            
            spawningPosition = new Vector3();
            cubeClone = Instantiate(cube, transform.position, new Quaternion(0, 0, 0, 0));
        }
	}
}
