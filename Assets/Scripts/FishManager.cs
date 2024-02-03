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
    
    public float startVelMag = 1;
    public float fishTankTorque = 0.1f;
    public float innerFishTankTorque = 0.1f;
    public float tankRadius = 20;
    public float innerTankRadius = 5;
    public float maxTorque = 1;

    public bool a = false;
    public bool b = false;
    public bool c = false;
    void Start()
    {
        GenerateFish();
        // GenerateSingleFish();

        foreach (GameObject fishGameObject in GameObject.FindGameObjectsWithTag("fish")){
            allFish.Add(fishGameObject.GetComponent<Fish>());
            Debug.Log("found fish: " + fishGameObject.name);
        }

        foreach (Fish fish in allFish){
            fish.selfRigidbody.AddRelativeForce(Vector3.left*startVelMag, ForceMode.VelocityChange);
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

        foreach (Fish fish in allFish){
            // outer tank
            Vector3 tankTorque = Vector3.Cross(fish.selfRigidbody.velocity, fish.transform.position);
            Vector3 newTorque = Vector3.Normalize(tankTorque);
            if (newTorque.magnitude == 0){
                newTorque = UnityEngine.Random.insideUnitSphere;
                newTorque = Vector3.Normalize(newTorque);
                newTorque *= innerFishTankTorque;
                Debug.Log(Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position));
            }
            else if (tankRadius-fish.transform.position.magnitude == 0){
            }
            else if (fish.transform.position.magnitude > tankRadius){
                newTorque/= tankRadius-fish.transform.position.magnitude;
                newTorque *= fishTankTorque;
                Debug.Log(Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position));
                // if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) < 90){
                //     fish.selfRigidbody.AddTorque(newTorque*fish.torqueScaling, ForceMode.VelocityChange);
                // }
            }
            else{
                newTorque/= tankRadius-fish.transform.position.magnitude;
                newTorque *= -1*fishTankTorque;
                // if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) < 90){
                // fish.selfRigidbody.AddTorque(newTorque*fish.torqueScaling, ForceMode.VelocityChange);
                // }
            }
            if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) < 90){
                fish.selfRigidbody.AddTorque(newTorque*fish.torqueScaling, ForceMode.VelocityChange);
            }
            // fish.selfRigidbody.AddTorque(newTorque*fish.torqueScaling, ForceMode.VelocityChange);
            // inner sphere
            Vector3 innerTankTorque = Vector3.Cross(fish.selfRigidbody.velocity, fish.transform.position);
            Vector3 newInnerTorque = Vector3.Normalize(innerTankTorque);
            if (innerTankRadius-fish.transform.position.magnitude == 0){
                // innerTankTorque = UnityEngine.Random.insideUnitSphere;
                // newInnerTorque = Vector3.Normalize(innerTankTorque);
                // newInnerTorque *= innerFishTankTorque;
                // fish.selfRigidbody.AddTorque(newInnerTorque*fish.torqueScaling, ForceMode.VelocityChange);
            }
            else if (fish.transform.position.magnitude > innerTankRadius){
                newInnerTorque/= fish.transform.position.magnitude-innerTankRadius;
                newInnerTorque *= innerFishTankTorque;
                if (newInnerTorque.magnitude > maxTorque){
                    newInnerTorque = Vector3.Normalize(innerTankTorque);
                    newInnerTorque *= maxTorque;
                }
                // if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) < 90){
                // fish.selfRigidbody.AddTorque(newInnerTorque*fish.torqueScaling, ForceMode.VelocityChange);
                // }
            }
            else{
                newInnerTorque/= fish.transform.position.magnitude-innerTankRadius;
                newInnerTorque *= -1*innerFishTankTorque;
                if (newInnerTorque.magnitude > maxTorque){
                    newInnerTorque = Vector3.Normalize(innerTankTorque);
                    newInnerTorque *= maxTorque;
                }
                // if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) < 90){
                // fish.selfRigidbody.AddTorque(newInnerTorque*fish.torqueScaling, ForceMode.VelocityChange);
                // }
            }
            if (Vector3.Angle(fish.selfRigidbody.velocity, fish.transform.position) > 90){
                fish.selfRigidbody.AddTorque(newInnerTorque*fish.torqueScaling, ForceMode.VelocityChange);
            }
            // fish.selfRigidbody.AddTorque(newInnerTorque*fish.torqueScaling, ForceMode.VelocityChange);
        }


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
            fishTankTorque = 1f;
            innerFishTankTorque = 0f;
            // inhale(1, 1, 0.5f);
        }
        if (Time.time >= 15 && !b){
            Debug.Log("Exhale");
            b = true;
            fishTankTorque = 0f;
            innerFishTankTorque = 1f;
            // inhale(2, 0.5f, 1f);
        }
        if (Time.time >= 20 && !c){
            Debug.Log("INHALE");
            c = true;
            fishTankTorque = 0.1f;
            innerFishTankTorque = 0.1f;
            
            // inhale(2, 0.5f, 1f);
        }


        // if (currInhale){
        //     float newMass = Mathf.Lerp(inhaleStartMass, inhaleEndMass, (startInhale+=Time.fixedDeltaTime)/inhaleDur);
        //     if (startInhale > inhaleDur){
        //         currInhale = false;
        //     }
        //     foreach (Fish fish in allFish){
        //         fish.selfRigidbody.mass = newMass;
        //     }
        // }
        // else if (currExhale){
        //     float newMass = Mathf.Lerp(1.5f, 0.5f, (startExhale+=Time.fixedDeltaTime)/exhaleDur);
        //     if (startExhale > exhaleDur){
        //         currExhale = false;
        //     }
        //     foreach (Fish fish in allFish){
        //         fish.selfRigidbody.mass = newMass;
        //     }
        // }
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

    void GenerateSingleFish(){
        Instantiate(fish, new Vector3(20, 0, 0), Quaternion.Euler(0, 180, 0));
    }

    void GenerateFish(){
        for (int i = -boundary; i < boundary; i+=2){
            for (int j = -boundary; j < boundary; j+=2){
                for (int k = -boundary; k < boundary; k+=2){
                    // Vector3 pos = new Vector3(i, j, k);
                    // if (pos.magnitude > innerTankRadius+1){
                    //     Instantiate(fish, new Vector3(i, j, k), UnityEngine.Random.rotation);
                    // }
                    Instantiate(fish, new Vector3(i, j, k), UnityEngine.Random.rotation);
                }
            }
        }
    }
}
