using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragezeichen : MonoBehaviour
{
    public GameObject dialogPrefab;
    private LevelContentManager levelContentManager;

    // Start is called before the first frame update
    void Start()
    {
        levelContentManager = LevelContentManager.GetInstance();
    }

    public void GetHint()
    {
        string hintStr = levelContentManager.GetHint();
        Dialog dialog1 = Dialog.Open(dialogPrefab, DialogButtonType.OK, "Hinweis", hintStr, true);
    }
}
