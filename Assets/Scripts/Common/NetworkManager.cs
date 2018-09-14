using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;

namespace Network
{
    public class NetworkManager : UnitySingleton<NetworkManager>
    {
        private Network.WebClient _client = null;

        protected override void Awake()
        {
            base.Awake();
            _client = this.gameObject.AddComponent<Network.WebClient>();
        }

        private string DataPath
        {
            get
            {
    #if UNITY_EDITOR
                return "file://" + Application.streamingAssetsPath + "/";
    #elif UNITY_ANDROID
                return Application.streamingAssetsPath + "/";
    #elif UNITY_IOS
                return Application.dataPath + "/Raw/";
    #endif
            }
        }

        private string Resolve(string filename)
        {
            return this.DataPath + filename;
        }

        public void GetLocalData(string path, WebClient.WebResponse response)
        {
            _client.Request(Network.WebRequest.GET(Resolve(path)), response);
        }

        /*
        private void OnResponse(long statusCode, string body)
        {
            Debug.Log(statusCode);
            Debug.Log(body);
        }*/
    }
}
