using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BolotaManager : MonoBehaviour
{

    public static BolotaManager instance;
    public List<Material> bolotasColors;
    public List<Frask> frasks;
    public Bolota bolota;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        InstantiateFirstBolotas();
        InputManager.instance.selector.position = frasks[0].bolotas[frasks[0].bolotas.Count - 1].transform.position;
        InputManager.instance.currentFrask = frasks[0];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateBolotasJoins()
    {
        foreach(Frask fr in frasks)
        {
            fr.UpdateBolotaJoin();
        }
    }

    private void InstantiateFirstBolotas()
    {
        foreach (Frask f in frasks)
        {
            int index = frasks.IndexOf(f);
            f.InstantiateNewBolota(index+1);
        }
       

    }

    internal void GenerateBolotasAfterChange(Frask f1, Frask f2)
    {
        Debug.Log("F1" + f1, f1.gameObject);
        Debug.Log("F1" + f2, f2.gameObject);
        foreach (Frask f in frasks)
        {
            if (f != f1 && f != f2)
            {
                int index = frasks.IndexOf(f);
                f.InstantiateNewBolota(index + 1);
            }
        }    
    }

    public void BreakCheck()
    {
        foreach (Frask f in frasks)
        {
            if (f.bolotas.Count != 0 && f.currentGoal == f.bolotas[0].currentValue)
            {
                f.BolotaBreak();
            }
        }
    }

    public IEnumerator GeneratingAfterChange(Frask currentFrask, Frask fraskToChange)
    {
        yield return new WaitForSeconds(0.5f);
        GenerateBolotasAfterChange(currentFrask, fraskToChange);
        yield return new WaitForSeconds(0.5f);
        UpdateBolotasJoins();
        yield return new WaitForSeconds(0.5f);
        BolotaManager.instance.BreakCheck();
    }

}
