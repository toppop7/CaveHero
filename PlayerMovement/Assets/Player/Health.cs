﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float currHealth = 0.0f;
    public float maxHealth = 100.0f;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        StartCoroutine(Regenerate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage (float damage) {
        if (currHealth > 0)
        {
            currHealth -= damage;

            healthBar.SetHealth(currHealth);
        }
        if (currHealth <= 0)
        {
            this.gameObject.SetActive(false);
            Invoke("Death", 3f);
        }

    }

    public void Death()
    {
        SceneManager.LoadScene("DemoGame");
    }

    public IEnumerator Regenerate()
    {
        while (true)
        {
            if (currHealth < 100)
            {
                currHealth += 1;
                healthBar.SetHealth(currHealth);
                yield return new WaitForSeconds(1);
            } 
            else
            {
                yield return null;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        bool isEnemy = collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Spider");
        if (isEnemy)
        {
            Damage(0.5f);
        }
    }
}