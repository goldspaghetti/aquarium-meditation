using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    // public SphereCollider collider;
    public ArrayList nearbyFish = new ArrayList();
    public Rigidbody selfRigidbody;
    public Vector3 targetVelocity;
    public float torqueScaling = 1f;
    public float forceScaling = 0.01f;
    public float startVelMag = 1;
    bool start = true;
    public float tankRadius = 20;
    // public float maxTankTorqueMag = 10;
    // public Vector3 targetDir;
    // public bool useTargetDir;
    // public GameObject cube;
    public int updateClock = 20;
    public int updateClockDur = 200;
    public float fishTankTorque = 0.1f;
    void Start()
    {
        // if (this.name == "FishV1"){
        //     Vector3 a = new Vector3(1F, 0, 0);
        //     this.selfRigidbody.AddForce(a, ForceMode.VelocityChange);
        // }
        
    }

    // void FixedUpdate(){
    //     if (this.name == "FishV1" && this.selfRigidbody.velocity.magnitude < 1){
    //         Vector3 a = new Vector3(1, 1, 1);
    //         Debug.Log(a);
    //         // this.selfRigidbody.AddForce(a, ForceMode.VelocityChange);
    //         // Debug.Log(this.selfRigidbody.velocity.magnitude);
    //         this.selfRigidbody.AddForce(a, ForceMode.Force);
    //         // Debug.Log("applying force");
    //     }
    //     Debug.Log(this.selfRigidbody.velocity.magnitude);
    // }

    void FixedUpdate(){
        //Each particle is subjected to a force, depending on the difference in velocity with all neighbors within a fixed distance,
        //The force is proportional to the difference of speeds, and to the cosine of half the angle between orientations
        //and to a torque, depending on the sum of orientation differences.

        // using vector3 left since model is offset :(

        if (start){
            start=false;
            // selfRigidbody.AddRelativeForce(Vector3.left * startVelMag, ForceMode.VelocityChange);
            selfRigidbody.AddRelativeForce(Vector3.left*startVelMag, ForceMode.VelocityChange);
        }
        else if (nearbyFish.Count > 0){
            float forceMag = this.selfRigidbody.velocity.magnitude;
            // Quaternion torque = this.transform.localRotation;
            Vector3 torqueDir = new Vector3(0, 0, 0);
            foreach (Rigidbody rigidbody in this.nearbyFish){
                forceMag += rigidbody.velocity.magnitude;
                Quaternion rotDiff = rigidbody.transform.localRotation* Quaternion.Inverse(this.transform.localRotation);
                // this.transform.localRotation * Quaternion.Inverse(rigidbody.transform.localRotation);
                torqueDir += new Vector3(rotDiff.x, rotDiff.y, rotDiff.z);
            }
            torqueDir /= this.nearbyFish.Count;
            selfRigidbody.AddTorque(torqueDir*torqueScaling, ForceMode.Force);
            forceMag /= (this.nearbyFish.Count+1);
            selfRigidbody.AddRelativeForce(Vector3.left * forceMag*forceScaling, ForceMode.Force);
            //and to a torque, depending on the sum of orientation differences.

            
        }
        else{
            selfRigidbody.AddRelativeForce(Vector3.left * forceScaling, ForceMode.Force);
        }
        
        if (name == "FishV1"){
            // Debug.Log("current velocity: " + selfRigidbody.velocity.magnitude);
            // Debug.Log("local rotation: " + transform.localRotation);
            // Debug.Log("rotation: " + transform.rotation);
            // transform.position;
        }

        Vector3 tankTorque = Vector3.Cross(selfRigidbody.velocity, transform.position);
        Vector3 newTorque = Vector3.Normalize(tankTorque);
        if (tankRadius-transform.position.magnitude == 0){
            
        }
        else{
            newTorque/= tankRadius-transform.position.magnitude;
            newTorque *= -1*fishTankTorque;
        }
        if (Vector3.Angle(selfRigidbody.velocity, transform.position) < 90){
            selfRigidbody.AddTorque(newTorque*torqueScaling, ForceMode.VelocityChange);
        }
        updateClock -= 1;
        if (updateClock <= 0){
            Vector3 randTorque = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            selfRigidbody.AddTorque(randTorque*torqueScaling, ForceMode.Force);
            float randForce = Random.Range(-10f, 10f);
            selfRigidbody.AddRelativeForce(Vector3.left * randForce*forceScaling, ForceMode.Force);
            updateClock = updateClockDur;
        }
        // selfRigidbody.AddTorque(tankTorque, ForceMode.VelocityChange);


        // Vector3 tankTorque = Vector3.Cross(selfRigidbody.velocity, transform.position);
        // float tankTorqueMag = 0;
        // if (transform.position.magnitude >= tankRadius){
        //     tankTorqueMag = maxTankTorqueMag;
        //     if (Vector3.Angle(selfRigidbody.velocity, transform.position) == 0){
        //         Debug.Log("0!!");
        //         tankTorque = UnityEngine.Random.insideUnitSphere;
        //     }
        //     else if (Vector3.Angle(selfRigidbody.velocity, transform.position) > 90){
        //         Debug.Log(">90!!");
        //         tankTorqueMag = 0;
        //     }
        // }
        // tankTorque *= tankTorqueMag;
        // selfRigidbody.AddTorque(tankTorque, ForceMode.Force);
        // // transform.rotation
        // if (name == "FishV1"){
        //     Debug.Log("tank torque: " + tankTorque);
        //     Debug.Log("distance: " + transform.position.magnitude);
        // }



    }
    /*
    void FixedUpdate(){
        //Each particle is subjected to a force, depending on the difference in velocity with all neighbors within a fixed distance,
        //The force is proportional to the difference of speeds, and to the cosine of half the angle between orientations
        //and to a torque, depending on the sum of orientation differences.

        // using vector3 left since model is offset :(

        if (start){
            start=false;
            selfRigidbody.AddRelativeForce(Vector3.left * startVelMag, ForceMode.VelocityChange);
        }
        else if (nearbyFish.Count > 0){
        float forceMag = this.selfRigidbody.velocity.magnitude;
        // Quaternion torque = this.transform.localRotation;
        Vector3 torqueDir = new Vector3(0, 0, 0);
        foreach (Rigidbody rigidbody in this.nearbyFish){
            // forceMag += rigidbody.velocity.magnitude * Mathf.Cos(Quaternion.Angle(this.transform.localRotation, rigidbody.transform.localRotation));
            forceMag += rigidbody.velocity.magnitude;
            Quaternion rotDiff = this.transform.localRotation * Quaternion.Inverse(rigidbody.transform.localRotation);
            torqueDir += new Vector3(rotDiff.x, rotDiff.y, rotDiff.z);
            // torque += Quaternion.Angle(this.transform.localRotation, rigidbody.transform.localRotation)
        }
        torqueDir /= this.nearbyFish.Count;
        selfRigidbody.AddTorque(torqueDir*torqueMax*forceScaling, ForceMode.Force);
        forceMag /= this.nearbyFish.Count;
        selfRigidbody.AddRelativeForce(Vector3.left * forceMag, ForceMode.Force);
        //and to a torque, depending on the sum of orientation differences.
        }
    }
    */
    /*
    Vector3 Vicsek(){
        float forceMag = this.selfRigidbody.velocity.magnitude;
        // Quaternion torque = this.transform.localRotation;
        Vector3 torqueDir = new Vector3(0, 0, 0);
        foreach (Rigidbody rigidbody in this.nearbyFish){
            // forceMag += rigidbody.velocity.magnitude * Mathf.Cos(Quaternion.Angle(this.transform.localRotation, rigidbody.transform.localRotation));
            forceMag += rigidbody.velocity.magnitude;
            Quaternion rotDiff = this.transform.localRotation * Quaternion.Inverse(rigidbody.transform.localRotation);
            torqueDir += new Vector3(rotDiff.x, rotDiff.y, rotDiff.z);
            // torque += Quaternion.Angle(this.transform.localRotation, rigidbody.transform.localRotation)
        }
        torqueDir /= this.nearbyFish.Count;
        selfRigidbody.AddTorque(torqueDir*torqueMax*forceScaling, ForceMode.Force);
        forceMag /= this.nearbyFish.Count;
        selfRigidbody.AddRelativeForce(Vector3.left * forceMag, ForceMode.Force);
    }
    */

    // Update is called once per frame
    // void Update()
    // {

    // }
    void OnTriggerEnter(Collider collider){
        if (collider.name != "FishTank"){
            if (name == "FishV1"){
            Debug.Log("added fish!");
        }
        nearbyFish.Add(collider.attachedRigidbody);
        }
    }
    void OnTriggerExit(Collider collider){
        if (collider.name != "FishTank"){
            if (name == "FishV1"){
            Debug.Log("removed fish :(");
        }
        nearbyFish.Remove(collider.attachedRigidbody);
        }
    }
    // void OnCollisionEnter(Collision collision){
    //     Debug.Log("collided with ");
    //     Debug.Log(collision.body.name);
    // }
    // void OnCollisionExit(Collision collider){

    // }
    // void OnCollisionStay(Collision collision){
    //     Debug.Log("a");
    // }
    // void OnTriggerStay(Collider collider){
    //     Debug.Log("trigger collided with");
    //     Debug.Log(collider.name);
    // }
}
