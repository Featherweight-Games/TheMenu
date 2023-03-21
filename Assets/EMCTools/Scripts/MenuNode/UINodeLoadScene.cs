using UnityEngine;
using UnityEngine.SceneManagement;

namespace EMCTools
{
    public class UINodeLoadScene : UIMenuNode
    {
        [SerializeField] string nameOfSceneToLoad;
        //[SerializeField] bool playTransitionBackwards;
        //[SerializeField] bool destroyAllParticipantsOnSceneLoad;
        //[SerializeField] bool updateAllParticipantActionMaps;
        //[SerializeField] string nameOfActionMap;
        //[SerializeField] Animator transitionAnimator;

        override public void OnAction(Action action, int playerNumber)
        {
            switch (action)
            {
                case Action.Submit:
                    SceneManager.LoadScene(nameOfSceneToLoad);
                    break;
                default:
                    break;
            }
        }

        //IEnumerator LoadScene(float timeToWait)
        //{
        //    foreach (PlayerParticipant participant in FindObjectsOfType<PlayerParticipant>())
        //        participant.DisablePlayerInput();

        //    yield return new WaitForSeconds(timeToWait);

        //    if (destroyAllParticipantsOnSceneLoad)
        //    {
        //        foreach (PlayerParticipant participant in FindObjectsOfType<PlayerParticipant>())
        //            participant.DestroyParticipant();
        //    }

        //    SceneManager.LoadScene(nameOfSceneToLoad);

        //    foreach (PlayerParticipant participant in FindObjectsOfType<PlayerParticipant>())
        //        participant.EnablePlayerInput();
        //}
    }
}