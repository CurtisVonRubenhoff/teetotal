using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiuController : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;
    [SerializeField]
    private Animator myAnim;
    [SerializeField]
    private AudioSource mySound;
    public bool amOn = false;
    private bool canUse;

    void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if (canUse) {
            if (Input.GetMouseButtonDown(0)) {
               GM.GoodByeMiu();
            }
        }

        myAnim.SetBool("enter", amOn);
        mySound.enabled = amOn;
    }

    private void OnMouseEnter() {
        canUse = true;
    }

    private void OnMouseExit() {
        canUse = false;
    }
}
