using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UpgradeButtons : MonoBehaviour
{
    public List<UpgradesSO> upgrades;
    public TextMeshProUGUI moneyText;
    public AutoClicker autoClicker;
    public List<GameObject> objetos;
    public List<Image> Imagenes;
    public Button[] botones;

    private void Awake()
    {
        if (objetos.Count != upgrades.Count)
        {
            Debug.LogError("La cantidad de objetos y actualizaciones no coincide.");
            return;
        }

        for (int i = 0; i < objetos.Count; i++)
        {
            TextMeshProUGUI titulo = objetos[i].transform.Find("Titulo").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cost = objetos[i].transform.Find("Costo").GetComponent<TextMeshProUGUI>();
            titulo.text = upgrades[i].nombre;
            cost.text = upgrades[i].cost.ToString("N0");
        }
    }

    private void Start()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i;
            botones[i].onClick.AddListener(() => PurchaseUpgrade(index));
        }
    }
    /*private void Update()
    {
        for (int i = 0; i < objetos.Count; i++)
        {
            UpgradesSO UPGRADE = upgrades[i];
            if (autoClicker.money >= UPGRADE.cost)
            {
               

            }
        }
    }*/

    public void PurchaseUpgrade(int index)
    {
        if (index < 0 || index >= upgrades.Count)
        {
            Debug.LogError("�ndice de actualizaci�n fuera de rango.");
            return;
        }

        UpgradesSO upgrade = upgrades[index];

        if (autoClicker.money >= upgrade.cost)
        {
            AudioManager.instance.Play("Mejora");
            autoClicker.money -= upgrade.cost;
            autoClicker.AddClickPerSecond(upgrade.clickPerSecondBonus);
            autoClicker.AddClickPerTouch(upgrade.clickPerTouchBonus);
        }
    }
}
