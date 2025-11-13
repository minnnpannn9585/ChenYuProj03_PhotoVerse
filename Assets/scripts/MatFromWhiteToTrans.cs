using System.Collections;
using UnityEngine;

public class MatFromWhiteToTrans : MonoBehaviour
{
    // Fade duration in seconds (fixed to 1 second as requested)
    [SerializeField] float duration = 1f;

    IEnumerator Start()
    {
        var rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogWarning($"{nameof(MatFromWhiteToTrans)}: no Renderer found on the GameObject.");
            yield break;
        }

        // Accessing renderer.materials creates instances so shared materials aren't modified.
        var materials = rend.materials;

        // Try to ensure Standard-like shaders render with alpha (best-effort).
        foreach (var m in materials)
        {
            if (m == null) continue;
            if (m.HasProperty("_Mode"))
                m.SetFloat("_Mode", 3); // Standard: Transparent
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.DisableKeyword("_ALPHABLEND_ON");
            m.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / Mathf.Max(0.0001f, duration));
            float a = Mathf.Lerp(1f, 0f, t);
            SetAlphaForAll(materials, a);
            yield return null;
        }

        SetAlphaForAll(materials, 0f);
    }

    void SetAlphaForAll(Material[] materials, float alpha)
    {
        foreach (var mat in materials)
            SetMaterialAlpha(mat, alpha);
    }

    void SetMaterialAlpha(Material mat, float alpha)
    {
        if (mat == null) return;

        if (mat.HasProperty("_Color"))
        {
            Color c = mat.GetColor("_Color");
            c.a = alpha;
            mat.SetColor("_Color", c);
            return;
        }

        if (mat.HasProperty("_BaseColor"))
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = alpha;
            mat.SetColor("_BaseColor", c);
            return;
        }

        if (mat.HasProperty("_TintColor"))
        {
            Color c = mat.GetColor("_TintColor");
            c.a = alpha;
            mat.SetColor("_TintColor", c);
            return;
        }

        // If no known color property exists, do nothing (keeps code minimal as requested).
    }
}
