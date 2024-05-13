using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cursor : MonoBehaviour
{
    public float speed = 10;
    private Collider2D _targetCollider = null;

    private void Update() {
        //you move the cursor with wasd
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;

        //you press with spacebar
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(_targetCollider == null) return;

            IInteractable interactable = _targetCollider.GetComponent<IInteractable>();

            if (interactable != null) {
                //you call the OnClick function
                interactable.OnClick();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //you check if the object has the IInteractable interface
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null) {
            //you call the OnHoverEnter function
            interactable.OnHoverEnter();
            _targetCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //you check if the object has the IInteractable interface
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null) {
            //you call the OnHoverExit function
            interactable.OnHoverExit();
            _targetCollider = null;
        }
    }
}
