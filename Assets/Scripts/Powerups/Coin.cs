using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public int coinValue = 1;

    public override void OnInteract()
    {
        base.OnInteract();
        GameManager.instance.GainCoins(coinValue);

        GameManager.instance.SetScore(GameManager.instance.score + coinValue * 10);
        VoiceLineManager.instance.PlayClip(VoiceLineManager.instance.GetRandomClip(VoiceLineManager.instance.coinClips));

        Deactivate();
    }
}
