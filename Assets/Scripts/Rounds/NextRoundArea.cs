using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoundArea : MonoBehaviour
{
    public static bool readyToReturn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null && readyToReturn)
        {
            Rounds.instance.CurrentPhase = Phase.Combat;
            readyToReturn = false;
        }
    }
}
