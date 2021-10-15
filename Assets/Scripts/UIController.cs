using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    public TMP_Text stCounterText;
    public TMP_Text tpCounterText;
    public TMP_Text algNameText;

    private static int s_stCounter = 0;
    private static int s_tpCounter = 0;
    private static string s_algName = "";
    
    // Start is called before the first frame update
    void Start()
    {
        s_stCounter = 0;
        s_tpCounter = 0;
        s_algName = "";
    }

    // Update is called once per frame
    void Update()
    {
        stCounterText.text = s_stCounter.ToString();
        tpCounterText.text = s_tpCounter.ToString();
        algNameText.text = s_algName;
    }

    public static void IncreaseST()
    {
        s_stCounter++;
    }
    
    public static void IncreaseTP()
    {
        s_tpCounter++;
    }
    
    public static void ChangeAlgName(string algName)
    {
        s_algName = algName;
    }
}
