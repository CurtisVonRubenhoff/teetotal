using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMover : MonoBehaviour
{

    [SerializeField]
    private float startXPos = -13f;
    [SerializeField]
    private float targetXPos = 5.58f;
    [SerializeField]
    private float movementSpeed = 10f;

    private bool canUse = false;

    [SerializeField]
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(GM.gameRunning) {
            var currentPos = transform.position;

            if (canUse) {
                if (Input.GetMouseButton(0)) {
                    transform.position = new Vector3(startXPos, currentPos.y, currentPos.z);
                    canUse = false;
                }
            }

            if (currentPos.x < targetXPos) {
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            } else {
                GM.YouLose();
            }
        }
    }

    private void OnMouseEnter() {
        if (!GM.amAnnoyed) {
            canUse = true;
        }
    }

    private void OnMouseExit() {
        canUse = false;
    }
}
