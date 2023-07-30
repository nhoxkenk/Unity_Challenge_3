using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawn;
    private int index = 0;  
    public Vector3 spawnPosition = new Vector3(30, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObstacle()
    {
        index = Random.Range(0, spawn.Length);
        if (!playerController.gameOver)
        {
            if(index == 2)
            {
                Instantiate(spawn[index], new Vector3(30, 1.5f, 0), spawn[index].transform.rotation);
            }else
            Instantiate(spawn[index], spawnPosition, spawn[index].transform.rotation);
            
        }
        
    }
}
