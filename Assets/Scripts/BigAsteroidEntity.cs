using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BigAsteroidEntity : MonoBehaviour
{
   public GameObject Mothership;
   public int AsteroidSpeed;
   public int BaseDamage;
   private float _damage;
   private bool getImpactedByShip;
   private float _shipVelocity;

   private void Update() {
       if(getImpactedByShip)ImpulseIntoMothership();
   }

   private void OnCollisionEnter(Collision other) {
       if (other.gameObject.CompareTag("Ship")) {
           _shipVelocity = other.gameObject.GetComponent<Rigidbody>().velocity.z;
           _damage = BaseDamage * _shipVelocity;
           getImpactedByShip = true;
       }
       if(other.gameObject.CompareTag("Mothership"))other.gameObject.GetComponent<MotherShipEntity>().TakeDamage((int)_damage);
   }

   public void ImpulseIntoMothership() {
       var transformPosition = transform.position;
       if (Mothership.transform.position.x < transformPosition.x)
           transformPosition.x += Time.deltaTime * AsteroidSpeed;
       if (Mothership.transform.position.x > transformPosition.x)
           transformPosition.x -= Time.deltaTime * AsteroidSpeed;
       
       if (Mothership.transform.position.y < transformPosition.y)
           transformPosition.y += Time.deltaTime * AsteroidSpeed;
       if (Mothership.transform.position.y > transformPosition.y)
           transformPosition.y -= Time.deltaTime * AsteroidSpeed;
       
       if (Mothership.transform.position.z < transformPosition.z)
           transformPosition.z += Time.deltaTime * AsteroidSpeed;
       if (Mothership.transform.position.z > transformPosition.z)
           transformPosition.z -= Time.deltaTime * AsteroidSpeed;
   }
}
