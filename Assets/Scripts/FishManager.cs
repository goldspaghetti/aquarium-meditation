using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Fish> allFish = new List<Fish>();
    public GameObject fish;
    public int boundary;
    public float startInhale;
    public float inhaleDur;
    public float inhaleEnd;
    public bool currInhale;
    public float inhaleStartMass;
    public float inhaleEndMass;

    public float startExhale;
    public float exhaleDur;
    public bool currExhale;
    public float exhaleEnd;
    
    public bool a = false;
    public bool b = false;
    public bool c = false;
    void Start()
    {
        GenerateFish();

        foreach (GameObject fishGameObject in GameObject.FindGameObjectsWithTag("fish")){
            allFish.Add(fishGameObject.GetComponent<Fish>());
            Debug.Log("found fish: " + fishGameObject.name);
        }
        // Time.fixedDeltaTime = 0.2f;
        // foreach (Fish fish in allFish){
        //     fish.forceScaling = 100;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        // move all fish, if outside sphere, move to the opposite side of sphere
        // foreach (Fish fish in allFish){
        //     // go to the other side!
        //     // if (Vector3.Distance(transform.position, fish.transform.position)>radius){
                
        //     // }
        //     Vector3 newVector = fish.transform.position;
        //     if (fish.transform.position.x <=-boundary){
        //         newVector.x = boundary;
        //     }
        //     if (fish.transform.position.x > boundary){
        //         newVector.x = -boundary;
        //     }
        //     if (fish.transform.position.y <=-boundary){
        //         newVector.y = boundary;
        //     }
        //     if (fish.transform.position.y > boundary){
        //         newVector.y = -boundary;
        //     }
        //     if (fish.transform.position.z <=-boundary){
        //         newVector.z = boundary;
        //     }
        //     if (fish.transform.position.z > boundary){
        //         newVector.z = -boundary;
        //     }
        //     fish.transform.position = newVector;
            
        // }
        if (Time.time >= 10 && !a){
            Debug.Log("INHALE");
            a = true;
            foreach (Fish fish in allFish){
                fish.fishTankTorque = 1f;
            }
            // inhale(1, 1, 0.5f);
        }
        if (Time.time >= 15 && !b){
            Debug.Log("INHALE");
            b = true;
            foreach (Fish fish in allFish){
                fish.fishTankTorque = -1f;
            }
            // inhale(2, 0.5f, 1f);
        }
        if (Time.time >= 20 && !c){
            Debug.Log("INHALE");
            c = true;
            foreach (Fish fish in allFish){
                fish.fishTankTorque = 0.1f;
            }
            // inhale(2, 0.5f, 1f);
        }


        if (currInhale){
            float newMass = Mathf.Lerp(inhaleStartMass, inhaleEndMass, (startInhale+=Time.fixedDeltaTime)/inhaleDur);
            if (startInhale > inhaleDur){
                currInhale = false;
            }
            foreach (Fish fish in allFish){
                fish.selfRigidbody.mass = newMass;
            }
        }
        else if (currExhale){
            float newMass = Mathf.Lerp(1.5f, 0.5f, (startExhale+=Time.fixedDeltaTime)/exhaleDur);
            if (startExhale > exhaleDur){
                currExhale = false;
            }
            foreach (Fish fish in allFish){
                fish.selfRigidbody.mass = newMass;
            }
        }
    }

    void inhale(float sec, float minMass, float maxMass){
        currInhale = true;
        inhaleDur = sec;
        startInhale = Time.time;
        inhaleEnd = startInhale + inhaleDur;
        inhaleEndMass = maxMass;
        inhaleStartMass = minMass;

    }

    void exhale(float sec){
        currExhale = true;
        exhaleDur = sec;
        startExhale = Time.time;
        exhaleEnd = startExhale + exhaleDur;
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
