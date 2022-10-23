using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeUi : MonoBehaviour
{
    public GameObject Winner;

    //text component
    TextMeshProUGUI PlayerWin_text;


    // Start is called before the first frame update
    void Start()
    {
        PlayerWin_text = Winner.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWin_text.text = Name.name + " wins!!!";
    }
}
