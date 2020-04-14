using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            inventory.coins += 1;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
