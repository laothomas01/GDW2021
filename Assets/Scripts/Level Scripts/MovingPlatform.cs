using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *How to use this script:
 *  the moving platform takes these objects:
 *      Player
 *        empty object 1 called point1
 *        empty object 2 called point2
 *       
 *     set how many points you want to move between
 */
public class MovingPlatform : MonoBehaviour, ITriggerable
{
    public GameObject player;
    public bool onPlatform;
    private int index = 0;
    private float wait;
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private float speed;
    [SerializeField] private float distanceOffset;
    [SerializeField] private float initWait;
    [SerializeField] private bool on;

    private void Start()
    {
        wait = initWait;

    }

    void FixedUpdate()
    {
        if (on) Move();
    }

    private void Move()
    {

        if (points.Count > 0)
        {

            //this platform will 
            transform.position = Vector3.MoveTowards(transform.position, points[index].transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, points[index].transform.position) < distanceOffset)
            {
                if (wait <= 0)
                {
                    index = (index + 1) % points.Count;
                    wait = initWait;
                }
                else
                {
                    wait -= Time.deltaTime;
                }
            }
        }
    }

    public void Trigger()
    {
        on = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = this.transform;
            //onPlatform = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //onPlatform = false;
            player.transform.parent = null;
        }
    }
}


