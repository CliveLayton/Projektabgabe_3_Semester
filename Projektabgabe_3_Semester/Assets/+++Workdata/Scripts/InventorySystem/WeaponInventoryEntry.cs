using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponInventoryEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private GameObject selectionVisuals;

    public Weapon weapon;

    /// <summary>
    /// displays the name of the weapon it gets equipped
    /// </summary>
    /// <param name="weapon">Script Weapon</param>
    public void Initialize(Weapon weapon)
    {
        selectionVisuals.SetActive(false);
        this.weapon = weapon;
        displayNameText.text = weapon.displayName;
    }

    /// <summary>
    /// select this weapon or not
    /// </summary>
    /// <param name="isSelected">boolean</param>
    public void SetSelected(bool isSelected)
    {
        selectionVisuals.SetActive(isSelected);
    }
}
