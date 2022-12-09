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

            Material[] originalMats = new Material[renderers[i].materials.Length];

            for(int j = 0; j < renderers[i].materials.Length; j++)
            {
                originalMats[j] = renderers[i].materials[j];
            }
            normalMaterials[i] = originalMats;
        }

    }

    public void HighlightMaterial()
    {
        if(renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Material[] newMats = new Material[renderers[i].materials.Length];
                for (int j = 0; j < renderers[i].materials.Length; j++)
                {
                    newMats[j] = highlightedMaterial;
                }

                renderers[i].materials = newMats;
            }
        }

    }

    public void RemoveHighlight()
    {
        if(renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].materials = normalMaterials[i];
            }
        }
    }

}
