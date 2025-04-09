using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private LineRenderer lr;
    private Canvas cv;
    private WireGame wireGame;

    [SerializeField]
    private bool isSucces = false;


    private bool isDragStarted;


    private void Awake()
    {
        wireGame = GetComponentInParent<WireGame>();
        image = GetComponent<Image>();
        lr = GetComponent<LineRenderer>();
        cv = GetComponentInParent<Canvas>();

        lr.useWorldSpace = true;
        lr.sortingLayerName = "UI"; // Of een andere layer die bovenaan ligt
        lr.sortingOrder = 100; // Hoe hoger, hoe meer 'vooraan' het ligt
        lr.startColor = image.color;
        lr.endColor = image.color;
    }

    private void Update()
    {
        if (isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                cv.transform as RectTransform,
                Input.mousePosition,
                cv.worldCamera,
                out movePos);

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, cv.transform.TransformPoint(movePos));
        }
        else
        {
            if (isSucces) return;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, Vector3.zero);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSucces) return;
        Debug.Log("yes");
        isDragStarted = true;
        wireGame.currentDrag = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (wireGame.currentDrag != null)
        {
            if(wireGame.Hoverd != null)
            {
                if (wireGame.currentDrag.image.color == wireGame.Hoverd.image.color)
                {
                    isSucces = true;
                    wireGame.Hoverd.isSucces = true;
                    wireGame.CorrectWires();
                }else
                {
                    wireGame.LoseLife();
                }
            }else
            {
                wireGame.LoseLife();
            }
        }
        Debug.Log("no");
        isDragStarted = false;
        wireGame.currentDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        wireGame.Hoverd = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        wireGame.Hoverd = null;

    }
}
