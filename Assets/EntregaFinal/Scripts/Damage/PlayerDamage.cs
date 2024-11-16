using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour, IDamageSender
{
    public float damage;
    public DamagePayload.Severity _severity;
    public int Faction => 1;

    public void SetValues(float damage, DamagePayload.Severity _severity)
    {
        this.damage = damage;
        this._severity = _severity;
    }

    public void SendDamage(IDamageReciever target)
    {
        target.RecieveDamage(this, new DamagePayload { damage = damage, position = transform.position, severity = _severity });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageReciever reciever))
        {
            SendDamage(reciever);
        }
    }
}
