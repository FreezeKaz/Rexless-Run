using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    // Start is called before the first frame update
    public int Value = 5;
    [SerializeField] private List<GameObject> Fills;
    public event Action <int> OnValueChanged;

    public void Increase()
    {
        Value += Value == 5 ? 0 : 1;
        DeactivateObjectsByVolume();
        OnValueChanged?.Invoke(Value);
    }


    public void Decrease()
    {
        Value -= Value > 0 ? 1 : 0;
        DeactivateObjectsByVolume();
        OnValueChanged?.Invoke(Value);
    }

    private void DeactivateObjectsByVolume()
    {
        foreach (var obj in Fills)
        {
            obj.SetActive(true);
        }

        int objectsToDeactivate = 5 - Value;
        for (int i = 0; i < objectsToDeactivate; i++)
        {
            int index = Fills.Count - 1 - i;
            if (index >= 0)
            {
                Fills[index].SetActive(false);
            }
        }
    }
}
