using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SusunKata : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public static SusunKata wordDragged;
    [SerializeField] TextMeshProUGUI wordDisplay;
    private bool hint, filled;
    private Vector3 startPosition;
    private Transform parentPosition;
    float damage = 5f;
    public string Word { get ; private set; }
    private QuizController quizController;

    public void Initialize(Transform parent, string word, bool hint, QuizController quizController){
        Word = word;
        transform.SetParent(parent);
        this.hint = hint;
        this.quizController = quizController;
        GetComponent<CanvasGroup>().alpha = hint ? 0.5f : 1f;
        if(this.hint)
            wordDisplay.SetText("");
        else
            wordDisplay.SetText(Word);
    }

    public void Match(Transform parent){
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        hint = true;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if(hint)
            return;
        
        startPosition = transform.position;
        parentPosition = transform.parent;
        wordDragged = this;
        GetComponent<CanvasGroup>().blocksRaycasts = false; // Dapat membaca huruf dibelakangnya
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(hint)
            return;
        
        transform.position = Input.mousePosition;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (hint && !filled){
            if(wordDragged.Word == Word){
                KataManager.Instance.TambahPoin();
                wordDragged.Match(transform);
                filled = true;
                GetComponent<CanvasGroup>().alpha = 1f;
            }
            else{
                this.quizController.timer -= damage;
                quizController.Wrong();
            }
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if(hint)
            return;
        
        wordDragged = null;

        if(transform.parent == parentPosition){
            transform.position = startPosition;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}