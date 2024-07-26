using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Vector3 motionDebug;

    private int velXId;
    private int velYId;

#if UNITY_EDITOR

    private void OnValidate()
    {
        Move(motionDebug);
    }

    private void Awake()
    {
        velXId = Animator.StringToHash("VelX");
        velYId = Animator.StringToHash("VelY");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector3 motionDirection)
    {
        anim.SetFloat(velXId, motionDirection.x);
        anim.SetFloat(velYId, motionDirection.y);
    }
}