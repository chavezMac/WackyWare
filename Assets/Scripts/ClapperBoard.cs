using UnityEngine;
using UnityEngine.UI;

public class ClapperBoard : MonoBehaviour
{
    public Text roll;
    public Text scene;
    public Text take;
    public Text director;
    public Text sound;
    public Text cameracred;
    public Text date;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateClapperText(int sceneNum, string directorName, string soundName, string cameraName)
    {
        roll.text = Random.Range(1, 15).ToString();
        scene.text = sceneNum.ToString();
        take.text = Random.Range(1, 15).ToString();
        director.text = directorName;
        sound.text = soundName;
        cameracred.text = cameraName;
        date.text = System.DateTime.Today.ToLongDateString();
    }
}
