using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    Animator cameraShake;

    public Joystick shootJoystick;

    [HideInInspector]
    public bool canShoot = true;

    public void Start()
    {
        cameraShake = Camera.main.GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (shootJoystick.InputDir != Vector3.zero)
        {
            angle = Mathf.Atan2(shootJoystick.InputDir.y, shootJoystick.InputDir.x) * Mathf.Rad2Deg;
        }
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                
                shotTime = Time.time + timeBetweenShots;
            }
        }

    }

}
