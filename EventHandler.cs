using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using InventorySystem.Items.MarshmallowMan;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using Scp914;
using UnityEngine;

namespace Chaos_Mode;

public class EventHandler : CustomEventsHandler
{
    public override void OnScp914ProcessedPlayer(Scp914ProcessedPlayerEventArgs ev)
    {
        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Config Fine
        if (ev.KnobSetting == Scp914KnobSetting.Fine)
        {
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 100);

            if (chance < 30)
            {
                ev.Player.EnableEffect<MovementBoost>(intensity: 30, duration: 120);
                Hint hintmov = new Hint();
                hintmov.Alignment = HintAlignment.Center;
                hintmov.YCoordinate = 50;
                hintmov.Text = "Has recibido un boost de velocidad por 2 min.";
                hintmov.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintmov);
            }
          
            else if (chance < 60)
            {
                ev.Player.EnableEffect<Slowness>(intensity: 30, duration: 45);
                Hint hintslow = new Hint();
                hintslow.Alignment = HintAlignment.Center;
                hintslow.YCoordinate = 50;
                hintslow.Text = "Has recibido lentitud por 45 sec.";
                hintslow.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintslow);
            }
            else if (chance < 80)
            {
                ev.Player.EnableEffect<Fade>(intensity: 255, duration: 10);
                Hint hintinvisible = new Hint();
                hintinvisible.Alignment = HintAlignment.Center;
                hintinvisible.YCoordinate = 50;
                hintinvisible.Text = "Has recibido invisibilidad por 10 sec.";
                hintinvisible.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintinvisible);
            }
        }
    }

/// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Config Very Fine
    public override void OnScp914ProcessingPlayer(Scp914ProcessingPlayerEventArgs ev)
    {
        if (ev.KnobSetting == Scp914KnobSetting.VeryFine)
        {
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 100);

            if (chance < 45)
            {
                ev.Player.EnableEffect<Invigorated>(intensity: 255, duration: 20);
                Hint hintinv = new Hint();
                hintinv.Alignment = HintAlignment.Center;
                hintinv.YCoordinate = 50;
                hintinv.Text = "Has recibido el efecto de vigorizado por 20 sec.";
                hintinv.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintinv);
            }
            else if (chance < 70)
            {
                ev.Player.EnableEffect<CardiacArrest>(intensity: 1, duration: 5);
                Hint hintcardiac5 = new Hint();
                hintcardiac5.Alignment = HintAlignment.Center;
                hintcardiac5.YCoordinate = 50;
                hintcardiac5.Text = "Has sufrido un infarto por 5 sec.";
                hintcardiac5.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintcardiac5);
            }
            else if (chance < 80)
            {
                ev.Player.SetRole(RoleTypeId.Scp0492);
                Hint hintzombie = new Hint();
                hintzombie.Alignment = HintAlignment.Center;
                hintzombie.YCoordinate = 50;
                hintzombie.Text = "Has sido convertido en zombie.";
                hintzombie.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintzombie);
            }
            else if (chance < 85)
            {
                ev.Player.EnableEffect<Ghostly>(intensity: 255, duration: 8);
                Hint hintghostly = new Hint();
                hintghostly.Alignment = HintAlignment.Center;
                hintghostly.YCoordinate = 50;
                hintghostly.Text = "Puedes atravesar puertas por 8 sec.";
                hintghostly.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintghostly);
            }
            else if (chance < 90)
            {
                Timing.RunCoroutine(RutinaRegeneracion(ev.Player));
                
                Hint hintCuracion = new Hint();
                hintCuracion.Alignment = HintAlignment.Center;
                hintCuracion.YCoordinate = 50;
                hintCuracion.Text = "Has recibido regeneración pasiva (15 HP cada 8s)";
                hintCuracion.HideAfter(8f);
                
                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintCuracion);
            }
            IEnumerator<float> RutinaRegeneracion(Player player)
            {
                while (true)
                {
                    yield return Timing.WaitForSeconds(8f);
                    
                    if (player == null || !player.IsAlive)
                        yield break;
                    
                    if (player.Health < player.MaxHealth)
                    {
                        float nuevaVida = player.Health + 15;
                        
                        if (nuevaVida > player.MaxHealth)
                        {
                            nuevaVida = player.MaxHealth;
                        }
                        player.Health = nuevaVida;
                    }
                }
            }
        }
// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Config 1:1
        {
            if (ev.KnobSetting == Scp914KnobSetting.OneToOne)
            {
                System.Random rnd = new System.Random();
                int chance = rnd.Next(0, 100);

                if (chance < 19)
                { 
                    Player player = ev.Player;

                    if (player.IsAlive)
                    {
                        player.ClearInventory();
                        
                        for (int i = 0; i < 8; i++)
                        {
                            player.AddItem(ItemType.GunA7);
                        }
                        Hint hintA7 = new Hint();
                        hintA7.Alignment = HintAlignment.Center;
                        hintA7.YCoordinate = 50;
                        hintA7.Text = "Todo tu inventario fue reemplazado por 8 A7";
                        hintA7.HideAfter(8f);
                        
                        PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                        playerDisplay.AddHint(hintA7);
                    }
                }
                else if (chance < 39)
                {
                    Timing.RunCoroutine(RutinaManosMantequilla(ev.Player));
                    
                    Hint hintManosMantequilla = new Hint();
                    hintManosMantequilla.Alignment = HintAlignment.Center;
                    hintManosMantequilla.YCoordinate = 50;
                    hintManosMantequilla.Text = "Has sido maldecido, por 10 min cada 30 segundos lo que tengas en las manos lo tiraras.";
                    hintManosMantequilla.HideAfter(10f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintManosMantequilla);
                }
                
                else if (chance < 79)
                {
                    ev.Player.EnableEffect<DamageReduction>(intensity: 50, duration: 30);
                    Hint hintbodyshotReduction = new Hint();
                    hintbodyshotReduction.Alignment = HintAlignment.Center;
                    hintbodyshotReduction.YCoordinate = 50;
                    hintbodyshotReduction.Text = "Tienes una reducción de daño por 30 sec.";
                    hintbodyshotReduction.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintbodyshotReduction);
                }
               
                else if (chance < 80)
                {
                    int id = ev.Player.PlayerId;
                    
                    Server.RunCommand($"rocket {id} 1");
                       
                        Hint hintRocket = new Hint();
                        hintRocket.Alignment = HintAlignment.Center;
                        hintRocket.YCoordinate = 50;
                        hintRocket.Text = "¡Hasta luego!";
                        hintRocket.HideAfter(4f);
                        
                        PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                        playerDisplay.AddHint(hintRocket);
                }
                 else if (chance < 100)
                {
                    Timing.RunCoroutine(RutinaRegalarItems(ev.Player));
                    
                    Hint hintRegalarItems = new Hint();
                    hintRegalarItems.Alignment = HintAlignment.Center;
                    hintRegalarItems.YCoordinate = 50;
                    hintRegalarItems.Text = "¡Has sido bendecido! Recibirás objetos cada 20s por 5 min.";
                    hintRegalarItems.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintRegalarItems);
                }
                IEnumerator<float> RutinaRegalarItems(Player player)
                {
                    List<ItemType> objetosValidos = new List<ItemType>()
                    {
                        // --- Tarjetas ---
                        ItemType.KeycardJanitor, ItemType.KeycardScientist, ItemType.KeycardResearchCoordinator,
                        ItemType.KeycardZoneManager, ItemType.KeycardGuard, ItemType.KeycardMTFPrivate,
                        ItemType.KeycardMTFOperative, ItemType.KeycardMTFCaptain, ItemType.KeycardFacilityManager,
                        ItemType.KeycardChaosInsurgency, ItemType.KeycardO5,

                        // --- Curas ---
                        ItemType.Medkit, ItemType.Painkillers, ItemType.Adrenaline, ItemType.SCP500,

                        // --- Varios ---
                        ItemType.Radio, ItemType.Flashlight, ItemType.Coin,

                        // --- Armaduras ---
                        ItemType.ArmorLight, ItemType.ArmorCombat, ItemType.ArmorHeavy,

                        // --- Armas ---
                        ItemType.GunCOM15, ItemType.GunCOM18, ItemType.GunFSP9, ItemType.GunCrossvec,
                        ItemType.GunE11SR, ItemType.GunRevolver, ItemType.GunAK, ItemType.GunShotgun, 
                        ItemType.GunLogicer, ItemType.ParticleDisruptor, ItemType.Jailbird, ItemType.MicroHID,

                        // --- Granadas ---
                        ItemType.GrenadeHE, ItemType.GrenadeFlash,
                        
                        // --- Scps ---
                        ItemType.SCP018, ItemType.SCP207, ItemType.SCP1344, ItemType.SCP330, ItemType.SCP268,
                        ItemType.AntiSCP207, ItemType.SCP500, ItemType.SCP2176
                    };

                    for (int i = 0; i < 15; i++)
                    {
                        yield return Timing.WaitForSeconds(20f);

                        if (player == null || !player.IsAlive)
                            yield break;

                        if (player.Items.Count() < 8)
                        {
                            ItemType randomItem = objetosValidos[UnityEngine.Random.Range(0, objetosValidos.Count)];

                            
                            
                                player.AddItem(randomItem);
                                
                                Hint hintItemRecibido = new Hint();
                                hintItemRecibido.Alignment = HintAlignment.Center;
                                hintItemRecibido.YCoordinate = 50;
                                hintItemRecibido.Text = $"Has recibido: {randomItem}";
                                hintItemRecibido.HideAfter(3f);
                                
                                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                                playerDisplay.AddHint(hintItemRecibido);
                            
                        }
                        else
                        {
                            Hint hintItemPerdido = new Hint();
                            hintItemPerdido.Alignment = HintAlignment.Center;
                            hintItemPerdido.YCoordinate = 50;
                            hintItemPerdido.Text = "Inventario lleno, perdiste el regalo.";
                            hintItemPerdido.HideAfter(3f);
                            
                            PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                            playerDisplay.AddHint(hintItemPerdido);
                        }
                    }
                }
            }
        }
        // /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Config Coarse
        if (ev.KnobSetting == Scp914KnobSetting.Coarse)
        {
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 100);
            if (chance < 30)
            {
                ev.Player.EnableEffect<Flashed>(intensity: 255, duration: 15);
                Hint hintblind = new Hint();
                hintblind.Alignment = HintAlignment.Center;
                hintblind.YCoordinate = 50;
                hintblind.Text = "Has sido cegado 15 sec.";
                hintblind.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintblind);
            }
            else if (chance < 40)
            {
                ev.Player.SetRole(RoleTypeId.Scp0492);
                Hint hintzombie = new Hint();
                hintzombie.Alignment = HintAlignment.Center;
                hintzombie.YCoordinate = 50;
                hintzombie.Text = "Has sido convertido en zombie.";
                hintzombie.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintzombie);
            }
            else if (chance < 50)
            {
                Player player = ev.Player;

                if (player.IsAlive)
                {
                    player.MaxHealth = 300;
                    
                    player.Health = 300;
                    
                    Hint hintHealtMax =  new Hint();
                    hintHealtMax.Alignment = HintAlignment.Center;
                    hintHealtMax.YCoordinate = 50;
                    hintHealtMax.Text = "Se te ha puesto como maximo de vida 300.";
                    hintHealtMax.HideAfter(8f);
                    
                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintHealtMax);
                }
            }
            else if (chance < 70)
            {
                ev.Player.EnableEffect<MovementBoost>(intensity: 45, duration: 120);
                Hint hintmov2 = new Hint();
                hintmov2.Alignment = HintAlignment.Center;
                hintmov2.YCoordinate = 50;
                hintmov2.Text = "Has recibido un boost de velocidad por 2 min.";
                hintmov2.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintmov2);
            }
            else if (chance < 75)
            {
                Timing.RunCoroutine(TransformarScp(ev.Player));
            }
            IEnumerator<float> TransformarScp(Player player)
            {
                yield return Timing.WaitForSeconds(0.5f);
                
                if (player != null && player.IsAlive)
                {
                    List<RoleTypeId> validScps = new List<RoleTypeId>()
                    {
                        RoleTypeId.Scp173,
                        RoleTypeId.Scp049,
                        RoleTypeId.Scp096,
                        RoleTypeId.Scp106,
                        RoleTypeId.Scp939,
                        RoleTypeId.Scp3114
                    };

                    RoleTypeId randomScp = validScps[UnityEngine.Random.Range(0, validScps.Count)];
                   
                    Vector3 posicionActual = player.Position;
                    player.SetRole(randomScp);
                    
                    Timing.CallDelayed(0.1f, () => {
                        player.Position = posicionActual;
                    });
                    
                    player.SetRole(randomScp);
                    
                    Hint hintRandomSCP = new Hint();
                    hintRandomSCP.Alignment = HintAlignment.Center;
                    hintRandomSCP.YCoordinate = 50;
                    hintRandomSCP.Text = $"Has convertido en {randomScp}.";
                    hintRandomSCP.HideAfter(8f);
                    
                    yield return Timing.WaitForSeconds(0.1f); 
                }
            }
        }
// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Config Rough
        if (ev.KnobSetting == Scp914KnobSetting.Rough)
        {
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 100);

            if (chance < 40)
            {
                ev.Player.EnableEffect<AntiScp207>(intensity: 2, 300);
                Hint hintantiscp207 = new Hint();
                hintantiscp207.Alignment = HintAlignment.Center;
                hintantiscp207.YCoordinate = 50;
                hintantiscp207.Text = "Has recibido el efecto de la anti cola por 5 min.";
                hintantiscp207.HideAfter(8f);
                
                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintantiscp207);
            }

            if (chance < 80)
            {
                ev.Player.EnableEffect<Fade>(intensity: 255,duration: 30);
                Hint hintFade = new Hint();
                hintFade.Alignment = HintAlignment.Center;
                hintFade.YCoordinate = 50;
                hintFade.Text = "Tienes invisibilidad por 30 sec.";
                hintFade.HideAfter(8f);
                
                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintFade);
            }

            if (chance < 100)
            {
                Player player = ev.Player;

                if (player.IsAlive)
                {
                    float currentHealth = player.Health;
                    float newHealth = currentHealth / 2f;

                    player.Health = newHealth;

                    try
                    {
                        ev.Player.EnableEffect<Burned>(intensity: 255, duration: 2f);

                        Hint hintHalfLife = new Hint();
                        hintHalfLife.Alignment = HintAlignment.Center;
                        hintHalfLife.YCoordinate = 50;
                        hintHalfLife.Text = "Se te redució la vida por la mitad.";
                        hintHalfLife.HideAfter(8f);

                        PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                        playerDisplay.AddHint(hintHalfLife);
                    }
                    catch (System.Exception ex)
                    {
                        LabApi.Features.Console.Logger.Error($"Error en 914 Rough Hint: {ex}");
                    }
                }
            }
        }
        IEnumerator<float> RutinaManosMantequilla(Player player)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return Timing.WaitForSeconds(30f);

                if (player == null || !player.IsAlive)
                    yield break;
                
                if (player.CurrentItem != null)
                {
                    player.DropItem(player.CurrentItem);
                            
                    Hint hintTirarObjeto = new Hint();
                    hintTirarObjeto.Alignment = HintAlignment.Center;
                    hintTirarObjeto.YCoordinate = 50;
                    hintTirarObjeto.Text = "Se te ha resbalado tu objeto";
                    hintTirarObjeto.HideAfter(3f);
                            
                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintTirarObjeto);
                }
            }
        }
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Fin del plugin 914
}
        