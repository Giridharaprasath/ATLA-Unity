using System.Collections;
using UnityEngine;
using Mirror;

public class EarthBending : NetworkBehaviour
{
    [Header("Player")]
    [SerializeField]
    private Animator animator;

    [Header("Ground Vision")]
    [SerializeField]
    private GameObject visionCam;
    [SerializeField]
    private Material groundMat;
    private Vector3 playerT;
    private float innerR = -2f;
    private float outerR = 0;
    [SerializeField]
    private bool isSensing;
    private bool isGrounded;

    [Header("Aim Target")]
    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    private GameObject target;

    [Header("Wall Open")]
    [SerializeField]
    private GameObject wallParent;
    [SerializeField]
    private GameObject wallTrigger;
    private bool wantToOpen;
    [SyncVar]
    public bool isOpeningWall;
    
    public override void OnStartAuthority()
    {
        visionCam.SetActive(false);
        groundMat.SetFloat("_outerR", outerR);
        groundMat.SetFloat("_innerR", innerR);
        groundMat.SetVector("_startPos", playerT);

        target = GameObject.Find("Target");
        aimTarget.position = transform.position + transform.forward * 10 + transform.up * 2.5f;

        InputManager.Controls.EarthBending.SeismicSense.performed += ctx => PerformedSeismicSense();

        InputManager.Controls.EarthBending.OpenWall.performed += ctx => CmdSetIsOpeningWall(true);
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
        if (isGrounded && wantToOpen)
        {
            Debug.Log("WANT TO OPEN THIS WALL: " + wallParent.name);
            float time = 1f;

            Vector3 lookAt = wallParent.transform.position;
            lookAt.y = transform.position.y;
            StartCoroutine(LookAtSmoothly(transform, lookAt, time));
            InputManager.StartedOpenWall();
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            playerT = transform.position;
            isGrounded = GetComponent<AangPlayerMovement>().isGrounded;
            if (isSensing) aimTarget.position = target.transform.position;
        }
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
        if (other.gameObject.tag == "WallOpenTrigger")
        {
            wallTrigger = other.gameObject;
            wallParent = other.transform.parent.gameObject;
            wantToOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wallParent = null;
        wallTrigger = null;
        wantToOpen = false;
    }

    IEnumerator LookAtSmoothly(Transform objectToMove, Vector3 worldPosition, float duration)
    {
        Quaternion currentRot = objectToMove.rotation;
        Quaternion newRot = Quaternion.LookRotation(worldPosition -
            objectToMove.position, objectToMove.TransformDirection(Vector3.up));

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToMove.rotation =
                Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        animator.SetBool("ToOpenWall", true);
    }

    public void FinishedOpenWall()
    {
        InputManager.FinishedOpenWall();
        animator.SetBool("ToOpenWall", false);
        isOpeningWall = false;
    }

    public void PerformedOpenSingleWall()
    {
        if (isGrounded && wantToOpen)
        {
            //CmdSetIsOpeningWall(true);
            float time = 1f;

            Vector3 lookAt = wallParent.transform.position;
            lookAt.y = transform.position.y;
            StartCoroutine(LookAtSmoothly(transform, lookAt, time));
            InputManager.StartedOpenWall();
        }
    }

    [Command]
    private void CmdSetIsOpeningWall(bool val)
    {
        isOpeningWall = val;
        //wallParent.GetComponent<DoorManager>().SetDoorState();
        PerformedOpenSingleWall();
    }
}