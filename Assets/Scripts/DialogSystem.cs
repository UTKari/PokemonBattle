using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }
    [SerializeField]
    private Text dialogText;
    [SerializeField]
    private float timerBetweenWords = 0.25f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string showAnimationName = "ShowDialog";
    [SerializeField]
    private string hideAnimationName = "HideDialog";
    private Coroutine showDialogCoroutine;
    
    private void Awake()
    {
        Instance = this;
    }
    public void ShowDialog(string dialog)
    {
        if (showDialogCoroutine != null)
        {
            StopCoroutine(showDialogCoroutine);
        }
        showDialogCoroutine = StartCoroutine(ShowDialogCoroutine(dialog));
    }
    private IEnumerator ShowDialogCoroutine(string dialog)
    {
        dialogText.text = "";
        animator.Play(showAnimationName, 0, 0f);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            SoundManager.instance.Play("DialogLetter", 1f, Random.Range(0.9f, 1.1f));
            yield return new WaitForSeconds(timerBetweenWords);
        }
        yield return new WaitForSeconds(1f);
        animator.Play(hideAnimationName, 0, 0f);
    }
    public void StopDialog()
    {
        if (showDialogCoroutine != null)
        {
            StopCoroutine( showDialogCoroutine);
            showDialogCoroutine = null;
        }
        dialogText.text = "";
        animator.Play(hideAnimationName, 0, 0f);
    }

}
