using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public int startHealth = 5;
    private int health;

    void Start()
    {
        health = startHealth;
    }

    public void takeDamage(int damage) {
        health -= damage;
        Debug.Log(gameObject.name + " taking " + damage + " damage. " + health + " hp left.");
        if (health <= 0)
            die();
    }

    public void die() {
        gameObject.SetActive(false);
    }
}
