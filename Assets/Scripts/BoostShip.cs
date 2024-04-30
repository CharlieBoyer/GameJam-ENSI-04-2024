using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostShip : MonoBehaviour
{
   public float BoostValue;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Ship"))
      {
         Vector3 boostValue = other.gameObject.transform.GetComponent<Rigidbody>().velocity * BoostValue;
         other.gameObject.transform.GetComponent<Rigidbody>().velocity = boostValue;
      }
   }
}
