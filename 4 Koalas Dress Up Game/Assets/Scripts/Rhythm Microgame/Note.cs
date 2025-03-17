using System.Collections;
using System.Collections.Generic;
using RhythmMicrogame;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float positionOffset;
    public float speed;

    public NoteInfo data;

    private Conductor _conductor;

    private RectTransform _rect;
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _conductor = Conductor.Instance;

        _rect = (RectTransform)transform;
        _image = GetComponent<Image>();
    }

    public void SetData(NoteInfo data)
    {
        this.data = data;

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForFixedUpdate();

        switch (data.direction)
        {
            case NoteInfo.Direction.Up:
                _image.sprite = _conductor.upNoteSprite;
                break;

            case NoteInfo.Direction.Down:
                _image.sprite = _conductor.downNoteSprite;
                break;

            case NoteInfo.Direction.Left:
                _image.sprite = _conductor.leftNoteSprite;
                break;

            case NoteInfo.Direction.Right:
                _image.sprite = _conductor.rightNoteSprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _rect.anchoredPosition = new((data.beat - _conductor.songPosition) * speed + positionOffset, 0f);
    }
}
