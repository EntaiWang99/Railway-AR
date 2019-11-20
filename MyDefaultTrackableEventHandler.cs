/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class MyDefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    public GameObject CubePrefab;
    public GameObject CubePrefab1;
    public GameObject CubePrefab2;
    public GameObject CubePrefab3;
    public GameObject CubePrefab4;
    public GameObject CubePrefab5;

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Enable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = true;

        //// Enable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = true;

        //// Enable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = true;

        GameObject MyCube = GameObject.Instantiate(CubePrefab);
        MyCube.transform.position = this.transform.position;
        MyCube.transform.rotation = this.transform.rotation;
        MyCube.transform.parent = this.transform;

        GameObject MyCube1 = GameObject.Instantiate(CubePrefab1);
        MyCube1.transform.position = this.transform.position;
        MyCube1.transform.parent = this.transform;

        GameObject MyCube2 = GameObject.Instantiate(CubePrefab2);
        MyCube2.transform.position = this.transform.position;
        MyCube2.transform.rotation = this.transform.rotation;
        MyCube2.transform.parent = this.transform;

        GameObject MyCube3 = GameObject.Instantiate(CubePrefab3);
        MyCube3.transform.position = this.transform.position;
        MyCube3.transform.parent = this.transform;

        GameObject MyCube4 = GameObject.Instantiate(CubePrefab4);
        MyCube4.transform.position = this.transform.position;
        MyCube4.transform.parent = this.transform;

        GameObject MyCube5 = GameObject.Instantiate(CubePrefab5);
        MyCube5.transform.position = this.transform.position;
        MyCube5.transform.parent = this.transform;

        //GameObject MyRedKuang = GameObject.Instantiate(RedKuangPrefab);
        //MyRedKuang.transform.position = this.transform.position;
        //MyRedKuang.transform.rotation = this.transform.rotation;
        //MyRedKuang.transform.parent = this.transform;

    }


    protected virtual void OnTrackingLost()
    {
        //var rendererComponents = GetComponentsInChildren<Renderer>(true);
        //var colliderComponents = GetComponentsInChildren<Collider>(true);
        //var canvasComponents = GetComponentsInChildren<Canvas>(true);

        //// Disable rendering:
        //foreach (var component in rendererComponents)
        //    component.enabled = false;

        //// Disable colliders:
        //foreach (var component in colliderComponents)
        //    component.enabled = false;

        //// Disable canvas':
        //foreach (var component in canvasComponents)
        //    component.enabled = false;

        Destroy(GameObject.Find("风管指导视频(Clone)"));
        Destroy(GameObject.Find("风管连接(Clone)"));
        Destroy(GameObject.Find("jingshipai警示牌(Clone)"));
        Destroy(GameObject.Find("2haopai号牌(Clone)"));
        Destroy(GameObject.Find("锤子(Clone)"));
        Destroy(GameObject.Find("info-2(Clone)"));
    }

    #endregion // PROTECTED_METHODS
}
