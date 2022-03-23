using UnityEngine;
using TMPro;

/* summary :
 * Linked to UICanvas/AverageConso
 * Handles the buttons on the AverageConso panel to display water examples
 * 
 * variables : 
 * - private -
 * sceneData - Link to SceneData's script
 * createMesh - Link to MeshHandler's script
 */
public class ExampleConsumption : MonoBehaviour
{
    private SceneData sceneData;
    private CreateMesh createMesh;

    public void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        foreach (SceneData.ExampleName name in SceneData.ExampleName.GetValues(typeof(SceneData.ExampleName)))
        {
            GameObject.Find(name.ToString() + "/Text").GetComponent<TextMeshProUGUI>().text += (" (" + sceneData.GetExampleConsumption(name) * 1000 + " L)");
        }
    }

    /* summary :
     * Calls createMesh to set a new water volume value
     * 
     * parameter :
     * whichOne - String to know which button is clicked
     */
    public void SetValue(string whichOne)
    {
        float volume = 0f;
        foreach (SceneData.ExampleName tmpName in SceneData.ExampleName.GetValues(typeof(SceneData.ExampleName)))
        {
            if (whichOne.Equals(tmpName.ToString()))
            {
                volume = sceneData.GetExampleConsumption(tmpName);
            }
        }
        if (volume != 0f)
        {
            createMesh.SetCustomVolume(volume*1000);
            GameObject.Find("AverageConso").SetActive(false);
        }
    }
}
