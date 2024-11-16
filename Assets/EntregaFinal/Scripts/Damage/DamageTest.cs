using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DamageTest : MonoBehaviour, IDamageSender
{
    [SerializeField] private float damage;
    [SerializeField] private DamagePayload.Severity _severity;
    public int Faction => 1;

    public void SendDamage(IDamageReciever target)
    {
        target.RecieveDamage(this, new DamagePayload { damage = 10f, position = transform.position, severity = _severity });
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out IDamageReciever reciever)){
            SendDamage(reciever);
        }
    }
}
