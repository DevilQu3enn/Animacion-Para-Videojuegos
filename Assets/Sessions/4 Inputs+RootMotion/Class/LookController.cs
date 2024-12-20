using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class LookController : MonoBehaviour
{
    [SerializeField] private VectorDampener lookVector;
    [SerializeField] private Transform lookRig;
    [SerializeField] private float sensitivity;
    [SerializeField] private Vector2 verticalRotationLimits;

    private float rotationy;
    public void Look(InputAction.CallbackContext ctx)
    {
        lookVector.TargetValue = ctx.ReadValue<Vector2>() / new Vector2(Screen.width, Screen.height);

    }
    private void Update()
    {
        lookVector.Update();
        lookRig.RotateAround(lookRig.position, transform.up, lookVector.CurrentValue.x * sensitivity *360f);
        rotationy -= lookVector.CurrentValue.y * sensitivity * 360f;
        rotationy = Mathf.Clamp(rotationy, verticalRotationLimits.x, verticalRotationLimits.y);
        Vector3 euler = lookRig.localEulerAngles;
        lookRig.localEulerAngles = new Vector3(rotationy, euler.y, euler.z);

    }
}
