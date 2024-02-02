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
    public int boundary;
    void Start()
    {
        GenerateFish();
        // foreach (GameObject fishGameObject in GameObject.FindGameObjectsWithTag("fish")){
        //     allFish.Add(fishGameObject.GetComponent<Fish>());
        //     Debug.Log("found fish: " + fishGameObject.name);
        // }
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
            if (fish.transform.position.x <=-boundary){
                newVector.x = boundary;
            }
            if (fish.transform.position.x > boundary){
                newVector.x = -boundary;
            }
            if (fish.transform.position.y <=-boundary){
                newVector.y = boundary;
            }
            if (fish.transform.position.y > boundary){
                newVector.y = -boundary;
            }
            if (fish.transform.position.z <=-boundary){
                newVector.z = boundary;
            }
            if (fish.transform.position.z > boundary){
                newVector.z = -boundary;
            }
            fish.transform.position = newVector;
            
        }
    }
    void GenerateFish(){
        for (int i = -boundary; i < boundary; i+=2){
            for (int j = -boundary; j < boundary; j+=2){
                for (int k = -boundary; k < boundary; k+=2){
                    Instantiate(fish, new Vector3(i, j, k), UnityEngine.Random.rotation);
                }
            }
        }
    }
}
