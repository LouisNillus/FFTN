using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    
    public List<AudioClip> clips = new List<AudioClip>();

    public Queue<AudioClip> queue = new Queue<AudioClip>();
    public Stack<AudioClip> pastSongs = new Stack<AudioClip>();

    public AudioSource audioSource;

    public AudioClip currentClip;

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        QueueSongs(QueueType.Random);
        PlayNext();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PlayPrevious();
        if (Input.GetKeyDown(KeyCode.N)) PlayNext();
    }


    public void QueueSongs(QueueType queueType)
    {
        List<AudioClip> temp = new List<AudioClip>(clips);
       
        for (int i = 0; i < clips.Count; i++)
        {
            if(queueType == QueueType.Random)
            {
                AudioClip ac = temp[Random.Range(0, temp.Count - 1)];
                queue.Enqueue(ac);
                temp.Remove(ac);
            }
            else if (queueType == QueueType.Lined)
            {
                AudioClip ac = temp[0];
                queue.Enqueue(ac);
                temp.RemoveAt(0);
            }
        }
    }

    public void PlayNext()       
    {

        StopAllCoroutines();
        audioSource.Stop();

        if(currentClip != null) pastSongs.Push(currentClip);

        AudioClip ac = queue.Dequeue();
        currentClip = ac;


        audioSource.PlayOneShot(ac);
        StartCoroutine(CheckClipEnd(ac));


        if (queue.Count == 0) QueueSongs(QueueType.Random);
    }

    public void PlayPrevious()
    {
        if (pastSongs.Count == 0) return;

        StopAllCoroutines();
        audioSource.Stop();
        
        queue.Enqueue(currentClip);

        AudioClip ac = pastSongs.Pop();
        currentClip = ac;
        audioSource.PlayOneShot(ac);

        StartCoroutine(CheckClipEnd(ac));
    }

    public IEnumerator CheckClipEnd(AudioClip clip)
    {
        float clipLength = clip.length;

        yield return new WaitForSeconds(clipLength);

        PlayNext();
    }

}
public enum QueueType {Random, Lined}
