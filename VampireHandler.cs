using System.Collections.Generic;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using UnityEngine;

namespace Chaos_Mode;

public class VampireHandler : CustomEventsHandler
{
    private const float StandardHeal = 20f;
    
    private Dictionary<int, float> _pendingHealDisplay = new Dictionary<int, float>();
    private Dictionary<int, CoroutineHandle> _hintCoroutines = new Dictionary<int, CoroutineHandle>();
    
    public override void OnPlayerDying(PlayerDyingEventArgs ev)
    {
        if (ev.Attacker != null && ev.Player != null && ev.Attacker != ev.Player && ev.Attacker.IsAlive)
        {
            float currentHealth = ev.Attacker.Health;
            float maxHealth = ev.Attacker.MaxHealth;
            
            if (currentHealth >= maxHealth) return;
            
            float missingHealth = maxHealth - currentHealth;
            
            float realHealAmount = (missingHealth < StandardHeal) ? missingHealth : StandardHeal;
            
            ev.Attacker.Heal(realHealAmount);
            
            if (realHealAmount < 0.1f) return;

            int attackerId = ev.Attacker.PlayerId;
            
            if (_pendingHealDisplay.ContainsKey(attackerId))
            {
                _pendingHealDisplay[attackerId] += realHealAmount;
            }
            else
            {
                _pendingHealDisplay[attackerId] = realHealAmount;
            }
            
            if (_hintCoroutines.ContainsKey(attackerId))
            {
                Timing.KillCoroutines(_hintCoroutines[attackerId]);
            }

            _hintCoroutines[attackerId] = Timing.CallDelayed(0.25f, () => 
            {
                SendAccumulatedHint(ev.Attacker);
            });
        }
    }
    private void SendAccumulatedHint(Player attacker)
    {
        if (attacker == null || !_pendingHealDisplay.ContainsKey(attacker.PlayerId)) return;
        
        try
        {
            float rawTotal = _pendingHealDisplay[attacker.PlayerId];
            int displayTotal = Mathf.RoundToInt(rawTotal);
            
            _pendingHealDisplay.Remove(attacker.PlayerId);
            _hintCoroutines.Remove(attacker.PlayerId);
            
            if (displayTotal <= 0) return;
            
            Hint vampHint = new Hint();
            vampHint.Alignment = HintAlignment.Center;
            vampHint.YCoordinate = 50; 
            vampHint.Text = $"<size=25>Has absorbido <color=#00ff00>+{displayTotal} HP</color> de tu víctima.</size>";
            vampHint.HideAfter(4f);   
            
            PlayerDisplay display = PlayerDisplay.Get(attacker);
            if (display != null)
            {
                display.AddHint(vampHint);
            }
        }
        catch (System.Exception ex)
        {
            LabApi.Features.Console.Logger.Error($"Error enviando Vampire Hint: {ex.Message}");
        }
    }
}