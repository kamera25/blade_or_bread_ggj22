using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildNumberScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var _text = this.GetComponent<Text>();
        string _version = Application.version;
        string _unityVersion = Application.unityVersion;
        _text.text = $"Build version : {_version} / Unity {_unityVersion}";
    }
}
