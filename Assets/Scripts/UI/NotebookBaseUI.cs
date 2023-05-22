﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookBaseUI : MonoBehaviour
{
    //public static bool timeIsStopped = false;
    private AudioSource source;
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                Close();
        }
    }

    public void Open()
    {
        CanvasContainerHandler.SetSceneUIVisible(false);
        PlaySound();
        transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        GameTime.timeIsStopped = true;
        RenderOrderAdjustment.anyOverlayOpen = true;

    }

    public void Close()
    {
        CanvasContainerHandler.SetSceneUIVisible(true);
        //Debug.Log("CLOSE");
        transform.GetChild(0).localScale = new Vector3(0, 0, 0);

        //Debug.Log(transform.GetChild(0).localScale);
        GameTime.timeIsStopped = false;
        RenderOrderAdjustment.anyOverlayOpen = false;
        // 
        GameObject.Find("JobManagement").GetComponent<JobsManagement>().UpdateActiveJobs(false);
    }

    public void SwitchToTab()
    {
        // close this one
        // open the target
    }

    public void PlaySound()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(sound, 1f);
    }

}
