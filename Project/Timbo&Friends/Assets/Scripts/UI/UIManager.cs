using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    private static Dictionary<string, GameObject> m_UIDic = new Dictionary<string, GameObject>();
    private static Canvas m_Canvas;
    private static string m_UIPath = "UI/Prefab/";
    private static GameObject m_CurUI;

    public static void Init()
    {
        var cans = GameObject.FindObjectsOfType<Canvas>();
        foreach (var item in cans)
        {
            if (item.gameObject.CompareTag("MainCanvas"))
            {
                m_Canvas = item;
                break;
            }
        }

    }

    public static GameObject Show(string uiName)
    {
        try
        {
            GameObject uiObj = null;
            if (m_UIDic.ContainsKey(uiName))
            {
                m_UIDic[uiName].GetComponent<RectTransform>().SetAsLastSibling();
                uiObj = m_UIDic[uiName].gameObject;
                uiObj.SetActive(true);
            }
            else
            {
                uiObj = Resources.Load<GameObject>(m_UIPath + uiName).gameObject;

                uiObj = GameObject.Instantiate(uiObj) as GameObject;

                uiObj.transform.SetParent(m_Canvas.gameObject.transform); ;
                uiObj.transform.localPosition = Vector3.one;
                uiObj.transform.localScale = Vector3.one;
                uiObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                uiObj.SetActive(true);
                m_UIDic.Add(uiName, uiObj);
            }
            m_CurUI = uiObj;
            return uiObj;
        }
        catch (System.Exception)
        {
            Debug.LogWarning("UI is not exist: " + m_UIPath + uiName);
        }
        return null;
    }

    public static void HideCurUI()
    {
        if (m_CurUI != null)
        {
            m_CurUI.SetActive(false);
        }
    }

    public static void HideUI(string uiName)
    {
        m_UIDic[uiName].gameObject.SetActive(false);
    }

    public static void HideAllUI()
    {
        foreach (var item in m_UIDic)
        {
            item.Value.gameObject.SetActive(false);
        }
    }

    private static void ShowUI(string uiName)
    {
        m_UIDic[uiName].gameObject.SetActive(true);
    }

    public static void ClearUIs()
    {
        foreach (var item in m_UIDic)
        {
            GameObject.Destroy(item.Value);
        }
        m_UIDic.Clear();
    }
}
