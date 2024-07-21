using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_CreditScene : MonoBehaviour
{
    public String URL;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Application.OpenURL(URL);
        }
    }
}
