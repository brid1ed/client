using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Manager.DesignPattern;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    public static CamManager main;
    public CinemachineVirtualCamera cam;
    public CinemachineBasicMultiChannelPerlin noise;
    float orSize_d;
    float dutch_d;

    IEnumerator routine = null;

    private void Start() {
        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        main = this;

        orSize_d = cam.m_Lens.OrthographicSize;
        dutch_d = cam.m_Lens.Dutch;
    }

    void ClearRoutine() {
        if (routine != null) {
            StopCoroutine(routine);

            routine = null;
        }
    }

    public void CloseUp(float orSize, float dutch, float dur = 0) {
        ClearRoutine();
        routine = _closeUp(orSize, dutch, dur);

        StartCoroutine(routine);
    }
    public void CloseOut(float dur = 0) {
        ClearRoutine();
        routine = _closeOut(dur);

        StartCoroutine(routine);
    }

    public void Shake(float strength = 1, float dur = 0.05f)
    {
        StartCoroutine(_shake(strength, dur));
    }

    IEnumerator _closeUp(float orSize, float dutch, float dur) {
        if (dur > 0) {
            float dSize = cam.m_Lens.OrthographicSize, dDutch = cam.m_Lens.Dutch;

            for (int i = 1; i <= 10; i++) {
                cam.m_Lens.OrthographicSize = dSize - (dSize - orSize) / 10 * i;
                cam.m_Lens.Dutch = dDutch - (dDutch - dutch) / 10 * i;

                yield return new WaitForSeconds(dur / 10);
            }
        }

        cam.m_Lens.OrthographicSize = orSize;
        cam.m_Lens.Dutch = dutch;

        routine = null;
    }

    IEnumerator _closeOut(float dur) {
        if (dur > 0) {
            float dSize = cam.m_Lens.OrthographicSize, dDutch = cam.m_Lens.Dutch;

            for (int i = 1; i <= 10; i++) {
                cam.m_Lens.OrthographicSize = dSize + (orSize_d - dSize) / 10 * i;
                cam.m_Lens.Dutch = dDutch + (dutch_d - dDutch) / 10 * i;

                yield return new WaitForSeconds(dur / 10);
            }
        }
        
        cam.m_Lens.OrthographicSize = orSize_d;
        cam.m_Lens.Dutch = dutch_d;

        routine = null;
    }

    IEnumerator _shake(float strength, float dur)
    {
        noise.m_AmplitudeGain = strength;

        yield return new WaitForSeconds(dur);

        noise.m_AmplitudeGain = 0;
    }
}
