using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Tomar_Objeto : MonoBehaviour
{
    bool isPressed;
    bool isObjectNextYou;
    GameObject objTaken;
    GameObject padre;
    string aux;

    public bool hasStoredTransform;
    public Vector3 original_position;
    public Quaternion original_rotation;
    public Vector3 original_scale;

    private void Awake()
    {
        padre = GameObject.Find("Player");
    }

    void Start()
    {
        isPressed = false;
        hasStoredTransform = false;
        objTaken = null;
        aux = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isObjectNextYou)
            {
                isPressed = !isPressed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TakenObj")
        {
            if (aux != other.gameObject.name) hasStoredTransform = false;

            if (!hasStoredTransform)
            {
                aux = other.gameObject.name;

                original_position = other.transform.position;
                original_rotation = other.transform.rotation;
                original_scale = other.transform.localScale;

                hasStoredTransform = true;
            }
            isObjectNextYou = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject temp = other.gameObject;
        if (other.CompareTag("TakenObj"))
        {
            if (isObjectNextYou && isPressed)
            {
                objTaken = temp;

                temp.transform.SetParent(padre.transform);
                Rigidbody rb = temp.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                temp.transform.position = transform.position;
                temp.transform.rotation = transform.rotation;
                temp.transform.localScale = transform.localScale;
            }
            else
            {
                if (objTaken != null)
                {
                    objTaken = null;

                    temp.transform.SetParent(null);
                    Rigidbody rb = temp.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    temp.transform.localScale = original_scale;

                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TakenObj"))
        {
            isObjectNextYou = false;
        }
    }
}
