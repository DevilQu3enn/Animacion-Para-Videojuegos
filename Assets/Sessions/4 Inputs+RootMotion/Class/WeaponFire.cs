using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        bool state = ctx.ReadValueAsButton();
        anim.SetBool("Firing", state);
    }
    public void Onshoot()
    {
        
    }
}
