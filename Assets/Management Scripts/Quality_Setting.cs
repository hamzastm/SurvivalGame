using UnityEngine;
using UnityEngine.UI;
public class Quality_Setting : MonoBehaviour
{
    [SerializeField] private Dropdown qualityDropDown;

    private void Awake()
    {
        qualityDropDown = GetComponent<Dropdown>();
    }

    public void SetQualitySetting(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
