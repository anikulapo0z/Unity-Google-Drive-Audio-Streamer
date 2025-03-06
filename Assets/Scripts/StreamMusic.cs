using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StreamMusic : MonoBehaviour
{
    // Remember to use UnityEngine.Networking
    // Remember to add the audio source to the game object and attach this script to it
    // This script will stream music from a URL
    // Make sure to change the audio type to the of the music you are streaming
    // This script uses a public google drive link to stream music
    // Make sure to copy the file ID from the google drive link and replace it in the musicURL

    public string musicURL = "https://drive.google.com/uc?export=download&id=1DTNdZkZ3UKfuFsfd-49l7eSmOI_apXDL";
    public AudioSource audioSource;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Create a UnityWebRequest that fetches an AudioClip of type MPEG (change to the correct AudioType if needed)
        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(musicURL, AudioType.MPEG))
        {
            // Enable streaming so playback can begin before full download
            ((DownloadHandlerAudioClip)request.downloadHandler).streamAudio = true;
            yield return request.SendWebRequest();

            // Check for network or protocol errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                // Get the AudioClip from the download handler and play it
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
