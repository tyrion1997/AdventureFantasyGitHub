using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Min and max values")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, this.transform.position.z);
        this.transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), this.transform.position.z);

    }


}
