using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] List<Material> materials = new List<Material>();


    MeshRenderer meshRenderer;
    int activeMaterialIndex = 0;



    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeItemColor()
    {
        activeMaterialIndex++;
        if (activeMaterialIndex >= materials.Count)
        {
            activeMaterialIndex = 0;
        }
        meshRenderer.material = materials[activeMaterialIndex];
    }
}
