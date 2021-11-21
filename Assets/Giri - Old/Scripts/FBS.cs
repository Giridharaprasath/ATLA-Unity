using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

// Fire Bending Script
public class FBS : MonoBehaviour
{
    [SerializeField]
    private GameObject torchLight;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool isTorch;

    private void Awake()
    {
        IM.Controls.FireBending.TorchLight.started += ctx => SetTorchLight();
        torchLight.SetActive(false);
        isTorch = false;
    }

    private void SetTorchLight()
    {
        if (isTorch == false) 
        {
            isTorch = true;
        }
        else if (isTorch == true) 
        {
            isTorch = false;
        }
    }

    private void Update()
    {
        if (isTorch == true)
        {
            torchLight.SetActive(true);
            animator.SetBool("IsTorchOn", true);
        }
        else if (isTorch == false)
        {
            torchLight.SetActive(false);
            animator.SetBool("IsTorchOn", false);
        }
    }
}
