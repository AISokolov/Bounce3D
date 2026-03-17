using UnityEngine;

public class StarsManager : MonoBehaviour
{
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private void Awake()
    {
        HideAllStars();
    }

    public void HideAllStars()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public int GetStarsFromTime(float timeInSeconds)
    {
        if (timeInSeconds < 120f) return 3;
        if (timeInSeconds < 180f) return 2;
        if (timeInSeconds < 240f) return 1;
        return 0;
    }

    public void ShowStarsForTime(float timeInSeconds)
    {
        int stars = GetStarsFromTime(timeInSeconds);
        ShowStars(stars);
    }

    public void ShowStars(int stars)
    {
        HideAllStars();

        if (stars >= 1) ShowStar(star1);
        if (stars >= 2) ShowStar(star2);
        if (stars >= 3) ShowStar(star3);
    }

    private void ShowStar(GameObject star)
    {
        star.SetActive(true);

        Animator animator = star.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
            animator.Play("Star", 0, 0f);
        }
    }

    public string GetStarsString(float timeInSeconds)
    {
        int stars = GetStarsFromTime(timeInSeconds);
        return stars + "/3";
    }
}