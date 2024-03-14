using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    [SerializeField]
    List<HelpPage> allPages;

    [SerializeField]
    Canvas helpCanvas;

    int currentHelpIndex = 0;

    GameMath currentMath;

    // Start is called before the first frame update
    void Start()
    {
        HidePage();
        currentMath = GameManager.Manager.CurrentMath;
        foreach (var page in allPages)
        {
            page.SetData(currentMath);
        }
    }

    public void ShowPage()
    {
        currentHelpIndex = 0;
        UpdateShownPage();
        helpCanvas.enabled = true;
    }

    public void HidePage()
    {
        helpCanvas.enabled = false;
    }

    public void NextPage()
    {
        currentHelpIndex = (currentHelpIndex + 1) % allPages.Count;
        UpdateShownPage();
    }

    public void PreviousPage()
    {
        currentHelpIndex--;
        if (currentHelpIndex < 0)
        {
            currentHelpIndex = allPages.Count - 1;
        }
        UpdateShownPage();
    }

    void UpdateShownPage()
    {
        for (int i = 0; i < allPages.Count; i++)
        {
            HelpPage page = allPages[i];
            page.gameObject.SetActive(i == currentHelpIndex);
        }
    }
}
