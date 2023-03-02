using System;

using UnityEngine;


public class Wallet : MonoBehaviour
{
    // public static Wallet Instance { get; private set; }
    // public static event Action OnBalanceChanged;
    //
    // [SerializeField] private float totalBalance;
    //
    // private void Awake()
    // {
    //     if(Instance == null)
    //         Instance = this;
    //     else
    //         Destroy(gameObject);
    // }
    //
    // private void Start()
    // {
    //     LoadBalance();
    // }
    //
    // public float GetBalance() => totalBalance;
    //
    // public void AddBalance(float balance)
    // {
    //     totalBalance += balance;
    //     SaveBalance();
    // }
    //
    // public void RemoveBalance(float balance)
    // {
    //     totalBalance -= balance;
    //     SaveBalance();
    // }
    //
    // private void SaveBalance()
    // { 
    //     PlayerPrefs.SetFloat(Constants.walletBalancePrefsKey, totalBalance);
    //     OnBalanceChanged?.Invoke();
    // }
    //
    // private void LoadBalance()
    // {
    //     if(PlayerPrefs.HasKey(Constants.walletBalancePrefsKey))
    //     {
    //         totalBalance = PlayerPrefs.GetFloat(Constants.walletBalancePrefsKey);
    //         OnBalanceChanged?.Invoke();
    //     }
    //     else
    //     {
    //         totalBalance = Constants.walletBalanceStartingDefault;
    //         SaveBalance();
    //     }
    // }
}
