using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    private ParticleSystem pS;

    private void Awake()
    {
        pS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (pS && !pS.IsAlive())
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
