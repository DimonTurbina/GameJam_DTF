using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class AutoTransparent : MonoBehaviour
{
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;
    private float m_Transparency = 0.3f;
    private const float m_TargetTransparancy = 0.3f;
    private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec
    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_OldShader = m_Renderer.material.shader;
        m_OldColor = m_Renderer.material.color;
        var shader = Shader.Find("Transparent/Diffuse");
        for (int i = 0; i < m_Renderer.materials.Length; i++)
        {
            m_Renderer.materials[i].shader = shader;
        }
         
    }

    public void BeTransparent()
    {
        m_Transparency = m_TargetTransparancy;
    }
    private void Update()
    {
        if (m_Transparency < 1.0f)
        {
            Color C = m_Renderer.material.color;
            C.a = m_Transparency;
            //m_Renderer.material.color = C;
            for (int i = 0; i < m_Renderer.materials.Length; i++)
            {
                m_Renderer.materials[i].color = C;
            }
        }
        else
        {
            m_Renderer.material.shader = m_OldShader;
            m_Renderer.material.color = m_OldColor;
            for (int i = 0; i < m_Renderer.materials.Length; i++)
            {
                m_Renderer.materials[i].shader = m_OldShader;
                m_Renderer.materials[i].color = m_OldColor;
            }
            // And remove this script
            Destroy(this);
        }
        m_Transparency += ((1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
    }

}