using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ship")) {
            Destroy(other);
        }
    }
}
