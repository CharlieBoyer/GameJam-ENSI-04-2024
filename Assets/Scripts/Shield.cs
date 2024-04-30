using UnityEngine;
using UnityEngine.UI;

public class Shield: MonoBehaviour
{
    [Header("Interface")]
    public Slider ShieldGauge;

    [Header("Values")]
    public int InitialPower = 100;
    public int Power = 100;

    private MeshRenderer _renderer;
    private static readonly int PowerProperty = Shader.PropertyToID("_Power");

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        Power = InitialPower;
        ShieldGauge.maxValue = InitialPower;
        ShieldGauge.value = Power;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bang");
        if (other.gameObject.CompareTag("SmallAsteroid"))
        {
            Power -= 20;
            ShieldGauge.value = Power;
            _renderer.material.SetFloat(PowerProperty, (float)Power/InitialPower);

            if (Power <= 0) {
                gameObject.SetActive(false);
            }
        }
    }
}
