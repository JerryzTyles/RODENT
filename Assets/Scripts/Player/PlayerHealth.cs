using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    bool isInmune;
    public float InmunityTime;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;

    [SerializeField] private Animator healthAnimator;

    [SerializeField] private CanvasGroup damageGroup;
    [SerializeField] private CanvasGroup healGroupToHide;


    //muerte
    public GameObject menuMuerte;
    public bool GameOver = false;



    private float previousHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        previousHealth = health;

        material = GetComponent<Blink>();
        sprite = GetComponent<SpriteRenderer>();

        // Asegura que al empezar, el HUD de curación esté oculto
        if (healGroupToHide != null)
            healGroupToHide.alpha = 0f;

        // Asegura que el HUD de daño inicie visible y sincronizado
        if (damageGroup != null)
            damageGroup.alpha = 1f;

        ForceUpdateDamageHUD(); // Sincroniza visualmente al iniciar
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            UpdateHealthBar();
            StartCoroutine(Inmuniy());

            if (collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            if (health <= 0)
            {
                print("player dead");
                MUERTO();
            }
        }
    }


    public void MUERTO()
    {
        menuMuerte.SetActive(true);
        Time.timeScale = 0;
        GameOver = true;
    }


    IEnumerator Inmuniy()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(InmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }

    void UpdateHealthBar()
    {
        int roundedHealth = Mathf.RoundToInt(health);

        // 🧱 Verificamos si la vida actual está en un valor que debe mostrar animación
        if (roundedHealth != 0 && roundedHealth != 20 && roundedHealth != 40 &&
            roundedHealth != 60 && roundedHealth != 80 && roundedHealth != 100)
        {
            return; // 🚫 No se reproduce ninguna animación ni se muestra el HUD
        }

        // ✅ Oculta HUD de curación (si estaba activo)
        if (healGroupToHide != null)
            healGroupToHide.alpha = 0f;

        // ✅ Muestra HUD de daño solo si se va a animar
        if (damageGroup != null)
            damageGroup.alpha = 1f;

        string stateName = "";

        switch (roundedHealth)
        {
            case 0:
                stateName = "Dead";
                break;
            case 20:
                stateName = "20";
                break;
            case 40:
                stateName = "40";
                break;
            case 60:
                stateName = "60";
                break;
            case 80:
                stateName = "80";
                break;
            case 100:
                stateName = "full";
                break;
        }

        if (healthAnimator != null)
        {
            healthAnimator.Play(stateName, 0, 0f);
            healthAnimator.Update(0);
            healthAnimator.speed = 1f;
            StartCoroutine(ResetAnimator(healthAnimator));
        }

        previousHealth = health;
    }


    IEnumerator ResetAnimator(Animator anim)
    {
        yield return null;
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);
        anim.speed = 0f;
    }

    // NUEVO MÉTODO: permite al otro script sincronizar la animación actual
    public void ForceUpdateDamageHUD()
    {
        int roundedHealth = Mathf.RoundToInt(health);
        string stateName = "";

        switch (roundedHealth)
        {
            case 0:
                stateName = "Dead";
                break;
            case 20:
                stateName = "20";
                break;
            case 40:
                stateName = "40";
                break;
            case 60:
                stateName = "60";
                break;
            case 80:
                stateName = "80";
                break;
            case 100:
                stateName = "Full";
                break;
            default:
                return; // No hay animación para este valor
        }

        if (healthAnimator != null)
        {
            healthAnimator.Play(stateName, 0, 0f);
            healthAnimator.Update(0);
            healthAnimator.speed = 0f;
        }
    }
}
