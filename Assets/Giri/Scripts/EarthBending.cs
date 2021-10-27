using System.Collections;
using UnityEngine;

public class EarthBending : MonoBehaviour
{
    [Header("Ground Vision")]
    [SerializeField]
    private GameObject visionCam;
    [SerializeField]
    private Material groundMat;
    [SerializeField]
    private Vector3 playerT;
    [SerializeField]
    private float innerR = -2f;
    [SerializeField]
    private float outerR = 0;
    [SerializeField]
    private bool isSensing;
    [SerializeField]
    private bool isGrounded;

    [Header("Aim Taget")]
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private GameObject target;

    [Header("Wall Destroy")]
    [SerializeField]
    private GameObject wallParent;
    [SerializeField]
    private GameObject wallTrigger;
    [SerializeField]
    private bool wantToDestroy;
    
    private void Awake()
    {
        visionCam.SetActive(false);
        groundMat.SetFloat("_outerR", outerR);
        groundMat.SetFloat("_innerR", innerR);
        groundMat.SetVector("_startPos", playerT);

        target = GameObject.Find("Target");
        aimTarget.position = transform.position + transform.forward * 10 + transform.up * 2.5f;

        InputManager.Controls.EarthBending.SeismicSense.performed += ctx => PerformedSeismicSense();

        InputManager.Controls.EarthBending.DestroyWall.performed += ctx => PerformedDestroyWall();
    }

    private void PerformedSeismicSense()
    {
        if (!isSensing && isGrounded)
        {
            visionCam.SetActive(true);
            StartCoroutine(SetSeismicSense());
            StartCoroutine(SetTarget());
        }
    }

    private void PerformedDestroyWall()
    {
        if (isGrounded && wantToDestroy)
        {
            Debug.Log("WANT TO DESTROY THIS WALL: " + wallParent.name);
        }
    }

    private void Update()
    {
        playerT = transform.position;
        isGrounded = GetComponentInParent<AangPlayerMovement>().isGrounded;
        if (isSensing) aimTarget.position = target.transform.position;
    }

    private IEnumerator SetSeismicSense()
    {
        isSensing = true;
        Debug.Log("SEISMIC SENSE PERFORMED");
        groundMat.SetVector("_startPos", playerT);
        while (outerR < 40)
        {
            yield return new WaitForSeconds(0.01f);
            outerR++;
            groundMat.SetFloat("_outerR", outerR);
            innerR++;
            groundMat.SetFloat("_innerR", innerR);
        }
        yield return new WaitForSeconds(0.1f);
        outerR = 0;
        innerR = -2;
        groundMat.SetFloat("_outerR", outerR);
        groundMat.SetFloat("_innerR", innerR);
        visionCam.SetActive(false);
        isSensing = false;
    }

    private IEnumerator SetTarget()
    {
        yield return new WaitForSeconds(2f);
        aimTarget.position = transform.position + transform.forward * 10 + transform.up * 2.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WallDestroyTrigger")
        {
            wallTrigger = other.gameObject;
            wallParent = other.transform.parent.gameObject;
            wantToDestroy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wallParent = null;
        wallTrigger = null;
        wantToDestroy = false;
    }
}