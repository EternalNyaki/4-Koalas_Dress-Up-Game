using System.Collections;
using System.Collections.Generic;
using RhythmMicrogame;
using UnityEngine;
using UnityEngine.UI;

public class RhythmInputManager : MonoBehaviour
{
    public Image playerSprite;
    public SongInfo song;

    public GameObject notePrefab;

    public Sprite defaultSprite, upDanceSprite, downDanceSprite, leftDanceSprite, rightDanceSprite;

    public float leniency;

    private Conductor _conductor;
    private List<Note> _notes;

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

        StartCoroutine(_conductor.Play());
    }

    // Update is called once per frame
    void Update()
    {
        NotesUpdate();
        VisualUpdate();
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
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) &&
                    _conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {

                    Debug.Log("Hit!!");
                    Destroy(focusedNote.gameObject);
                    _notes.Remove(focusedNote);
                }
                break;

            case NoteInfo.Direction.Down:
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) &&
                    _conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {

                    Debug.Log("Hit!!");
                    Destroy(focusedNote.gameObject);
                    _notes.Remove(focusedNote);
                }
                break;

            case NoteInfo.Direction.Left:
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) &&
                    _conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {

                    Debug.Log("Hit!!");
                    Destroy(focusedNote.gameObject);
                    _notes.Remove(focusedNote);
                }
                break;

            case NoteInfo.Direction.Right:
                if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) &&
                    _conductor.songPosition <= focusedNote.data.beat + leniency / song.crotchet && _conductor.songPosition >= focusedNote.data.beat - leniency / song.crotchet)
                {

                    Debug.Log("Hit!!");
                    Destroy(focusedNote.gameObject);
                    _notes.Remove(focusedNote);
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
            playerSprite.sprite = upDanceSprite;
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
