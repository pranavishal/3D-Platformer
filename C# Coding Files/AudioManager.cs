using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import an audio reference
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /*
     * the audio manager scrity will control how all mkusic and sound effects function inside the game
     * also will control the users ability to change music levels from the options menu
     * 
     */
    //create an instance of this class
    public static AudioManager instance;
    //create an audioSource array for music tracks
    //this variable only is accessible through the unityengine.Audio import
    public AudioSource[] tracks;
    //create an audioSource array for the different sound effects a player can have
    public AudioSource[] soundEffects;
    //make a reference to the music mixer
    public AudioMixerGroup musicMixer;
    //make a reference to the sound effects mixer
    public AudioMixerGroup soundEffectsMixer;
    //create an integer to store which track to play in the tracks array list
    //through this, the music can cghange through different scenes 
    public int trackToPlay;

    //this was a test used to make sure that the user could change the song through pressing a key
    //this was takern out of the final product as it was deemed unnecessary
    //private int currentTrack;
    
    //the awake function will initialize the instance of this class
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //when the game starts, run the playtracks method to start playing music
        PlayTracks(trackToPlay);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * The function commented out was tested to see if the user could change the song by pressing a key
         * 
         */
         //the only conditional to change the song is if the user presses shift
        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
        //change the currentTrack number by 1
            /*currentTrack++;
            //if the current track is at the end, then make the next increase back to the beginning
            //this avoids an index out of bounds error
            if(currentTrack >= 8)
            {
                currentTrack = 0;
            }
            PlayTracks(currentTrack);
            
            PlaySoundEffects(5);

        }
    */
        
    }
    /*
     * when this function is called the basic premise is to shut off all other tracks, and run only 1
     */
    public void PlayTracks(int trackNumber)
    {
        //for loop to run through all of the tracks and stop all of them from playing
        for (int i = 0; i < tracks.Length; i++)
        {
            //stop the track at position i in the array list
            tracks[i].Stop();
            
        }
        //now that all the tracks are done, play the track at the track number which was specified before the scene is run
        tracks[trackNumber].Play();
    }
    //a void to play the sound effects
    //same concept as PlayTracks except sound effects dont loop and are very short so they don't all need to be turned off
    public void PlaySoundEffects(int soundEffectToPlay)
    {
        //the .Play() is unitys in built function to play music
        soundEffects[soundEffectToPlay].Play();
    }
    //using the sliders in the options menu to change the music level
    public void SetMusicLevel()
    {
        //set the value of the volume from the game manager slider volume
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicSliderVol.value);
    }
    //using the sliders in the options menu to change the sound effects level
    public void SetSoundEffectsLevel()
    {
        //set the value of the sounf effects from the game manager slider for sound effects
        soundEffectsMixer.audioMixer.SetFloat("SoundEffectsVol", UIManager.instance.soundEffectsSliderVol.value);
    }
    //if the level is won (the player collider comes into  ontact with the level end object's collider
    public void LevelWon(int levelWon)
    {
        //play the victory theme
        PlayTracks(levelWon);
    }
}
