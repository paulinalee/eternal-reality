using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    bool start = false;
    public GameObject enemy;
    Vector3 spawn_location;
    public float spawnrate = 2.0f;
    private float nextspawn = 0.0f;
    private float timer = 0.0f;
    public float width = 75;
    public float height = 30;
    public float xwidth = 70;
    SpawnController spawner;

    // Start is called before the first frame update
    void Start()
    {
        //start = false;
        //leftbound = transform
        float xpos = Random.Range(-width / 2, width / 2);
        float zpos = Random.Range(0, xwidth/2);
        transform.position = new Vector3(xpos, height / 2, zpos);
        spawner = transform.parent.GetComponent<SpawnController>();
    }

    private void OnEnable()
    {
        Debug.Log("START");
        start = true;
    }

    private void OnDisable()
    {
        start = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (start) {
            timer += Time.deltaTime;
            //if timer and current enemies under wave
            if (timer > nextspawn && spawner.enemiesperwave > spawner.getCurrentEnemies()) {
                nextspawn = timer + spawnrate;
                //Debug.Log("spawning at " + transform.position);
                Instantiate(enemy, transform.position, Quaternion.identity, transform.parent.Find("Enemies"));
                spawner.addEnemies();
            }
        }
    }
}
