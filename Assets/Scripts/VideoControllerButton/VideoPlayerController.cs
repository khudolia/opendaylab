using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private Transform _playerTransform;
    public float pauseDistance = 5.0f; // Adjust this value based on your requirements
    public VideoButtonController videoButtonController;
    
    private bool isPlayerInRange = true;

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();

        if (_playerTransform == null)
        {
            // Assuming the player is the main camera.
            _playerTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (_playerTransform == null)
            return;

        // Calculate the distance between the player and the VideoPlayer.
        float distance = Vector3.Distance(_playerTransform.position, transform.position);

        // Check if the player is within the pause distance.
        if (distance > pauseDistance)
        {
            if (!_videoPlayer.isPaused)
            {
                _videoPlayer.Pause();
                videoButtonController.ShowPlay();
            }
        }
    }
    
    public void ToggleVideoPlayerState()
    {
        if (_videoPlayer.isPaused)
        {
            _videoPlayer.Play();
            videoButtonController.ShowPause();
        }
        else
        {
            _videoPlayer.Pause();
            videoButtonController.ShowPlay();
        }

    }
}