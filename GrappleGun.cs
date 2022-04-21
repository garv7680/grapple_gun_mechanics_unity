using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    private LineRenderer ln;
    private Vector3 GrapplePoint;
    public LayerMask layerMask;
    public Transform gunTip,cam,player;
    private SpringJoint springjoint;

    public int maxDistance = 200;

    // Start is called before the first frame update
    void Start()
    {
        ln = GetComponent<LineRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        drawRope();
    }
    void StartGrapple()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.position, cam.forward,out hitInfo))
        {
            GrapplePoint = hitInfo.point;

            springjoint = player.gameObject.AddComponent<SpringJoint>();
            springjoint.autoConfigureConnectedAnchor = false;
            springjoint.connectedAnchor = GrapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, GrapplePoint);

            // max and min movement
            springjoint.maxDistance = distanceFromPoint * .17f;
            springjoint.minDistance = distanceFromPoint * .2f;

            //Set different spring values
            springjoint.spring = 6f;
            springjoint.damper = 2f;
            springjoint.massScale = 200f;

            //2 positions (start and end)
            ln.positionCount = 2;
        }
    }
    void drawRope()
    {
        if (!springjoint) return;
        
        ln.SetPosition(0, gunTip.position);
        ln.SetPosition(1, GrapplePoint);
    }
    void StopGrapple()
    {
        //0 positions
        ln.positionCount = 0;
        Destroy(springjoint);
        
    }
}
