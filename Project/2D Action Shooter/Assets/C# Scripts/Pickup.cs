using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public weapon weaponToEquip;

    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            collision.GetComponent<player_movment>().ChangeWeapon(weaponToEquip);
            Destroy(gameObject);
        }
    }

}
