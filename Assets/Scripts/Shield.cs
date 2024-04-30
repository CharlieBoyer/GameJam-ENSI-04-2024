using UnityEngine;
using UnityEngine.UI;

public class Shield: MonoBehaviour
{
    public int InitialPower = 100;
    public int Power { get; set; }

    [Header("Interface")]
    public Slider ShieldGauge;
    
    void Start()
    {
        Power = InitialPower;
        ShieldGauge.maxValue = InitialPower;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SmallAsteroid"))
        {
            Power -= 20;

            if (Power <= 0) {
                gameObject.SetActive(false);
                ShieldGauge.value = Power;
            }
        }
    }
}
