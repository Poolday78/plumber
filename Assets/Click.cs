using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    Pipe pipe;
    private void OnMouseDown()
    {
        pipe = Manager.instance.PipeMatch(this.gameObject);
        
        if (pipe == null)
        {
            pipe = Manager.instance.PipeMatch(this.transform.parent.gameObject);
        }

        if (pipe != null)
        {
            pipe.go.transform.Rotate(0, 0, -90);
        }
    }
}
