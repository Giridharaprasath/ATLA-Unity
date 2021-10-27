using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Earth Bending Script
public class EBS : MonoBehaviour
{
    [SerializeField]
    private GameObject VisionCam;
    [SerializeField]
    private Material GSM;
    [SerializeField]
    private Vector3 playerT;
    [SerializeField]
    private Vector3 matT;
    [SerializeField]
    private float innerR;
    [SerializeField]
    private float outerR;
    [SerializeField]
    private bool isSensing;
    [SerializeField]
    private bool isGrounded;

    private void Awake()
    {
        VisionCam.SetActive(false);
        GSM.SetFloat("Outer_Radius", outerR);
        GSM.SetFloat("Inner_Radius", innerR);
        GSM.SetVector("Start_Position", matT);
        IM.Controls.EarthBending.SeismicSense.performed += ctx => PerformedSeismicSense();
    }

    private void PerformedSeismicSense()
    {
        if (!isSensing && isGrounded)
        {
            VisionCam.SetActive(true);
            StartCoroutine(SetSeismicSense());   
        }
    }

    private void Update()
    {
        playerT = transform.position;
        matT = playerT;
        isGrounded = GetComponentInParent<DPM>().isGrounded;
    }

    private IEnumerator SetSeismicSense()
    {
        isSensing = true;
        Debug.Log("SEISMIC SENSE PERFORMED");
        GSM.SetVector("Start_Position", matT);
        while (outerR < 40)
        {
            yield return new WaitForSeconds(0.01f);
            outerR += 1;
            GSM.SetFloat("Outer_Radius", outerR);
            innerR += 1;
            GSM.SetFloat("Inner_Radius", innerR);
        }
        yield return new WaitForSeconds(0.1f);
        outerR = 0;
        innerR = -2f;
        VisionCam.SetActive(false);
        isSensing = false;
    }
}
