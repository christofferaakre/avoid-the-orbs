using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthUp : Powerup
{
    public int healthAmount;
    public bool overheal = true;

    public override void OnInteract()
    {
        base.OnInteract();
        Player.instance.Heal(healthAmount, overheal: overheal);
        VoiceLineManager.instance.PlayClip(VoiceLineManager.instance.GetRandomClip(VoiceLineManager.instance.healthUpClips));
        FloatingTextManager.instance.ShowFloatingText("+" + healthAmount.ToString() + "HP",
                                                      fontSize: 16,
                                                      color: Color.green,
                                                      position: transform.position + Vector3.up * 0.5f,
                                                      motion: Vector3.up * 5,
                                                      duration: 1.5f
            );

        Deactivate();
    }

}
