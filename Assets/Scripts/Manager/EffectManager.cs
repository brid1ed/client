using System.Collections;
using Manager.DesignPattern;
using UnityEngine;
using UnityEngine.Rendering;

public class EffectManager : Singleton<EffectManager> {
    public Volume glitch;

    public void StartGlitch(float endTime) {
        StartCoroutine(glitchEnd(endTime));
    }

    IEnumerator glitchEnd(float endTime) {
        glitch.weight = 1;

        yield return new WaitForSeconds(endTime);

        glitch.weight = 0;
    }
}