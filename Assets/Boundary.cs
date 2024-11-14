using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && collision.gameObject.activeInHierarchy)
        {
            GameManager.Instance.LostInSpace();
        }
    }
}
