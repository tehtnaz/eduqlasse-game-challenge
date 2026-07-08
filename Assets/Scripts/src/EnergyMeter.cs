using TMPro;
using UnityEngine;

public class EnergyMeter : MonoBehaviour
{
    [SerializeField] TextMeshPro inGameText;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Showing energy meter text");
            inGameText.text = (0.5f * collision.rigidbody.mass * collision.rigidbody.linearVelocity.sqrMagnitude).ToString() + " J";
        }
    }
}
