using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultCanvasUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI resultText;
    void Start()
    {
        
    }
    public void setResultText(string _text)
    {
        resultText.text = _text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
