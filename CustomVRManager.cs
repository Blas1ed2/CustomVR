// Keep In Mind This Code Is In Very, Very early testing phase! 

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Voice;

using ExitGames.Client.Photon;
using System;
using Photon.VR;
using JetBrains.Annotations;
using Unity.VisualScripting;

namespace CustomVR
{

    public class CustomVRManager : MonoBehaviourPunCallbacks
    {
        public static CustomVRManager Manager { get; private set; }

        [NonSerialized]
        private ConnectionState state = ConnectionState.Nothing;

        private string CustomRoomName;

        private List<string> CustomConnectIds;
        private List<string> CustomVoiceIds;

        private int CurrentRoomInt;

        private bool Testing;

        private bool CoolDown;

        void Start()
    {
            Manager = this;
    }

    void Update()
    {
        
    }

        public static void CustomServerConnect(List<string> CustomConnectId, List<string> CustomConnectVoiceId, string CustomServerName)
        {
            PhotonNetwork.Disconnect();

            if (Manager.CurrentRoomInt < CustomConnectId.Count && Manager.CurrentRoomInt < CustomConnectVoiceId.Count)
            {                
                
                if (Manager.state == ConnectionState.CustomConnecting)
                {
                    Manager.CurrentRoomInt++;
                }

                PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = CustomConnectId[Manager.CurrentRoomInt];
                PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice = CustomConnectVoiceId[Manager.CurrentRoomInt];

                Manager.CustomConnectIds = CustomConnectId;
                Manager.CustomVoiceIds = CustomConnectVoiceId;
                Manager.CustomRoomName = CustomServerName;

                PhotonNetwork.ConnectUsingSettings();
                


                Manager.state = ConnectionState.CustomConnecting;
            }
            else
            {
                Debug.LogError("Index out of range for CustomConnectId or CustomConnectVoiceId");
            }
        }

        public enum ConnectionState
        {
            Nothing,
            CustomConnecting
        };

        public override void OnConnectedToMaster()
        {
            if (Manager.state == ConnectionState.CustomConnecting)
            {
                CheckRoomPlayers();

                PhotonNetwork.JoinOrCreateRoom(CustomRoomName, new RoomOptions 
                { 
                    MaxPlayers = 20
                 
                }, TypedLobby.Default);
            }
        }

        public void CheckRoomPlayers()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.CountOfRooms < 2)
                {
                    Debug.Log("Connection Succesfull");
                }
                else
                {
                    Manager.Testing = true;

                    CustomServerConnect(CustomConnectIds, CustomVoiceIds, CustomRoomName);
                }
            }
        }

        IEnumerator Cooldown()
        {
            Manager.CoolDown = true;
            yield return new WaitForSeconds(1.5f);
            Manager.CoolDown = false;
        }

    }
}

