using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 300f;
    private Vector3 lastMousePosition;
    private bool isDragging = false;

    private void Update()
    {
        // Mouse basýlmaya baþladýðýnda
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        // Mouse basýlý tutulduðunda
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // Yatay harekete göre dönüþ yap
            float rotationAmount = delta.x * Time.deltaTime * rotationSpeed;
            transform.Rotate(0, rotationAmount, 0);

            lastMousePosition = Input.mousePosition;
        }
        // Mouse býrakýldýðýnda
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Mobil için touch kontrolü
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isDragging = true;
                    lastMousePosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 delta = touch.position - new Vector2(lastMousePosition.x, lastMousePosition.y);
                        float rotationAmount = delta.x * Time.deltaTime * rotationSpeed;
                        transform.Rotate(0, rotationAmount, 0);
                        lastMousePosition = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    break;
            }
        }
    }
}