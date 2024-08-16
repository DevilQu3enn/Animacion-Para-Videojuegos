using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Animations;

public class AimController : MonoBehaviour
{
    [SerializeField] private AimConstraint chestAim;
    [SerializeField] private Transform aimRig;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Animator anim;

    private bool aiming;
    public void Aim(InputAction.CallbackContext ctx)
    {
        bool val = ctx.ReadValueAsButton();
        aiming = val;

        //Activar o desactivar el constrait de apuntado del pecho
        chestAim.enabled = val;

        //Desencadenar la activacion de la camara de apuntado
        aimRig.gameObject.SetActive(val);

        anim.SetBool("Aim", val);
    }
    private void Awake()
    {
        aimRig.gameObject.SetActive(false);
    }
    private void Update()
    {
       if (aiming) transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(camTransform.forward, transform.up).normalized);
    }
}
