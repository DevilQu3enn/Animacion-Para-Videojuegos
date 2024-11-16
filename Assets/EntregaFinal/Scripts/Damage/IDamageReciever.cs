using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReciever : IFactionMember
{
    void RecieveDamage(IDamageSender perpetrator, DamagePayload payload);
}
