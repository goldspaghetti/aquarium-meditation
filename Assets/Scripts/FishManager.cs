using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Fish> allFish = new List<Fish>();
    public GameObject fish;
    public float radius;
    void Start()
    {
        GenerateFish();
        foreach (GameObject fishGameObject in GameObject.FindGameObjectsWithTag("fish")){
            allFish.Add(fishGameObject.GetComponent<Fish>());
            Debug.Log("found fish: " + fishGameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        // move all fish, if outside sphere, move to the opposite side of sphere
        foreach (Fish fish in allFish){
            // go to the other side!
            // if (Vector3.Distance(transform.position, fish.transform.position)>radius){
                
            // }
            Vector3 newVector = fish.transform.position;
            if (fish.transform.position.x <=-6){
                newVector.x = 6;
            }
            if (fish.transform.position.x > 6){
                newVector.x = -6;
            }
            if (fish.transform.position.y <=-6){
                newVector.y = 6;
            }
            if (fish.transform.position.y > 6){
                newVector.y = -6;
            }
            if (fish.transform.position.z <=-6){
                newVector.z = 6;
            }
            if (fish.transform.position.z > 6){
                newVector.z = -6;
            }
            fish.transform.position = newVector;
            
        }
    }
    void GenerateFish(){
        for (int i = -6; i < 6; i+=2){
            for (int j = -6; j < 6; j+=2){
                for (int k = -6; k < 6; k+=2){
                    Instantiate(fish, new Vector3(i, j, k), UnityEngine.Random.rotation);
                }
            }
        }
    }
}
