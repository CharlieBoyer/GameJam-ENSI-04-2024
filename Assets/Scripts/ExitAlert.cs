using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAlert : MonoBehaviour
{
   public GameObject MyUiMessage;
   private void OnTriggerExit(Collider other)
   {
      MyUiMessage.SetActive(true);
   }

   private void OnTriggerEnter(Collider other)
   {
      MyUiMessage.SetActive(false);
   }
}
