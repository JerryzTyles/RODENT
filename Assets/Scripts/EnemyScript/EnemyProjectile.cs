using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject projectile;

    public float timeToShoot;
    public float shootCooldown;

    public bool freqShooter;
    public bool watcher;

    private Animator animator;

    void Start()
    {
        shootCooldown = timeToShoot;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (freqShooter)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (shootCooldown < 0)
        {
            Shoot();
        }

        if (watcher)
        {
            transform.GetComponent<PlayerDetect>(); // <- esta l�nea no hace nada, puedes eliminarla si no usas PlayerDetect aqu�
        }
    }

    public void Shoot()
    {
        // Instanciar proyectil
        GameObject bomba = Instantiate(projectile, transform.position, Quaternion.identity);

        // Aplicar fuerza seg�n la direcci�n
        if (transform.localScale.x < 0)
        {
            bomba.GetComponent<Rigidbody2D>().AddForce(new Vector2(7000f, 0f), ForceMode2D.Force);
        }
        else
        {
            bomba.GetComponent<Rigidbody2D>().AddForce(new Vector2(-7000f, 0f), ForceMode2D.Force);
        }

        // Reproducir animaci�n de disparo
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }

        shootCooldown = timeToShoot;
    }
}