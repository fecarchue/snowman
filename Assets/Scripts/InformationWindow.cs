using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json.Linq;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

public class InformationWindow : MonoBehaviour
{
    TMP_Text Weight, Mass;
    public void Awake()
    {
        Weight = GameObject.Find("Weight").GetComponent<TMP_Text>();
        Mass = GameObject.Find("Mass").GetComponent<TMP_Text>();
    }

    //´«»ç¶÷ Á¤º¸ Ç¥½ÃÇØÁÜ
    public void LoadStatus()
    {
        GameObject ClickWhat = EventSystem.current.currentSelectedGameObject;
        string SlotId = ClickWhat.name;
        Debug.Log(ClickWhat.name);

        string path = Path.Combine(Application.dataPath + "/Data/" + "snowballData.json");
        string Jsondata = File.ReadAllText(path);
        JObject status = JObject.Parse(Jsondata);

        Weight.text = "Weight: " + status["Weight"].ToString();
        Mass.text = "Mass: " + status["Mass"].ToString();
    }
}