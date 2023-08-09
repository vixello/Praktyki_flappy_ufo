using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.gameObject.GetComponent<Obstacles>().obstacleCount++;
        transform.parent.gameObject.GetComponent<Obstacles>().PointCount.text = transform.parent.gameObject.GetComponent<Obstacles>().obstacleCount.ToString();
    }
}
