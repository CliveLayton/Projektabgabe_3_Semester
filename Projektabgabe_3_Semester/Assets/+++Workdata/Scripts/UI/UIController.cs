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

   private void Awake()
   {
      gameOverScreen.SetActive(false);
      GameLoopManager.onGameStateChange += OnGameStateChanged;
   }

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

   private void OnGameStateChanged(GameLoopManager.GameState newState)
   {
      gameOverScreen.SetActive(newState == GameLoopManager.GameState.GameOver);
   }

   public void StartNewGame()
   {
      GameLoopManager.StartNewGame();
   }

   public void EnterMainMenu()
   {
      GameLoopManager.EnterMainMenu();
   }

   private void OnEnable()
   {
      instance = this;
      text.text = "0 Points";
   }

   public void IncreaseScore(int points)
   {
      if (text == null)
      {
         return;
      }
      score += points;
      text.text = score + " Points";
   }

   private void OnWeaponAdded(Weapon weapon)
   {
      WeaponInventoryEntry newEntry = Instantiate(entryPrefab, inventoryParent);
      newEntry.Initialize(weapon);
      weaponEntries.Add(newEntry);
   }

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

   private void OnActiveWeaponSwitched(Weapon weapon)
   {
      foreach (var entry in weaponEntries)
      {
         entry.SetSelected(entry.weapon == weapon);
      }
   }
}
