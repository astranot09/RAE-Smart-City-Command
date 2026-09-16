using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private List<GameObject> openPanels = new List<GameObject>();
    private bool canOpenPanel = true;

    public void OpenPanel(GameObject panel)
    {
        if (panel == null || !canOpenPanel) return;

        if (!openPanels.Contains(panel))
        {
            openPanels.Add(panel);
        }

        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        if (panel == null) return;

        panel.SetActive(false);
        openPanels.Remove(panel);
    }

    /// <summary>
    /// Closes the top-most (most recently opened) panel. Ideal for Escape / Back key navigation.
    /// </summary>
    public void CloseTopPanel()
    {
        if (openPanels.Count == 0) return;

        int lastIndex = openPanels.Count - 1;
        GameObject topPanel = openPanels[lastIndex];

        topPanel.SetActive(false);
        openPanels.RemoveAt(lastIndex);
    }


    public void ChangeAllPanel(GameObject newPanel)
    {
        CloseAllPanels();

        if (newPanel != null)
        {
            OpenPanel(newPanel);
        }
    }

    public void CloseAllPanels()
    {
        for (int i = 0; i < openPanels.Count; i++)
        {
            if (openPanels[i] != null)
            {
                openPanels[i].SetActive(false);
            }
        }

        openPanels.Clear();
    }

    public void CanOpenPanel(bool x)
    {
        canOpenPanel = x;
    }
}
