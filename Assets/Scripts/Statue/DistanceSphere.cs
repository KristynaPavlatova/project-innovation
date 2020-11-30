using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//for using Events

public class DistanceSphere : MonoBehaviour
{
    public static event Action PlayerTriggerEnter;
    public static event Action PlayerTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerTriggerEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerTriggerExit();
        }
    }
}
