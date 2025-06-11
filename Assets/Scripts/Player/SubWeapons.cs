using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapons : MonoBehaviour
{

    public GameObject Fireball;
    public float spawnDelay;

    private float lastDirection = 1f;

    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelay = 0.25f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }



    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
            lastDirection = moveInput;

        if (Input.GetButtonDown("Fire1") && !isSpawning)
            StartCoroutine(SpawnSubweaponWithDelay());
    }

    IEnumerator SpawnSubweaponWithDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        // Determina dirección basada en la escala del jugador (1 = derecha, -1 = izquierda)
        float direction = lastDirection > 0 ? 1f : -1f;

        // Instancia el proyectil
        GameObject subItem = Instantiate(Fireball, transform.position, Quaternion.identity);

        // Aplica fuerza en la dirección correcta
        subItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(6000f * direction, 0f), ForceMode2D.Force);

        // Voltea el sprite del proyectil visualmente (si es necesario)
        Vector3 newScale = subItem.transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * direction; // asegura que mire hacia la dirección
        subItem.transform.localScale = newScale;

        isSpawning = false;
        AudioManager.instance.PlayAudio(AudioManager.instance.shot);

        //Debug.Log("Player direction: " + direction);
    }
}

    
