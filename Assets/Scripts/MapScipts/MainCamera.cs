using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    #region Singleton
    public static MainCamera instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public Stack<Transform> targetStack;
    public Vector3 offset;
    public Vector3 dialogueOffset;
    private Transform player;
    private Camera camera;

    public float zoomOrtho;
    private float defOrtho;

    private Vector3 crrentVelocity;
    [SerializeField] [Range (0f, 1f)] private float smoothTime;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        defOrtho = camera.orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + offset;
        targetStack = new Stack<Transform>();
    }

    private void OnLevelWasLoaded(int level)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = player.position + offset;
    }
    void FixedUpdate()
    {
        if (targetStack.Count == 0)
        {
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, defOrtho, 20 * Time.deltaTime);
            transform.position = Vector3.SmoothDamp(transform.position, player.position + offset,
                                                    ref crrentVelocity, smoothTime);
        }
        else
        {
            if (targetStack.Peek() != null)
            {
                camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, zoomOrtho, 20 * Time.deltaTime);
                transform.position = Vector3.SmoothDamp(transform.position, targetStack.Peek().position + dialogueOffset,
                                                        ref crrentVelocity, smoothTime);
            }
            else
            {
                targetStack.Pop();
            }
        }

        if (!DialogManager.instance.isInDialogue && targetStack.Count != 0)
        {
            targetStack.Clear();
        }
    }

    public void DialogueZoom(Transform newTarget)
    {
        targetStack.Push(newTarget);
    }
}
