// Testing phase 11/10/20223

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomVR;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class CustomConnect : MonoBehaviourPunCallbacks
{
    public string ServerName;
    public List<string> CustomServerIds;
    public List<string> CustomVoiceIds;

    private bool CanTrigger;
    private bool CoolDown;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FingerTip"))
        {
            if (!CoolDown)
            {
            CustomVRManager.CustomServerConnect(CustomServerIds, CustomVoiceIds, ServerName);
                StartCoroutine(Cooldown());
            CanTrigger = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FingerTip"))
        {
            CanTrigger = true;
        }
    }

    IEnumerator Cooldown()
    {
        CoolDown = true;
        yield return new WaitForSeconds(2f);
        CoolDown = false;
    }
}
