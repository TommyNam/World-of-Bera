using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    // char damaged and dmg value
    public static UnityAction<GameObject, int> characterDamaged;

    // char healed and heal value
    public static UnityAction<GameObject, int> characterHealed;

}