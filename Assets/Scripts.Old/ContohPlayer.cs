// using UnityEngine;
// using System;
// using System.Reflection;

// // Hehe
// // AwokAwok
// public class ContohPlayer : MonoBehaviour {

//     private float health;

//     public void Damage(float damage){
//         heatlth -= damage;
//         GameEvents.OnPlayerHealthChanged?.Invoke(damage);
//     }
    
// }

// public class HealthUI {
//     private void Awake() {
//         GameEvents.OnPlayerHealthChanged += OnHealthChanged;
//         AppDomain domain = AppDomain.CurrentDomain;
//         Assembly[] assemblies = domain.GetAssemblies();
//         foreach (var a in assemblies)
//         {
//             a.GetFiles;
//         }
//     }

// //     void OnHealthChanged(float damage){
// //         text = damage;
// //     }
// // }

// // public interface IObserver{
// //     public void OnEvent(){

// //     }
// // }

// // public class Enemy{
// //     private void Awake()
// //     {
// //         GameEvents.OnPlayerHealthChanged += OnPlayerHealthChanged;
// //     }

// //     void OnPlayerHealthChanged(float damage)
// //     {
// //         if(damage > 400) Run();
// //     }
// // }