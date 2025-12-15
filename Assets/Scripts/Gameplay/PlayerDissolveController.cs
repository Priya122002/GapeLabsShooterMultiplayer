using UnityEngine;
using System.Collections;

public class PlayerDissolveController : MonoBehaviour
{
    [Header("Body Renderers (Dissolve)")]
    public Renderer[] bodyRenderers;   // ONLY body mesh

    [Header("Disable During Death")]
    public GameObject[] disableObjects; // eyes, gun, hands, UI

    public float burstDuration = 0.25f;

    static readonly int DissolveID = Shader.PropertyToID("_Dissolve");

    public void PlayDissolveOutBurst()
    {
        StopAllCoroutines();
        SetDisableObjects(false); // 🔥 hide eyes / gun / UI
        StartCoroutine(Dissolve(0f, 1f));
    }

    public void PlayDissolveInBurst()
    {
        StopAllCoroutines();
        StartCoroutine(Dissolve(1f, 0f, true));
    }

    IEnumerator Dissolve(float from, float to, bool enableAfter = false)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / burstDuration;
            float v = Mathf.Lerp(from, to, t);

            foreach (var r in bodyRenderers)
            {
                if (!r) continue;

                foreach (var m in r.materials)
                    m.SetFloat(DissolveID, v);
            }

            yield return null;
        }

        if (enableAfter)
            SetDisableObjects(true); // 🔥 show eyes / gun / UI
    }

    void SetDisableObjects(bool state)
    {
        foreach (var obj in disableObjects)
        {
            if (obj)
                obj.SetActive(state);
        }
    }
}
