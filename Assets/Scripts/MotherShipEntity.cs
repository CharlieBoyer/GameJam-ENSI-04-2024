using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipEntity : MonoBehaviour
{
   public GameObject MotherShipFillBar;
   public int BaseHPqtt;
   private int _currentHPqtt;

   private void Start()
   {
      _currentHPqtt = BaseHPqtt;
   }

   private void Update()
   {
      Vector3 transformLocalScale = MotherShipFillBar.transform.localScale;
      transformLocalScale.x = (_currentHPqtt*100/BaseHPqtt)/100;
      MotherShipFillBar.transform.localScale = transformLocalScale;
   }

   public void TakeDamage(int value)
   {
      _currentHPqtt -= BaseHPqtt;
   }
}
