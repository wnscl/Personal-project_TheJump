using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] Transform camPivot;
    public Transform CamPivot { get => camPivot; }

    [SerializeField] Transform realCam;
    [SerializeField] GameObject playerBody;

    private Vector3 lookForward;
    public Vector3 LookForward {get {return lookForward;}}
    private Vector3 lookRight;
    public Vector3 LookRight { get { return lookRight; } }

    public void PlayerLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = camPivot.rotation.eulerAngles;
        float camAngleX = camAngle.x - (mouseDelta.y * 2f);

        if (camAngleX < 180f)
        {
            camAngleX = Mathf.Clamp(camAngleX, -15f, 45f);

        }
        else
        {
            camAngleX = Mathf.Clamp(camAngleX, 335f, 361f);
        }


        camPivot.rotation = Quaternion.Euler(camAngleX, camAngle.y + (mouseDelta.x * 2f), camAngle.z);
        float angle = camPivot.eulerAngles.y;
        //eulerAngles °øºÎ
        playerBody.transform.rotation = Quaternion.Euler(0, angle, 0);

        lookForward = new Vector3(camPivot.forward.x, 0f, camPivot.forward.z).normalized;
        lookRight = new Vector3(camPivot.right.x, 0f, camPivot.right.z).normalized;


    }



}
