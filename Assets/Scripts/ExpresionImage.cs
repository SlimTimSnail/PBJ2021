using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), ExecuteInEditMode]
public class ExpresionImage : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_sprites;
    [SerializeField]
    private int m_expression = 0;



    public void SetExpression(int i)
    {
        m_expression = Mathf.Clamp(i, 0, m_sprites.Length - 1);
        GetComponent<Image>().sprite = m_sprites[m_expression];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (m_sprites != null && m_sprites.Length > 0)
            SetExpression(m_expression);
    }
#endif
}
