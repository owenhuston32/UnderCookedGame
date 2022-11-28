using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour, IHighlight
{
    private Renderer[] renderers;
    private Material[] normalMaterials;
    [SerializeField] private Material highlightedMaterial;
    [SerializeField] private GameObject[] highlightObjects;


    private void Start()
    {
        renderers = new Renderer[highlightObjects.Length];
        normalMaterials = new Material[highlightObjects.Length];
        for(int i = 0; i < highlightObjects.Length; i++)
        {
            renderers[i] = highlightObjects[i].GetComponent<Renderer>();
            normalMaterials[i] = renderers[i].material;
        }

    }

    public void HighlightMaterial()
    {
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = highlightedMaterial;
        }

    }

    public void RemoveHighlight()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
             renderers[i].material = normalMaterials[i];
        }
    }

}
