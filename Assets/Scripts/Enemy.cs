using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;
    public int moneyReward = 50;

    [Header("Effects")]
    public GameObject deathEffect;

    private bool alive;

    private void Start()
    {
        speed = startSpeed;
        alive = true;
    }

    private void Die()
    {
        if (alive)
        {
            alive = false;
            
            GameManager.Instance.addMoneyToPlayer(moneyReward);

            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
    }
}
