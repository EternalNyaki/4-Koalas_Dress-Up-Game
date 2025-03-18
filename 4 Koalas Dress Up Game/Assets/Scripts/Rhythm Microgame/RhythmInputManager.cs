using System.Collections;
using System.Collections.Generic;
using RhythmMicrogame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RhythmInputManager : MonoBehaviour
{
    public Image playerSprite;
    public SongInfo song;

    public GameObject notePrefab;

    public Sprite defaultSprite, upDanceSprite, downDanceSprite, leftDanceSprite, rightDanceSprite;

    public float leniency;

    public TMP_Text scoreText;

    public AudioClip goodHitSound;
    public AudioClip missSound;

    public AudioSource audioSource;

    private Conductor _conductor;
    private List<Note> _notes;

    private int _score;
    private int _maxScore;

    // Start is called before the first frame update
    void Start()
    {
        _conductor = Conductor.Instance;

        _conductor.SetSong(song);

        _notes = new List<Note>();
        foreach (NoteInfo note in song.chartInfo)
        {
            _notes.Add(Instantiate(notePrefab, new(1000f, 0f, 0f), Quaternion.identity, transform).GetComponent<Note>());
            _notes[_notes.Count - 1].SetData(note);
        }

        _score = 0;
        _maxScore = _notes.Count;

        _conductor.Play();
    }

    // Update is called once per frame
    void Update()
    {
        NotesUpdate();
        VisualUpdate();

        scoreText.text = "Score: " + _score + "/" + _maxScore;
        PlayerManager.Instance.SetDiscoScore(_score, _maxScore);
    }

    private void NotesUpdate()
    {
        if (_notes.Count <= 0) { return; }

        Note focusedNote = _notes[0];

        if (_conductor.songPosition >= focusedNote.data.beat + leniency / song.crotchet)
        {
            Debug.Log("Miss...");
            _notes.Remove(focusedNote);
            Destroy(focusedNote.gameObject);
            return;
        }

        switch (focusedNote.data.direction)
        {
            case NoteInfo.Direction.Up:
                if (_conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log("Miss...");
                        audioSource.PlayOneShot(missSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);
                    }
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Debug.Log("Hit!!");
                        audioSource.PlayOneShot(goodHitSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);

                        _score++;
                    }
                }
                break;

            case NoteInfo.Direction.Down:
                if (_conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log("Miss...");
                        audioSource.PlayOneShot(missSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);
                    }
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        Debug.Log("Hit!!");
                        audioSource.PlayOneShot(goodHitSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);

                        _score++;
                    }
                }
                break;

            case NoteInfo.Direction.Left:
                if (_conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log("Miss...");
                        audioSource.PlayOneShot(missSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);
                    }
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Debug.Log("Hit!!");
                        audioSource.PlayOneShot(goodHitSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);

                        _score++;
                    }
                }
                break;

            case NoteInfo.Direction.Right:
                if (_conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Debug.Log("Miss...");
                        audioSource.PlayOneShot(missSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);
                    }
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Debug.Log("Hit!!");
                        audioSource.PlayOneShot(goodHitSound);

                        Destroy(focusedNote.gameObject);
                        _notes.Remove(focusedNote);

                        _score++;
                    }
                }
                break;
        }
    }

    private void VisualUpdate()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            playerSprite.sprite = upDanceSprite;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            playerSprite.sprite = downDanceSprite;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            playerSprite.sprite = rightDanceSprite;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            playerSprite.sprite = leftDanceSprite;
        }
        else
        {
            playerSprite.sprite = defaultSprite;
        }
    }
}
