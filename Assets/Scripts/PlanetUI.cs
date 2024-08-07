using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetUI : MonoBehaviour
{
    [SerializeField] public Arrow[] planetList;
    [SerializeField] public int activePlanetIndex = 0;

    [SerializeField] public Button confirmButton;
    // Start is called before the first frame update
    void Start()
    {
       // confirmButton = transform.Find("ConfirmButton").GetComponent<Button>();

        confirmButton.onClick.RemoveListener(OnClickConfirmButton);
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }

    public void SetActivePlanetIndex(int index)
    {
        activePlanetIndex = index;
    }

    public void OnClickConfirmButton()
    {
        Debug.Log("ÇöÀç>> activePlanetIndex:" + activePlanetIndex);

        planetList[activePlanetIndex].SceneMove();
    }
}
