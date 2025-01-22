using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverEvent : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource audioSource;      // Reference to the AudioSource component
    private Image buttonImage;           // Reference to the Button's Image component
    private Color originalColor;         // Store the original button color
    public Color hoverColor = Color.red; // Set your desired hover color in the Inspector

    private void Awake()
    {
        // Get the AudioSource component attached to the button
        audioSource = GetComponent<AudioSource>();

        // Get the Image component attached to the button (for color changing)
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            originalColor = buttonImage.color; // Store the original color
        }
    }

    // This method is triggered when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Change button color
        if (buttonImage != null)
        {
            buttonImage.color = hoverColor;
        }
    }

    // This method is triggered when the mouse pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert button color to the original
        if (buttonImage != null)
        {
            buttonImage.color = originalColor;
        }
    }
}



