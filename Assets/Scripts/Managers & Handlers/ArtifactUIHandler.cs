using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactUIHandler : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject artifactMenu;
    [SerializeField] private GameObject closeMenuTrigger;

    [Header("Artifact")]
    [SerializeField] private ArtifactProgress progress;
    [SerializeField] private SerializedDictionary<Artifacts, GameObject> artifactIcons = new SerializedDictionary<Artifacts, GameObject>();
    
    [Header("Tool Tip")]
    [SerializeField] private GameObject artifactToolTip;
    [SerializeField] private TextMeshPro artifactName;
    [SerializeField] private TextMeshPro artifactDescription;

    private void Start()
    {
        artifactMenu.SetActive(false);
        artifactToolTip.SetActive(false);
        closeMenuTrigger.SetActive(false);
    }
    private void Update()
    {
        if (artifactToolTip.activeInHierarchy)
        {
            artifactToolTip.transform.position = Input.mousePosition;
        }
    }

    public void ShowArtifactMenu(GameObject slot)
    {
        artifactMenu.SetActive(true);
        artifactMenu.transform.position = slot.transform.position;
        closeMenuTrigger.SetActive(true);
        closeMenuTrigger.transform.position = slot.transform.position;

        SerializedDictionary<Artifacts, bool> pair = progress.UnlockedArtifacts;

        // Gray out artifacts that you don't have yet
        // to be replaced with switching icon etc.
        foreach (var kvp in artifactIcons)
        {
            Color currentColor = kvp.Value.GetComponent<Image>().color;
            if (pair[kvp.Key])
            {
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
                kvp.Value.GetComponent<Image>().color = newColor;
            }
            else
            {
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
                kvp.Value.GetComponent<Image>().color = newColor;
            }
        }
    }
    public void HideArtifactMenu()
    {
        artifactMenu.SetActive(false);
    }
    public void ShowArtifactToolTip()
    {
        artifactToolTip.SetActive(true);
        SetToolTipTexts();
    }
    public void HideArtifactToolTip()
    {
        artifactToolTip.SetActive(false);
    }

    private void SetToolTipTexts()
    {

    }
}
