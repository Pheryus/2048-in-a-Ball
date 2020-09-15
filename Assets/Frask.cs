using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class Frask : MonoBehaviour
{
    public List<Bolota> bolotas;
    public List<Transform> bolotasSpawnPosition;

    public Text fraskText;
 
    public int currentGoal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateNewBolota(float goalIndex) {

        currentGoal = (int)Mathf.Pow(2f, goalIndex);
        fraskText.text = currentGoal.ToString();

        Bolota newBolota = Instantiate(BolotaManager.instance.bolota);
        int index = bolotas.Count;
        newBolota.transform.position = bolotasSpawnPosition[index].position;

        int numberIndex = UnityEngine.Random.Range(0, 2);
        newBolota.bolotaMaterial.material = BolotaManager.instance.bolotasColors[numberIndex];
        newBolota.materialId = numberIndex;
        newBolota.currentValue = (int)Mathf.Pow(2f, numberIndex + 1);
        newBolota.bolotaText.text = Mathf.Pow(2f, numberIndex + 1).ToString();

        bolotas.Add(newBolota);
    }

    public void UpdateBolotaJoin()
    {
        bool join = true;
        while (join)
        {
            join = CheckBolotaJoin();
        }

    }
    public bool CheckBolotaJoin()
    {
        foreach (Bolota b in bolotas)
        {
            if (bolotas.IndexOf(b)+1 == bolotas.Count)
            {
                return false;
            }
            else if (b.currentValue == bolotas[bolotas.IndexOf(b) + 1].currentValue)
            {
                BolotaJoin(b, bolotas[bolotas.IndexOf(b) + 1]);
                return true;
            }
        }
        return false;
    }

    private void BolotaJoin(Bolota b, Bolota b2)
    {
        b2.currentValue = b.currentValue + b2.currentValue;
        b2.bolotaText.text = b2.currentValue.ToString();
     
        b2.bolotaMaterial.material = BolotaManager.instance.bolotasColors[b2.materialId + 1];
        b2.materialId++;

        RemoveBolota(b);
        Destroy(b.gameObject);
        OrderBolotas();
    }

    public void BolotaBreak()
    {
        Bolota b = bolotas[0];
        RemoveBolota(b);
        Destroy(b.gameObject);
        OrderBolotas();
        UpdateGoal();
    }

    public void UpdateGoal()
    {
        currentGoal = currentGoal * 2;
        fraskText.text = currentGoal.ToString();
    }

    public Bolota GetLastBolota() 
    {
        return bolotas[bolotas.Count-1];
    
    }

    public void AddBolota(Bolota newBolota)
    {
        int index = bolotas.Count;
        newBolota.transform.position = bolotasSpawnPosition[index].position;
        bolotas.Add(newBolota);
    }
    public void RemoveBolota(Bolota bolota)
    {
        bolotas.Remove(bolota);
    }

 

    public void OrderBolotas() 
    {
    
        for(int i=0; i< bolotas.Count; i++)
        {
            bolotas[i].transform.position = bolotasSpawnPosition[i].position;
        }
    
    }

}
