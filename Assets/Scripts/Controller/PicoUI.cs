using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicoUI : MonoBehaviour
{
    public Text hintText;

    public void SetText(string content)
    {
        hintText.text = content;
    }
}
