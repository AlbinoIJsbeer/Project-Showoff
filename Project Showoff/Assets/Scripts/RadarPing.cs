/* 
    ------------------- Code Monkey -------------------
    
    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

// Taken from CodeMonkey, YouTube link: "Awesome Radar Effect in Unity!" - https://www.youtube.com/watch?v=J0gmrgpx6gk&t=241s
// Project files taken from https://unitycodemonkey.com/video.php?v=J0gmrgpx6gk

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    private float disappearTimer;
    private float disappearTimerMax;
    private Color color;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        disappearTimerMax = 1.5f;
        disappearTimer = 0f;
        color = new Color(0, 1, 0, 1f);
    }

    private void Update() 
    {
        disappearTimer += Time.deltaTime;

        color.a = Mathf.Lerp(disappearTimerMax, 0f, disappearTimer / disappearTimerMax);
        spriteRenderer.color = color;

        if (disappearTimer >= disappearTimerMax) 
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color) 
    {
        this.color = color;
    }

    public void SetDisappearTimer(float disappearTimerMax) 
    {
        this.disappearTimerMax = disappearTimerMax;
        disappearTimer = 0f;
    }

}
