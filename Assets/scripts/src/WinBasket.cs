using System;
using UnityEngine;

public class WinBasket : MonoBehaviour
{
    [SerializeField]
    // The Game Object
    public Game game = null;

    // Signal saying game is over and you completed the level
    public static event Action<bool> OnWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Debug.Log("YOU WIN!@");
        }
    }
}
