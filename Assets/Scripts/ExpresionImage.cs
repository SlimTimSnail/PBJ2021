using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), ExecuteInEditMode]
public class ExpresionImage : MonoBehaviour
{
    [System.Serializable]
    public class Face
    {
        public Sprite Sprite => m_sprite;
        public int MaxThreshold => m_maxThreshold;

        [SerializeField]
        private Sprite m_sprite;
        [SerializeField]
        private int m_maxThreshold;
    }


    [SerializeField]
    private Face[] m_sprites;
    [SerializeField]
    private int m_expression = 0;

    private void Start()
    {
        GameController.Instance.SentenceManager.PointsEvent += OnPointsChanged;
        OnPointsChanged(0);
    }

    void OnPointsChanged(int points)
    {
        for (int i = 0; i < m_sprites.Length; ++i)
        {
            if (points <= m_sprites[i].MaxThreshold)
            {
                SetExpression(i);
                return;
            }
        }
        SetExpression(m_sprites.Length - 1);
    }

    public void SetExpression(int i)
    {
        m_expression = Mathf.Clamp(i, 0, m_sprites.Length - 1);
        GetComponent<Image>().sprite = m_sprites[m_expression].Sprite;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (m_sprites != null && m_sprites.Length > 0)
            SetExpression(m_expression);
    }
#endif
}
