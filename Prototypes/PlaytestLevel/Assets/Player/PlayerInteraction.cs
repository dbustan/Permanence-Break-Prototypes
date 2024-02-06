using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;
    public LayerMask interactionLayerMask;
    public Text interactionInfoText;
    public Transform heldTargetTransform;
    public float heldObjectTrackSpeed;

    private Camera playerCamera;

    private GameObject heldObject;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grabButtonPressed = Input.GetMouseButtonDown(0);
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.red);
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactionLayerMask)) {
            GameObject obj = hit.collider.gameObject;
            Interactible objInterData = obj.GetComponent<Interactible>();
            if(objInterData) {
                interactionInfoText.rectTransform.anchoredPosition = getInteractibleCenterPos(objInterData);
                interactionInfoText.text = objInterData.interactionInfoText;

                if(grabButtonPressed) {
                    grabButtonPressed = false;
                    if(heldObject) {
                        heldObject.GetComponent<Interactible>().drop();
                        heldObject = null;
                    } else {
                        heldObject = obj;
                        heldObject.GetComponent<Interactible>().grab();
                    }
                }

            } else {
                interactionInfoText.text = "";
            }
        } else {
            interactionInfoText.text = "";
        }
        if(grabButtonPressed) {
            if(heldObject) {
                heldObject.GetComponent<Interactible>().drop();
                heldObject = null;
            }
        }
        if(heldObject) {
            Vector3 distApart = heldTargetTransform.position - heldObject.transform.position;
            if(distApart.sqrMagnitude > Mathf.Pow(0.01f, 2)) {
                heldObject.GetComponent<Rigidbody>().velocity = distApart * heldObjectTrackSpeed;
            } else {
                heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    private Vector2 getInteractibleCenterPos(Interactible interactible) {
        Vector3Int screenSize = new Vector3Int(playerCamera.pixelWidth, playerCamera.pixelHeight);
        return playerCamera.WorldToScreenPoint(interactible.getInteractionTextPosition()) - screenSize / 2;
    }
}
