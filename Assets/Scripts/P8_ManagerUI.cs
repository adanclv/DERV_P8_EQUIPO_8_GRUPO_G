using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P8_ManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtColor;
    [SerializeField] TextMeshProUGUI txtResult;

    void Start()
    {
        txtResult.text = "";
    }

    public void SetColor(string color)
    {
        txtColor.text = color;
    }

    public void SetResult(string result)
    {
        txtResult.text = result;
    }

    public void ResetResult()
    {
        txtResult.text = "";
    }
}
