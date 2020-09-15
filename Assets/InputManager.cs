using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InputManager instance;
    public Transform selector;

    public Bolota bolotaToChange;
    public Frask fraskToChange;

    public Frask currentFrask;
    public bool selecting = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (selecting == true) 
        {
            bolotaToChange.transform.position = currentFrask.bolotasSpawnPosition[currentFrask.bolotasSpawnPosition.Count -1].position;
        
        }


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentFrask = BolotaManager.instance.frasks[BolotaManager.instance.frasks.IndexOf(currentFrask) + 1];
            if (currentFrask.bolotas.Count != 0)
            {
                selector.position = currentFrask.bolotas[currentFrask.bolotas.Count - 1].transform.position;
            }
            else
            {
                selector.position = currentFrask.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentFrask = BolotaManager.instance.frasks[BolotaManager.instance.frasks.IndexOf(currentFrask) - 1];
            if (currentFrask.bolotas.Count != 0)
            {
                selector.position = currentFrask.bolotas[currentFrask.bolotas.Count - 1].transform.position;
            }
            else
            {
                selector.position = currentFrask.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selecting == false)
            {
                selecting = true;
                bolotaToChange = currentFrask.GetLastBolota();
                fraskToChange = currentFrask;
            }

            else if (selecting == true)
            {
                selecting = false;
                currentFrask.AddBolota(bolotaToChange);
                fraskToChange.RemoveBolota(bolotaToChange);
                BolotaManager.instance.UpdateBolotasJoins();
                StartCoroutine(BolotaManager.instance.GeneratingAfterChange(currentFrask, fraskToChange));
                
               
            }
        }


    }




}
