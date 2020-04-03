using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwicher : MonoBehaviour
{
    [SerializeField] GameObject material = null;
    [SerializeField] bool skinnedMeshRenderer;
    [SerializeField] Material baseMaterial = null;
    [SerializeField] Material changeMaterial = null;
    [SerializeField] bool materialState = false;

    public void MaterialChange(bool materialState)
    {
        if (material != null)
        {
            if (materialState)
            {
                if(skinnedMeshRenderer)
                {
                    material.GetComponent<SkinnedMeshRenderer>().material = changeMaterial;
                }
                else
                {
                    material.GetComponent<MeshRenderer>().material = changeMaterial;
                }
                this.materialState = true;
            }
            else
            {
                if (skinnedMeshRenderer)
                {
                    material.GetComponent<SkinnedMeshRenderer>().material = baseMaterial;
                }
                else
                {
                    material.GetComponent<MeshRenderer>().material = baseMaterial;
                }
                this.materialState = false;
            }
        }

    }
}
