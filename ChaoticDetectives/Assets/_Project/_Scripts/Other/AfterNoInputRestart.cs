using UnityEngine;

public class AfterNoInputRestart : MonoBehaviour
{
    // 5 minutes
    [SerializeField] private float _timeToRestart = 300f;
    [SerializeField] private AbstractInput _input;

    private float _timeSinceLastInput = 0f;

    private bool _isCountingDown = false;

    private void OnEnable()
    {
        _input.OnDirectionclamped += OnDirectionClamped;
        _input.OnClickDown += OnClickDown;
        _input.OnClickUp += OnClickUp;
    }

    private void OnDisable()
    {
        _input.OnDirectionclamped -= OnDirectionClamped;
        _input.OnClickDown -= OnClickDown;
        _input.OnClickUp -= OnClickUp;
    }

    private void Update()
    {
        if (!_isCountingDown) { return; }

        _timeSinceLastInput += Time.deltaTime;
        if (_timeSinceLastInput >= _timeToRestart)
        {
            Restart();
        }
    }


    private void OnDirectionClamped(Vector2 direction)
    {
        _timeSinceLastInput = 0f;
    }

    private void OnClickDown()
    {
        _timeSinceLastInput = 0f;
    }

    private void OnClickUp()
    {
        _timeSinceLastInput = 0f;
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void CountingDown(bool isCountingDown)
    {
        _isCountingDown = isCountingDown;

        _timeSinceLastInput = 0f;
    }
}