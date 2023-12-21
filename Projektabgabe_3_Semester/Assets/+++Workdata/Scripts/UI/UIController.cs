using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

   [SerializeField] private TextMeshProUGUI text;
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private RectTransform inventoryParent;
   [SerializeField] private WeaponInventoryEntry entryPrefab;

   private int score;
   private List<WeaponInventoryEntry> weaponEntries = new List<WeaponInventoryEntry>();

   //turns the game over panel of
   //activates ongamestatechanged function
   private void Awake()
   {
      gameOverScreen.SetActive(false);
      GameLoopManager.onGameStateChange += OnGameStateChanged;
   }

   //gets the player axis controller from player to get and add functions for
   //adding weapon, remove weapon and switch active weapon
   private void Start()
   {
      PlayerAxisController controller = PlayerManager.GetPlayerController();
      controller.onWeaponAdded += OnWeaponAdded;
      controller.onWeaponRemoved += OnWeaponRemoved;
      controller.onActiveWeaponSwitched += OnActiveWeaponSwitched;
      foreach (var weapon in controller.GetInventory())
      {
         OnWeaponAdded(weapon);
      }
      
      OnActiveWeaponSwitched(controller.GetEquippedWeapon());
   }

   //if object get destroyed, deactivate all added functions above
   private void OnDestroy()
   {
      GameLoopManager.onGameStateChange -= OnGameStateChanged;
      
      PlayerAxisController controller = PlayerManager.GetPlayerController();
      if (controller != null)
      {
         controller.onWeaponAdded -= OnWeaponAdded;
         controller.onWeaponRemoved -= OnWeaponRemoved;
         controller.onActiveWeaponSwitched -= OnActiveWeaponSwitched;
      }
   }

   //if gamestate changed to gameover, turns the gameoverscreen on
   private void OnGameStateChanged(GameLoopManager.GameState newState)
   {
      gameOverScreen.SetActive(newState == GameLoopManager.GameState.GameOver);
   }

   /// <summary>
   /// get the function from gameloopmanager to start new game
   /// </summary>
   public void StartNewGame()
   {
      GameLoopManager.StartNewGame();
   }

   /// <summary>
   /// gets the function from gameloopmanager to enter main menu
   /// </summary>
   public void EnterMainMenu()
   {
      GameLoopManager.EnterMainMenu();
   }

   //set instance to this script
   //set the score text to "0 points"
   private void OnEnable()
   {
      instance = this;
      text.text = "0 Points";
   }

   /// <summary>
   /// sets the text to the variable of points
   /// </summary>
   /// <param name="points">int</param>
   public void IncreaseScore(int points)
   {
      if (text == null)
      {
         return;
      }
      score += points;
      text.text = score + " Points";
   }

   //add new weaponinventoryentry to the UI if player collect a new weapon
   private void OnWeaponAdded(Weapon weapon)
   {
      WeaponInventoryEntry newEntry = Instantiate(entryPrefab, inventoryParent);
      newEntry.Initialize(weapon);
      weaponEntries.Add(newEntry);
   }

   //destroy/remove a weaponinventoryentry from the ui if player loose a weapon
   private void OnWeaponRemoved(Weapon weapon)
   {
      for (int index = weaponEntries.Count -1; index >= 0; index--)
      {
         if (weaponEntries[index].weapon == weapon)
         {
            Destroy(weaponEntries[index].gameObject);
            weaponEntries.RemoveAt(index);
         }
      }
   }

   //sets the selected weapon in the inventory to the active weapon in the list
   private void OnActiveWeaponSwitched(Weapon weapon)
   {
      foreach (var entry in weaponEntries)
      {
         entry.SetSelected(entry.weapon == weapon);
      }
   }
}
