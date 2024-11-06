using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

// require a video player component to be attached to the same game object
[RequireComponent(typeof(UnityEngine.Video.VideoPlayer))]


// This script is used to control the video player component from the GUI
public class VideoControls : MonoBehaviour
{
    public UnityEngine.Video.
    VideoPlayer videoPlayer;
    /// <summary>
    /// The animator that controls fading in and out of the video
    /// </summary>
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get the video player component
        if (videoPlayer == null)
            videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();

        // Set up the prepare completed event
        videoPlayer.prepareCompleted += OnPrepared;

        // Set up the loop point reached event which is triggered when the video is done playing
        videoPlayer.loopPointReached += OnComplete;
    }

    public void SetVideoURL(string url)
    {
        // Debug.Log("Setting video URL to: " + url);

        // Set the video to play. URL can be either a local file or a http URL.
        // Handled by the VideoPlayer component
        GetComponent<VideoPlayer>().url = url;

        // Prepare the video
        videoPlayer.Prepare();        
    }

    public void OnPrepared(VideoPlayer source)
    {
        Debug.Log("Video is prepared");

        // Get the resolution of the video
        int width = source.texture.width;
        int height = source.texture.height;
        Debug.Log("Video resolution: " + width + "x" + height);
        
        // Create a new rendertexture for the video player
        // Set the resolution of the render texture to match the video resolution
        RenderTexture renderTexture = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();

        // Set the rendertexture as the target texture of the video player
        source.targetTexture = renderTexture;
        
        // Get the material of the player
        Material videoMaterial = GetComponent<Renderer>().material;

        // Set the render texture as the main texture of the player's material
        videoMaterial.mainTexture = renderTexture;
        
        // Reveal the video player
        if (animator != null)
            animator.SetFloat("direction", 1.0f);

        // Play the video and pause it immediately to show the first frame in the render texture
        videoPlayer.Play();
        videoPlayer.Pause();
    }
    private void OnComplete(VideoPlayer source)
    {
        Debug.Log("Video playback is complete");
        if (animator != null)
            animator.SetFloat("direction", -1.0f);
    }

    public void Stop()
    {
        // Stop the video
        videoPlayer.Stop();
    }

    public void Play()
    {
        // Play the video
        videoPlayer.Play();
    }

    public void Pause()
    {
        // Pause the video
        videoPlayer.Pause();
    }

    public void Skip(float increment = 10.0f)
    {
        // Skip increment seconds in the video
        videoPlayer.time += increment;
    }

    public void Seek(float percent)
    {
        // Seek to a specific percentage in the video
        videoPlayer.time = videoPlayer.length * percent;
    }
}
