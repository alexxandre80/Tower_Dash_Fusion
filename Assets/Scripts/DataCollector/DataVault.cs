using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new data vault",menuName = "Heat/new data vault")]
public class DataVault : ScriptableObject {
    [SerializeField] private List<PickUpData> PickUpData;
    public List<PickUpData> pickUpData => this.PickUpData;
    
    [SerializeField] private List<DeathData> DeathData;
    public List<DeathData> deathData => this.DeathData;
    
    [SerializeField] private List<SpringBoardData> SpringBoardData;
    public List<SpringBoardData> springBoardData => this.SpringBoardData;
    
    public void ResetData() {
        this.PickUpData.Clear();
        this.DeathData.Clear();
    }
    
    // fonction de collecte d'info sur les objets
    
    public void AddPickUpPosition(Vector3 pos) {
        this.PickUpData.Add(new PickUpData(){PickUpPos = pos});
    }
    public void AddPickUpPosition(Vector3 pos, float timeStamp) {
             this.PickUpData.Add(new PickUpData(){PickUpPos = pos, PartyTime = timeStamp});
    }
    
    public void AddPickUpPosition(Vector3 pos, float timeStamp, string objectTag) {
             this.PickUpData.Add(new PickUpData(){PickUpPos = pos, PartyTime = timeStamp, objectName = objectTag});
    }
    
    public void AddPickUpPosition(Vector3 pos, float timeStamp, string objectTag, int player) {
        this.PickUpData.Add(new PickUpData(){PickUpPos = pos, PartyTime = timeStamp, objectName = objectTag, player = player});
    }
    
    public void AddDeathData(PickUpData data) {
        this.PickUpData.Add(data);
    }
    
    //fonction de collecte de mort
    
    public void AddDeathData(Vector3 pos) {
        this.DeathData.Add(new DeathData(){deathPos = pos});
    }
    public void AddDeathData(Vector3 pos, float timeStamp) {
        this.DeathData.Add(new DeathData(){deathPos = pos, PartyTime = timeStamp});
    }
    
    public void AddDeathData(Vector3 pos, float timeStamp, int player) {
        this.DeathData.Add(new DeathData(){deathPos = pos, PartyTime = timeStamp, player = player});
    }
    
    public void AddDeathData(Vector3 pos, float timeStamp, int player, string death) {
        this.DeathData.Add(new DeathData(){deathPos = pos, PartyTime = timeStamp,  player = player, death = death});
    }
    
    public void AddDeathData(DeathData data) {
        this.DeathData.Add(data);
    }
    
    //Fonction collecte jump
    
    public void AddSpringboardData(int unCode) {
        this.SpringBoardData.Add(new SpringBoardData(){code = unCode});
    }

    
    
}

[Serializable]
public class PickUpData {
    public Vector3 PickUpPos;
    public float PartyTime;
    public String objectName;
    public int player;

}
[Serializable]
public class DeathData {
    public Vector3 deathPos;
    public float PartyTime;
    public int player;
    public string death;

}




[System.Serializable]
public class SpringBoardData {
    

    public int code;
    
    public static SpringBoardData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<SpringBoardData>(jsonString);
    }
 

}

public enum EntityKilled {
    Player,
    Bot
}