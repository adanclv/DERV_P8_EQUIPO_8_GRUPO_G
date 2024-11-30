using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plaquita : MonoBehaviour
{
    int rand, count;
    bool isCorrect;
    Renderer colorsito;
    GameObject aux;
    List<Colores> colores = new List<Colores>();

    [SerializeField] Tomar_Objeto pickUp;
    [SerializeField] P8_ManagerUI managerUI;

    public struct Colores
    {
        public Color color;
        public string nameColor;
    }

    void Start()
    {
        aux = null;
        isCorrect = false;
        count = 0;

        colorsito = GetComponent<Renderer>();
        
        colores.Add(new Colores { color = new Color(1.0f, 0f, 0f, 1.0f), nameColor = "Rojo" });
        colores.Add(new Colores { color = new Color(0f, 1.0f, 0f, 1.0f), nameColor = "Green" });
        colores.Add(new Colores { color = new Color(0f, 0f, 1.0f, 1.0f), nameColor = "Blue" });

        rand = ObtenerElemento();
        colorsito.material.color = colores[rand].color;
        AsignarColor();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TakenObj")
        {
            aux = other.gameObject;
            if (other.GetComponent<Renderer>().material.color == colorsito.material.color)
            {
                isCorrect = true;
                managerUI.SetResult("Correcto");
                other.gameObject.SetActive(false);
                colores.RemoveAt(rand);

                count++;

                Invoke("ResetResult", 2.0f);
                Invoke("ResetTransform", 2.0f);
            }
            else
            {
                managerUI.SetResult("Incorrecto");
                isCorrect = false;
                ResetTransform();
                Invoke("ResetResult", 1.0f);
            }
        }
    }

    void ResetTransform()
    {
        if (aux != null)
        {
            pickUp.hasStoredTransform = false;

            if (isCorrect)
            {
                rand = ObtenerElemento();
                if (count < 3) AsignarColor();
                
                isCorrect = false;
                return;
            }
            aux.transform.position = pickUp.original_position;
            aux.transform.rotation = pickUp.original_rotation;
        }
    }

    int ObtenerElemento()
    {
        if (colores.Count == 1)
        {
            return 0;
        }
        int numRand = Random.Range(0, colores.Count);
        return numRand;
    }

    void AsignarColor()
    {
        string color = colores[rand].nameColor;
        colorsito.material.color = colores[rand].color;
        managerUI.SetColor(color);
    }

    void ResetResult()
    {
        if (count == 3)
        {
            managerUI.SetResult("¡Felicidades! Has completado el juego");
            managerUI.SetColor("");
            gameObject.SetActive(false);
            Invoke("changeScene", 2.0f);
            return;
        }
        managerUI.ResetResult();
    }

    void changeScene()
    {
        SceneManager.LoadScene(2);
    }
}
