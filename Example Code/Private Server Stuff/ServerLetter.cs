// Early testing phase 11/10/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLetter : MonoBehaviour
{
    public CustomConnect ConnectButton;
    public string Letter;
    public float CooldownPerLetter;
    private bool CoolDown;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FingerTip"))
        {
            if (!CoolDown)
            {
            if (ConnectButton.ServerName.Length < ConnectButton.MaxNameLength)
            {
            ConnectButton.ServerName += Letter;
            }
                StartCoroutine(Cooldown());
            }
        }
    }


    IEnumerator Cooldown()
    {
        CoolDown = true;
        yield return new WaitForSeconds(CooldownPerLetter);
        CoolDown = false;
    }
}
