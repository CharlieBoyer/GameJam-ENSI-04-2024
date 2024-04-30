using UnityEngine;

public class ShipGameplay : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallAsteroid"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("TargetAsteroid"))
        {
            // "Pause" Player and watch Big Asteroid
        }
    }
}
