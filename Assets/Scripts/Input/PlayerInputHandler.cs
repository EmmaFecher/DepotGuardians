using TMPro;
using Unity.VisualScripting;
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
        
    [Header("Menu holder stuff")]
    [SerializeField] TextMeshProUGUI winTxt;
    [SerializeField] TextMeshProUGUI looseTxt;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject hudMenu;
    [SerializeField] GameObject gameDoneMenu;
    [SerializeField] GameObject optionForTowerBuildingMenu;
    [SerializeField] GameObject towerBuildSelectionMenu;



    GameObject currentPlatform;

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
        FindActionsmaps();
        RegisterInputActions();
    }
    private void Start() 
    {
        FindActionsmaps();
        RegisterInputActions();
        GetCanvases();
    }
    private void Update() 
    {
        bool isALevel = GameManager.Instance.SceneIsALevelScene();
        if(isALevel)
        {
            // OnEnable();
        }
        else
        {
            // OnDisable();
            pauseMenu.GetComponent<Canvas>().enabled = false;
            hudMenu.GetComponent<Canvas>().enabled = false;
            gameDoneMenu.GetComponent<Canvas>().enabled = false;
            optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = false;
            towerBuildSelectionMenu.GetComponent<Canvas>().enabled = false;
            winTxt.gameObject.SetActive(false);
            looseTxt.gameObject.SetActive(false);
        }
    }
    private void GetCanvases()
    {
        pauseMenu = GameObject.Find("PauseCanvas");
        hudMenu = GameObject.Find("HUDCanvas");
        gameDoneMenu = GameObject.Find("GameDoneCanvas");
        optionForTowerBuildingMenu = GameObject.Find("OptionToBuildTowerCanvas");
        towerBuildSelectionMenu = GameObject.Find("TowerChoosingCanvas");
        winTxt = gameDoneMenu.transform.Find("GameDoneMenuImage/GameDoneWinTxt").GetComponent<TextMeshProUGUI>();
        looseTxt = gameDoneMenu.transform.Find("GameDoneMenuImage/GameDoneLooseTxt").GetComponent<TextMeshProUGUI>();
    }
    private void FindActionsmaps()
    {
        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        pauseAction = playerControls.FindActionMap(actionMapName).FindAction(pause);
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
        Time.timeScale = 0f;
        UnlockCurser();
        OnDisable();
        hudMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.GetComponent<Canvas>().enabled = true;
        gameDoneMenu.GetComponent<Canvas>().enabled = false;
        optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = false;
    }
    public void UnPause()
    {
        LockCurser();
        OnEnable();
        hudMenu.GetComponent<Canvas>().enabled = true;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        gameDoneMenu.GetComponent<Canvas>().enabled = false;
        towerBuildSelectionMenu.GetComponent<Canvas>().enabled = false;
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

        hudMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        gameDoneMenu.GetComponent<Canvas>().enabled = true;
        optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = false;
    }
    
    public void OpenTowerSelection()
    {
        Time.timeScale = 0f;
        UnlockCurser();
        OnDisable();
        hudMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        gameDoneMenu.GetComponent<Canvas>().enabled = false;
        optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = false;
        towerBuildSelectionMenu.GetComponent<Canvas>().enabled = true;
    }
    public void GetCurrentPlatform()
    {
        towerBuildSelectionMenu.GetComponent<TowerBuildingUI>().SetTower(currentPlatform);
    }
    public bool CheckIfBuildScreenOptionIsActive(){
        return optionForTowerBuildingMenu.activeInHierarchy;
    }
    public void OpenOptionForBuildingScreen(GameObject whichPlatform)
    {
        currentPlatform = whichPlatform;
        optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = true;
    }
    public void CloseOptionForBuildingScreen()
    {
        optionForTowerBuildingMenu.GetComponent<Canvas>().enabled = false;
    }
}
