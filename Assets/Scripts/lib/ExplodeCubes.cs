using Unity.VisualScripting;
using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    // the castle blocks
    [SerializeField] private Rigidbody2D[] cubes;
    [SerializeField] private float force = 10f;
    // point they fly away from
    [SerializeField] private Transform explosionCenter;
    [SerializeField] private AudioSource sound;

    public void Explode()
    {
        sound.Play();

        Vector2 center = explosionCenter != null
            ? (Vector2)explosionCenter.position
            : (Vector2)transform.position;

        foreach (Rigidbody2D cube in cubes)
        {
            if (cube == null) continue;

            // direction from the center to this cube = outward
            Vector2 dir = ((Vector2)cube.transform.position - center).normalized;

            // if a cube is exactly at center, give it a random shove so it still moves
            if (dir == Vector2.zero) dir = Random.insideUnitCircle.normalized;

            cube.AddForce(dir * force, ForceMode2D.Impulse);
            cube.AddTorque(Random.Range(-force, force)); // spin for flair
        }
    }
}
