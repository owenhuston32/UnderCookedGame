using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour, IHighlight
{
    private Renderer[] renderers;
    private Material[][] normalMaterials;
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private GameObject[] highlightObjects;


    private void Start()
    {
        normalMaterials = new Material[highlightObjects.Length][];
        renderers = new Renderer[highlightObjects.Length];
        for(int i = 0; i < highlightObjects.Length; i++)
        {
            renderers[i] = highlightObjects[i].GetComponent<Renderer>();

            normalMaterials[i] = renderers[i].materials;
        }
    }



    public void HighlightMaterial()
    {
        for (int i = 0; i < renderers.Length; i++)
        {

            renderers[i].materials = createHighlightMaterialsArray(renderers[i].materials);
        }
    }

    private Material[] createHighlightMaterialsArray(Material[] mats)
    {
        Material[] newMats = new Material[mats.Length];
        for (int j = 0; j < mats.Length; j++)
        {
            newMats[j] = highlightedMaterial;
        }
        return newMats;
    }


    public void RemoveHighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].materials = normalMaterials[i];
        }

    }

}
