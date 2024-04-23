using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public bool InvertMouseY { get; private set; } = true;
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string pause = "Pause";
        
    [Header("Menu stuff")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject hudMenu;
    [SerializeField] GameObject gameDoneMenu;
    [SerializeField] TextMeshProUGUI winTxt;
    [SerializeField] TextMeshProUGUI looseTxt;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction pauseAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; }
    public bool PauseTriggered{get; private set; }

    public static PlayerInputHandler Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        pauseAction = playerControls.FindActionMap(actionMapName).FindAction(pause);

        RegisterInputActions();
    }
    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;

        pauseAction.performed += context => PauseTriggered = true;
        pauseAction.canceled += context => PauseTriggered = false;

    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        pauseAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        pauseAction.Disable();
    }

    public void LockCurser()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void UnlockCurser()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Pause()
    {
        UnlockCurser();
        OnDisable();
        hudMenu.SetActive(false);
        pauseMenu.SetActive(true);
        gameDoneMenu.SetActive(false);
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        LockCurser();
        OnEnable();
        hudMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameDoneMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void GameDone(bool hasWon)
    {
        OnDisable();
        UnlockCurser();
        Time.timeScale = 0f;
        
        if(hasWon)
        {
            winTxt.gameObject.SetActive(true);
            looseTxt.gameObject.SetActive(false);
        }
        else
        {
            looseTxt.gameObject.SetActive(true);
            winTxt.gameObject.SetActive(false);
        }

        hudMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameDoneMenu.SetActive(true);
        //Game done menu on
    }
}
