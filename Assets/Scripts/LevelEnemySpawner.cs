using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnemySpawner : MonoBehaviour {

    public Transform[] SpawnPositions;
    [Space]
    public GameObject Enemy;
    public GameObject Enemylvl2;
    public GameObject enemylvl3;
    [Space]
    public GameObject[] enemies;

    private GameObject Clone;
    private GameObject Player;

    private PlayerController pc;

    private Text WaveText;

    private System.Random ran = new System.Random();

    private int Enemies;
    private int wave;
    private int AmountSpawning;
    
    // Use this for initialization
    void Start()
    {
        WaveText = GameObject.Find("WaveText").GetComponent<Text>();
        Player = GameObject.Find("Player");
        pc = Player.GetComponent<PlayerController>();
        wave = 0;
        int a = ran.Next(0, SpawnPositions.Length + 1);
        Clone = Instantiate(Enemy, SpawnPositions[a].position, new Quaternion(0, 0, 0, 0));
    }


    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }
        else
        {
            Enemies = 0;
        }
        if (Enemies == 0)
        {
            wave += 1;
            pc.health += 20;
            
            for(int i = 0; i <= AmountSpawning; i++)
            {
                int index = ran.Next(0, SpawnPositions.Length + 1);
                int enemyType = ran.Next(1, 4);
                if (enemyType == 1)
                {
                    Instantiate(Enemy, SpawnPositions[index].position, new Quaternion(0, 0, 0, 0));
                }
                else if (enemyType == 2)
                {
                    Instantiate(Enemylvl2, SpawnPositions[index].position, new Quaternion(0, 0, 0, 0));
                }
                else if (enemyType == 3)
                {
                    Instantiate(enemylvl3, SpawnPositions[index].position, new Quaternion(0, 0, 0, 0));
                }
            }
        }
    }

    void LateUpdate()
    {
        AmountSpawning = wave * 2;
        WaveText.text = "Wave: " + wave;
    }
}
