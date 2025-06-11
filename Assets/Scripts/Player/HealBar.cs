using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarHealAnimator : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public PlayerHealth player;
   
    [Header("Animator que reproduce las animaciones de curación")]
    public Animator healAnimator;

    private float previousHealth;
    private Coroutine animCoroutine;


    [SerializeField] private CanvasGroup healGroup;
    [SerializeField] private CanvasGroup damageGroupToHide;

    void Start()
    {
        if (player != null)
        {
            previousHealth = player.health;
        }
    }

    int GetRange(float percent)
    {
        if (percent == 0.4f)
            return 1; // 20–40
        if (percent == 0.6f)
            return 2; // 40–60
        if (percent == 0.8f)
            return 3; // 60–80
        if (percent > 0.9f)
            return 4; // 60–80
        return 0;      // 80–Full
    }

    string GetAnimationName(int range)
    {
        switch (range)
        {
            case 0: return "";
            case 1: return "20-40";
            case 2: return "40-60";
            case 3: return "60-80";
            case 4: return "80-Full";
            default: return "";
        }
    }

    void PlayAnimation(string stateName)
    {
        healAnimator.Play(stateName, 0, 0f);
        healAnimator.Update(0);
        healAnimator.speed = 1f;

        if (animCoroutine != null)
            StopCoroutine(animCoroutine);
        animCoroutine = StartCoroutine(ResetAnimator(healAnimator));
    }

    void Update()
    {
        if (player == null || healAnimator == null)
            return;

        float current = player.health;
        float percent = current / player.maxHealth;

        // Calcular en qué rango estaba antes y en cuál está ahora
        int previousRange = GetRange(previousHealth / player.maxHealth);
        int currentRange = GetRange(percent);

        // Solo si subiste de un rango a otro, reproducimos animación
        if (current > previousHealth && currentRange > previousRange)
        {
            string stateName = GetAnimationName(currentRange);
            Debug.Log("Vida subió de rango " + previousRange + " a " + currentRange + " → " + stateName);

            if (damageGroupToHide != null)
                damageGroupToHide.alpha = 0f;

            if (healGroup != null)
                healGroup.alpha = 1f;
                        
            PlayAnimation(stateName);

            //if (player != null)
                //player.ForceUpdateDamageHUD();
        }

        previousHealth = current;
    }

    IEnumerator ResetAnimator(Animator anim)
    {
        yield return null;
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);
        anim.speed = 0f;
    }
}