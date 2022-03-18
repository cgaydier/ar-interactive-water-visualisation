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

    void Start()
    {
        sceneData = GameObject.Find("SceneData").GetComponent<SceneData>();
        createMesh = GameObject.Find("MeshHandler").GetComponent<CreateMesh>();
        foreach (SceneData.ExampleName name in SceneData.ExampleName.GetValues(typeof(SceneData.ExampleName)))
        {
            GameObject.Find(name.ToString() + "/Text").GetComponent<TextMeshProUGUI>().text += (" (" + sceneData.GetExampleConsumption(name) * 1000 + " L)");
        }
    }

    /* summary :
     * Call createMesh to set a new water value
     * 
     * parameter :
     * whichOne - String to know wich button was clicked
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
