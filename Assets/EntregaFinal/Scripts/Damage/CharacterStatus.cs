using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour, IDamageReciever
{
    private Animator anim;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;

    private float health;
    private float stamina;
    [SerializeField] private float staminaRegenerationTime;

    public float Health => health;
    public float Stamina => stamina;

    public Action<float> OnHealthChanged;
    public Action<float> OnStaminaChanged;

    private void Awake()
    {
        health = maxHealth;
        stamina = maxStamina;

        anim = GetComponent<Animator>();
    }

    public int Faction => 1;

    public void RecieveDamage(IDamageSender perpetrator, DamagePayload payload)
    {
        UpdateHealth(payload.damage);
        Vector3 damageDirection = transform.InverseTransformPoint(payload.position).normalized;
        if (Mathf.Abs(damageDirection.x) >= Mathf.Abs(damageDirection.z))
        {
            anim.SetFloat("damageX", Mathf.Ceil(damageDirection.x * (float)payload.severity));
            anim.SetFloat("damageY", 0);
        }
        else
        {
            anim.SetFloat("damageX", 0);
            anim.SetFloat("damageY", Mathf.Ceil(damageDirection.z * (float)payload.severity));
        }
        Debug.DrawLine(transform.position, payload.position, Color.blue, 1.0f);
        anim.SetTrigger("damaged");
    }



    public bool UpdateHealth(float healthDelta)
    {
        if (health >= healthDelta)
        {
            health -= healthDelta;
            OnHealthChanged?.Invoke(health);
            return true;
        }
        //Morir
        health = 0;
        OnHealthChanged?.Invoke(health);
        anim.SetTrigger("die");
        return false;
    }
    public bool UpdateStamina(float staminaDelta)
    {
        if (stamina >= staminaDelta)
        {
            stamina -= staminaDelta;
            OnStaminaChanged?.Invoke(stamina);
            return true;
        }
        stamina = 0;
        OnStaminaChanged?.Invoke(stamina);
        return false;
    }

    private void Update() {
        RegenerateStamina();
    }

    private void RegenerateStamina(){
        if(stamina < maxStamina){
            stamina += Time.deltaTime * staminaRegenerationTime;
            OnStaminaChanged?.Invoke(stamina);
        }
    }

}
