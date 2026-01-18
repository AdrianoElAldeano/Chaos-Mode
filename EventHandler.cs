using System.Collections.Generic;
using CustomPlayerEffects;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
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
            else if (chance < 70)
            {
                ev.Player.EnableEffect<Vitality>(intensity: 50, duration: 30);
                Hint hintvit = new Hint();
                hintvit.Alignment = HintAlignment.Center;
                hintvit.YCoordinate = 50;
                hintvit.Text = "Has recibido regeneración de vida vor 30 sec.";
                hintvit.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintvit);
            }
            else if (chance < 90)
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
            else if (chance < 100)
            {
                ev.Player.EnableEffect<Invisible>(intensity: 255, duration: 10);
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

            if (chance < 30)
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
                ev.Player.EnableEffect<Hemorrhage>(duration: 8);
                Hint hinthemorrhage = new Hint();
                hinthemorrhage.Alignment = HintAlignment.Center;
                hinthemorrhage.YCoordinate = 50;
                hinthemorrhage.Text = "Tienes una hemorragia por 8 sec.";
                hinthemorrhage.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hinthemorrhage);
            }
            else if (chance < 90)
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
            else if (chance < 100)
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
        }
// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Config 1:1
        {
            if (ev.KnobSetting == Scp914KnobSetting.OneToOne)
            {
                System.Random rnd = new System.Random();
                int chance = rnd.Next(0, 100);

                if (chance < 30)
                {
                    ev.Player.EnableEffect<MovementBoost>(intensity: 15, duration: 180);
                    Hint hintmov2 = new Hint();
                    hintmov2.Alignment = HintAlignment.Center;
                    hintmov2.YCoordinate = 50;
                    hintmov2.Text = "Has recibido un boost de velocidad por 3 min.";
                    hintmov2.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintmov2);
                }
                else if (chance < 70)
                {
                    ev.Player.EnableEffect<RainbowTaste>(intensity: 2, duration: 15);
                    Hint hintraste = new Hint();
                    hintraste.Alignment = HintAlignment.Center;
                    hintraste.YCoordinate = 50;
                    hintraste.Text = "Has recibido el efecto del caramelo arcoíris por 15 sec.";
                    hintraste.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintraste);
                }
                else if (chance < 90)
                {
                    ev.Player.EnableEffect<DamageReduction>(intensity: 3, duration: 25);
                    Hint hintbodyshotReduction = new Hint();
                    hintbodyshotReduction.Alignment = HintAlignment.Center;
                    hintbodyshotReduction.YCoordinate = 50;
                    hintbodyshotReduction.Text = "Tienes una reducción de daño por 25 sec.";
                    hintbodyshotReduction.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintbodyshotReduction);
                }
                else if (chance < 100)
                {
                    ev.Player.EnableEffect<Bleeding>(intensity: 2, duration: 8);
                    Hint hintBleeding = new Hint();
                    hintBleeding.Alignment = HintAlignment.Center;
                    hintBleeding.YCoordinate = 50;
                    hintBleeding.Text = "Estas sangrando por 8 sec.";
                    hintBleeding.HideAfter(8f);

                    PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                    playerDisplay.AddHint(hintBleeding);
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
                ev.Player.EnableEffect<Blindness>(intensity: 135, duration: 20);
                Hint hintblind = new Hint();
                hintblind.Alignment = HintAlignment.Center;
                hintblind.YCoordinate = 50;
                hintblind.Text = "Se te ha oscurecido la vista por 20 sec.";
                hintblind.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintblind);
            }
            else if (chance < 70)
            {
                ev.Player.EnableEffect<CardiacArrest>(intensity: 1, duration: 8);
                Hint hintcardiac = new Hint();
                hintcardiac.Alignment = HintAlignment.Center;
                hintcardiac.YCoordinate = 50;
                hintcardiac.Text = "Has sufrido un infarto por 8 sec.";
                hintcardiac.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintcardiac);
            }
            else if (chance < 100)
            {
                ev.Player.EnableEffect<Scp207>(intensity: 2, duration: 300);
                Hint hintscp207 = new Hint();
                hintscp207.Alignment = HintAlignment.Center;
                hintscp207.YCoordinate = 50;
                hintscp207.Text = "Has el efecto de un 207 por 5 min.";
                hintscp207.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintscp207);
            }
        }
// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Config Rough
        if (ev.KnobSetting == Scp914KnobSetting.Rough)
        {
            System.Random rnd = new System.Random();
            int chance = rnd.Next(0, 100);

            if (chance < 30)
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

            if (chance < 70)
            {
                ev.Player.EnableEffect<Hypothermia>(intensity: 3,duration: 10);
                Hint hinthypothermia = new Hint();
                hinthypothermia.Alignment = HintAlignment.Center;
                hinthypothermia.YCoordinate = 50;
                hinthypothermia.Text = "Te entro hipotermia por 10 sec.";
                hinthypothermia.HideAfter(8f);
                
                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hinthypothermia);
            }

            if (chance < 100)
            {
                ev.Player.Scale = new Vector3(0.75f, 0.75f, 0.75f);
                Hint hintSize = new Hint();
                hintSize.Alignment = HintAlignment.Center;
                hintSize.YCoordinate = 50;
                hintSize.Text = "Has sido reducido de tamaño por 1 min.";
                hintSize.HideAfter(8f);

                PlayerDisplay playerDisplay = PlayerDisplay.Get(ev.Player);
                playerDisplay.AddHint(hintSize);

                Timing.RunCoroutine(ResetSizeCoroutine(ev.Player, 60f));
            }
        }
    }
    private IEnumerator<float> ResetSizeCoroutine(Player player, float delay)
    {
        yield return Timing.WaitForSeconds(delay);

        if (player != null && player.GameObject != null)
        {
            player.Scale = new Vector3(1, 1, 1);
        }
    }
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Fin del plugin 914
}
        