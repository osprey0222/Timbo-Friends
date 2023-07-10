using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    public Action OnHitVWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("VirtualWall"))
        {
            if (OnHitVWall != null)
            {
                OnHitVWall();
                collision.gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Cookie"))
        {
            ++GameData.Singleton.CookieCount;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Toy")) 
        {
            GameData.Singleton.IsDead = true;
        }
    }

}
