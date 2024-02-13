using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelMusicController : MonoBehaviour
{
    public List<AudioClip> playlist;
    public TextMeshProUGUI nowPlayingText;
    public Sprite playSprite;
    public Sprite pauseSprite;
    private AudioSource audioSource;
    private List<AudioClip> shuffledPlaylist;
    private int currentTrackIndex = 0;
    private bool isPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Certifique-se de que o loop está desativado
        ShufflePlaylist();
        PlayTrack(currentTrackIndex);
    }

    void Update()
    {
        if (isPlaying && !audioSource.isPlaying)
        {
            OnTrackEnd();
        }
    }

    void ShufflePlaylist()
    {
        shuffledPlaylist = new List<AudioClip>(playlist);
        for (int i = 0; i < shuffledPlaylist.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledPlaylist.Count);
            AudioClip temp = shuffledPlaylist[i];
            shuffledPlaylist[i] = shuffledPlaylist[randomIndex];
            shuffledPlaylist[randomIndex] = temp;
        }
    }

    void PlayTrack(int index)
    {
        audioSource.clip = shuffledPlaylist[index];
        audioSource.Play();
        isPlaying = true;
        UpdateNowPlayingText();
    }

    void UpdateNowPlayingText()
    {
        if (shuffledPlaylist.Count > 0)
        {
            nowPlayingText.GetComponent<TMP_Text>().text = "Now Playing: " + shuffledPlaylist[currentTrackIndex].name;
        }
        else
        {
            nowPlayingText.GetComponent<TMP_Text>().text = "";
        }
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % shuffledPlaylist.Count;
        PlayTrack(currentTrackIndex);
    }

    public void PreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + shuffledPlaylist.Count) % shuffledPlaylist.Count;
        PlayTrack(currentTrackIndex);
    }

    void OnTrackEnd()
    {
        if (currentTrackIndex == shuffledPlaylist.Count - 1)
        {
            ShufflePlaylist();
            currentTrackIndex = 0;
        }
        else
        {
            NextTrack();
        }
    }
}