﻿/*  Copyright © 2016 NaturalPoint Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License. */

using System;
using UnityEngine;

// Implements live tracking of streamed OptiTrack rigid body data onto an object.
public class OptitrackRigidBody : MonoBehaviour {
    [Tooltip("The object containing the OptiTrackStreamingClient script.")]
    public OptitrackStreamingClient StreamingClient;

    [Tooltip("The Streaming ID of the rigid body in Motive")]
    public Int32 RigidBodyId;

    [Tooltip("Subscribes to this asset when using Unicast streaming.")]
    public bool NetworkCompensation = true;


    public Vector3 angleOffsets;
    public Vector3 positionOffsets;


    void Start() {
        // If the user didn't explicitly associate a client, find a suitable default.
        if ( this.StreamingClient == null ) {
            this.StreamingClient = OptitrackStreamingClient.FindDefaultClient();
            // If we still couldn't find one, disable this component.
            if ( this.StreamingClient == null ) {
                Debug.LogError( GetType().FullName + ": Streaming client not set, and no " + typeof( OptitrackStreamingClient ).FullName + " components found in scene; disabling this component.", this );
                this.enabled = false;
                return;
            }
        }
        this.StreamingClient.RegisterRigidBody( this, RigidBodyId );
    }

    void OnEnable() { Application.onBeforeRender += OnBeforeRender; }
    void OnDisable() { Application.onBeforeRender -= OnBeforeRender; }

    void OnBeforeRender() { UpdatePose(); }
    void Update() { UpdatePose(); }
    void UpdatePose() {
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState( RigidBodyId, NetworkCompensation);
        if ( rbState != null ) {
            
            //Have to flip X and Z position to match ARENA:
            this.transform.localPosition = new Vector3(rbState.Pose.Position.x, rbState.Pose.Position.y, rbState.Pose.Position.z) + positionOffsets;
            
            //Have to flip Z angle and add 180 to Y angle to match ARENA:
            Vector3 newEuler = rbState.Pose.Orientation.eulerAngles + angleOffsets;
            
            this.transform.localRotation = Quaternion.Euler(newEuler.x, newEuler.y, newEuler.z); //rbState.Pose.Orientation;
        }
    }
}
