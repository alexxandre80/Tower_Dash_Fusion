using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class DataCollector : MonoBehaviour {

    private static DataCollector instance;

    [SerializeField] private bool activated = true;
    
    [SerializeField] private DataVault dataVault;
    [SerializeField] private bool resetData = true;
    
    

    
    

    public static DataCollector Instance() {
        return instance;
    }

    private void Start() {
        if (instance == null) {
            instance = this;
        }
        if (this.resetData) {
            instance.dataVault.ResetData();
        }
        
        

        if (isActivated() && PhotonNetwork.isMasterClient)
        {
            
            string path = "SpringBoard.txt";
            if (File.Exists(path))
            {
                

                string dataAsJson = File.ReadAllText(path);
                string[] split = dataAsJson.Split('*');

                
                foreach (string springBoardData in split)
                {
                    if (!(springBoardData == ""))
                    {
                        UnityEngine.Debug.Log(springBoardData);
                       
                        foreach (GameObject trampoline in GameObject.FindGameObjectsWithTag("Trampoline"))
                        {
                            UnityEngine.Debug.Log("MISE A JOUR TRAMPOLINE");
                        
                            if (SpringBoardData.CreateFromJSON(springBoardData).code == trampoline.GetComponent<Trampoline>().codeSpringBoard)
                            {
                                trampoline.GetComponent<Trampoline>().nbUtilisation =
                                    trampoline.GetComponent<Trampoline>().nbUtilisation + 1;

                                trampoline.GetComponent<Trampoline>().textNBU.text =
                                    trampoline.GetComponent<Trampoline>().nbUtilisation.ToString();




                            }
                        }
                    }
                    
                    

                        

                    
                }
            }
            
             path = "death.txt";
            if (File.Exists(path))
            {
             
            
                string dataAsJson = File.ReadAllText(path);
                string[] split = dataAsJson.Split('*');
            
                foreach (string deathdata in split)
                {
                    if(!(deathdata==""))
                    {
                        DeathData ladeathdata = JsonUtility.FromJson<DeathData>(deathdata);
                    
                    

                        if (ladeathdata.death == "shot")
                        {
                            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "DCGunDeath"), ladeathdata.deathPos,
                                Quaternion.identity, 0);
                        
                        }

                        else
                        {
                            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "DCFallDeath"), ladeathdata.deathPos,
                                Quaternion.identity, 0);
                        }

                    }
                        
                   
                }
                
                
            }
            
            
            
            
        }
        
        
        
        
    }

    public static void RegisterObjectPickedUp(Vector3 pos, float timeStamp, string objectTag, int player) {
        if (instance != null && instance.dataVault != null) instance.dataVault.AddPickUpPosition(pos, timeStamp, objectTag, player);

        else
        {
            print("la fonction RegisterObjectPickedUp dit que datavault du datacollector est vide");
        }
    }
    
    public static void RegisterDeath(Vector3 pos, float timeStamp,  int player, string death) {
        if (instance != null && instance.dataVault != null)
        {
            instance.dataVault.AddDeathData(pos,  timeStamp,  player,  death);

            if (death == "shot")
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "DCGunDeath"), pos,
                    Quaternion.identity, 0);
            }

            else
            {
                PhotonNetwork.Instantiate(Path.Combine("Prefabs", "DCFallDeath"), pos,
                    Quaternion.identity, 0);
            }
            
            
        }
        
    }
    
    public static void RegisterSpringBoardUse(int code) {
        if (instance != null && instance.dataVault != null)
        {
            instance.dataVault.AddSpringboardData(code);
            
        }
        
    }
    
    public void CreateText()
    {
        string path = "death.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"");
        }

        foreach (DeathData uneDeathData in dataVault.deathData)
        {
            File.AppendAllText(path, JsonUtility.ToJson(uneDeathData) + "*");
        }
        
        path = "objects.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"");
        }

        foreach (PickUpData unePickupData in dataVault.pickUpData)
        {
            File.AppendAllText(path, JsonUtility.ToJson(unePickupData)+ "*");
        }
        
        path = "SpringBoard.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path,"");
        }

        foreach (SpringBoardData uneSpringBoardData in dataVault.springBoardData)
        {
            File.AppendAllText(path, JsonUtility.ToJson(uneSpringBoardData)+ "*");
        }
        
        
    }

    public bool isActivated()
    {
        return activated;
    }
    
    
}
