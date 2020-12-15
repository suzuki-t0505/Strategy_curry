using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField]
    private float sunSpeed;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.State == GameManager.GameState.Play)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(Time.deltaTime * sunSpeed, 0, 0);
        }
    }
}
