using System.Collections.Generic;
using System.Linq;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace Chaos_Mode;

public class AnomalyHandler : CustomEventsHandler
{
    private CoroutineHandle _anomalyCoroutine;

    public static AnomalyHandler Instance { get; private set; }

    public bool IsPaused { get; set; } = false;
    
    private bool _activeSizeAnomaly = false;

    public AnomalyHandler()
    {
        Instance = this;
    }

    public override void OnServerRoundStarted()

    {
        LabApi.Features.Console.Logger.Info("Los eventos anomalos se estan iniciando.");

        Timing.KillCoroutines(_anomalyCoroutine);
        
        _activeSizeAnomaly = false;

        _anomalyCoroutine = Timing.RunCoroutine(AnomalyCycle());
    }

    public override void OnPlayerDying(PlayerDyingEventArgs ev)
    {
        if (_activeSizeAnomaly)
        {
            ev.Player.Scale = Vector3.one;
        }
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Config tiempo anomalias
    private IEnumerator<float> AnomalyCycle()
    {
        // Espera entre 45sec y 1.30minutos para el primer evento
        float firstWait = UnityEngine.Random.Range(45f, 90f);

        yield return Timing.WaitForSeconds(firstWait);

        while (true)
        {
            if (IsPaused)
            {
                yield return Timing.WaitForSeconds(1f);
                continue;
            }

            TriggerRandomAnomaly(-1);

            // Espera de 2 a 3.30 minutos para la siguiente anomalia (después de la primera anomalia)
            float nextWait = UnityEngine.Random.Range(120f, 210f);

            yield return Timing.WaitForSeconds(nextWait);
        }
    }

// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Selector de Anomalías 
    public void TriggerRandomAnomaly(int forceId = -1)
    {
        int pick = forceId;
        // Cambiar numeros entre parentesis dependiendo de los eventos que tenga, ej: 10 eventos = (0, 10)
        if (pick == -1)
        {
            pick = UnityEngine.Random.Range(0, 8);
        }

        switch (pick)
        {
            // --- ANOMALÍA 1: FANTASMA ---
            case 0:
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando anomalía FANTASMA.");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 physics systems failure detected.",
                    "¡Falla en los sistemas de física!",
                    playBackground: true
                );
                Timing.RunCoroutine(GhostlyAnomaly(45f));
                break;

            case 1:
                // --- ANOMALÍA 2: SUPERVELOCIDAD ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando anomalía de VELOCIDAD.");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 attention . biological hazard detected . adrenaline level critical",
                    "Atención, peligro biológico detectado, nivel de adrenalina crítico.",
                    playBackground: true
                );
                Timing.RunCoroutine(SpeedAnomaly(120f));
                break;

            case 2:
                // --- ANOMALÍA 3: APAGON ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando anomalía de APAGON");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 facility power system failure detected",
                    "Falla detectada en el sistema de energía de la instalación.",
                    playBackground: true
                );
                Timing.RunCoroutine(BlackoutAnomaly(60f));
                break;

            case 3:
                // --- ANOMALÍA 4: DISCOTECA ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando anomalía de HACKEO DE LUCES");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_0.9 system breach detected in light control protocols",
                    "Brecha del sistema detectada en los protocolos de control de luces.",
                    playBackground: true
                );

                Timing.RunCoroutine(DiscoAnomaly(60f));
                break;

            case 4:
                // --- ANOMALÍA 5: ENANOS ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando Anomalía de ENANOS");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 attention . biological hazard detected. all human and scp will be smaller",
                    "Atención, peligro biológico detectado. Todos los humanos y scp serán más pequeños.",
                    playBackground: true
                );

                Timing.RunCoroutine(SmallAnomaly(duration: 90f));
                break;

            case 5:
                // --- ANOMALÍA 6: GIGANTES ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando Anomalía de GIGANTES");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 attention . biological hazard detected. all human and scp will be bigger",
                    "Atención, peligro biológico detectado. Todos los humanos y scp serán más grandes.",
                    playBackground: true
                );

                Timing.RunCoroutine(GigantAnomaly(duration: 90f));
                break;

            case 6:
                // --- ANOMALÍA 7: PUERTAS LOCAS ---
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando Anomalía PUERTAS LOCAS");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 attention . door control system failure detected",
                    "Atención, se detectó una falla en el sistema de control de la puerta",
                    playBackground: true
                );

                Timing.RunCoroutine(CrazyDoorsAnomaly(duration: 60f));
                break;

            case 7:
                // --- ANOMALÍA 8: TELETRANSPORTE ---   
                LabApi.Features.Console.Logger.Info("Chaos Mode: Iniciando Anomalía TELETRANSPORTE");

                Announcer.Message(
                    "pitch_0.2 .g4 .g4 pitch_1.0 Attention . unstable spatial coordinates",
                    "Atención, coordenadas espaciales inestables",
                    playBackground: true
                );

                Timing.RunCoroutine(SwapAnomaly(duration: 60f));
                break;
        }
    }
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // LÓGICA DE LAS ANOMALÍAS INDIVIDUALES

    // Anomalía de Ghostly
    private IEnumerator<float> GhostlyAnomaly(float duration)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            foreach (Player p in Player.List)
            {
                if (p.IsAlive)
                {

                    p.EnableEffect<CustomPlayerEffects.Ghostly>(1, 5f);
                }
            }

            yield return Timing.WaitForSeconds(1f);
            timePassed += 1f;
        }

        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 Stabilized physics systems.",
            "Sistemas de física estabilizados.",
            playBackground: true
        );
        yield return Timing.WaitForSeconds(2f);
        foreach (Player p in Player.List)
        {
            if (p.IsAlive) p.DisableEffect<CustomPlayerEffects.Ghostly>();
        }

        LabApi.Features.Console.Logger.Info("Chaos Mode: Anomalia FANTASMA finalizado.");
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía de Velocidad
    private IEnumerator<float> SpeedAnomaly(float duration)
    {
        float timePassed = 0f;

        // Cuando pasa el tiempo menos duracion tendra la gente que haga spawn
        while (timePassed < duration)
        {
            foreach (Player p in Player.List)
            {
                if (p.IsAlive)
                {
                    // Aquí se da el efecto a los jugadores, el timePassed es para los que hagan spawn en mitad del evento pues se les ponga el efecto, pero con la duracion restante del evento. (Para que no tengan velocidad cuando se termine el evento.)
                    p.EnableEffect<CustomPlayerEffects.MovementBoost>(30, 5f);
                }
            }

            yield return Timing.WaitForSeconds(1f);
            // Aquí añade el tiempo para calcular cuanto tiempo de efecto tiene que ponerle a los que hagan spawn.
            timePassed += 1f;
        }

        yield return Timing.WaitForSeconds(2f);

        foreach (Player p in Player.List)
        {
            if (p.IsAlive)
            {
                p.DisableEffect<CustomPlayerEffects.MovementBoost>();
            }
        }

        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 The adrenaline effect has end.",
            "El efecto de la adrenalina ha terminado.",
            playBackground: true
        );
        LabApi.Features.Console.Logger.Info("Chaos Mode: Anomalia VELOCIDAD finalizado.");
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía de APAGON (BLACKOUT)
    private IEnumerator<float> BlackoutAnomaly(float duration)
    {
        Map.TurnOffLights(60f);

        yield return Timing.WaitForSeconds(duration);

        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 facility power system stabilized",
            "Sistema de energía de la instalación estabilizado.",
            playBackground: true
        );
        LabApi.Features.Console.Logger.Info("Chaos Mode: Anomalia APAGON finalizado.");
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía HACKEO DE LUCES (DISCOTECA)
    private IEnumerator<float> DiscoAnomaly(float duration)
    {
        float timePassed = 0f;
        float interval = 0.5f;

        while (timePassed < duration)
        {
            foreach (Room room in Room.List)
            {
                if (room.LightController != null)
                {
                    Color randomColor = new Color(
                        UnityEngine.Random.value,
                        UnityEngine.Random.value,
                        UnityEngine.Random.value
                    );
                    room.LightController.OverrideLightsColor = randomColor;
                }
            }

            yield return Timing.WaitForSeconds(interval);
            timePassed += interval;
        }

        foreach (Room room in Room.List)
        {
            if (room.LightController != null)
            {
                room.LightController.OverrideLightsColor = Color.clear;
            }
        }

        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 light control protocols restored",
            "Protocolos de control de luces restaurados.",
            playBackground: true
        );
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía ENANOS
    private IEnumerator<float> SmallAnomaly(float duration)
    {
        _activeSizeAnomaly = true;
        
        float timePassed = 0f;
        float checkInterval = 0.5f;
        Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);

        while (timePassed < duration)
        {
            foreach (Player player in Player.List)
            {
                try
                {
                    if (player.IsAlive)
                    {
                        if (Vector3.Distance(player.Scale, smallScale) > 0.05f)
                        {
                            player.Scale = smallScale;
                        }
                    }
                }
                catch (System.Exception)
                {
                    continue;
                }
            }

            yield return Timing.WaitForSeconds(checkInterval);
            timePassed += checkInterval;
        }
        
        _activeSizeAnomaly = false;

        foreach (Player player in Player.List)
        {
            try
            {
                if (player.IsPlayer)
                {
                    player.Scale = Vector3.one;
                }
            }
            catch
            {
            }
        }

        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 all human and scp are now normal height",
            "Todos los humanos y scp ahora tienen una altura normal",
            playBackground: true
        );
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía GIGANTES
        private IEnumerator<float> GigantAnomaly(float duration)
        {
            _activeSizeAnomaly = true;
            
            float timePassed = 0f;
            float checkInterval = 0.5f;
            Vector3 gigantScale = new Vector3(1.15f, 1.15f, 1.15f);

            while (timePassed < duration)
            {
                foreach (Player player in Player.List)
                {
                    try
                    {
                        if (player.IsAlive)
                        {
                            if (Vector3.Distance(player.Scale, gigantScale) > 0.05f)
                            {
                                player.Scale = gigantScale;
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                }

                yield return Timing.WaitForSeconds(checkInterval);
                timePassed += checkInterval;
            }
            
            _activeSizeAnomaly = false;
            
            foreach (Player player in Player.List)
            {
                try
                {
                    if(player.IsPlayer) 
                    {
                        player.Scale = Vector3.one;
                    }
                }
                catch { }
            }
            Announcer.Message(
                "pitch_0.2 .g4 .g4 pitch_1.0 all human and scp are now normal height",
                "Todos los humanos y scp ahora tienen una altura normal",
                playBackground: true
            );
        }
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía PUERTAS LOCAS

    private IEnumerator<float> CrazyDoorsAnomaly(float duration)
    {
        float timePassed = 0f;
        float interval = 0.3f;
        
        var validDoors = Door.List
            .Where(d =>
                d is not ElevatorDoor &&
                d is not CheckpointDoor
            )
            .ToList();


        while (timePassed < duration)
        {
            for (int i = 0; i < 10; i++)
            {
                if (validDoors.Count > 0)
                {
                    var randomDoor = validDoors[UnityEngine.Random.Range(0, validDoors.Count)];

                    if (randomDoor != null)
                    {
                        randomDoor.IsOpened = !randomDoor.IsOpened;
                    }
                }
            }
            yield return Timing.WaitForSeconds(interval);
            timePassed += interval;
        }
    
        Announcer.Message(
            "pitch_0.2 .g4 .g4 pitch_1.0 door control systems back online",
            "Los sistemas de control de puertas vuelven a estar en línea",
            playBackground: true
        );
    }
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Anomalía TELETRANSPORTE
    private IEnumerator<float> SwapAnomaly(float duration)
    {
        float timePassed = 0f;
    float swapInterval = 5f; 
    
    List<int> swappedPlayerIds = new List<int>();

    while (timePassed < duration)
    {
        List<Player> validPlayers = Player.List
            .Where(p => p.IsAlive && p.Role != RoleTypeId.Scp079 && !swappedPlayerIds.Contains(p.PlayerId))
            .ToList();
        
        if (validPlayers.Count >= 2)
        {
            int numberOfSwaps = validPlayers.Count / 2;

            for (int i = 0; i < numberOfSwaps; i++)
            {
                if (validPlayers.Count < 2) break;

                // Jugador A
                int indexA = UnityEngine.Random.Range(0, validPlayers.Count);
                Player playerA = validPlayers[indexA];
                validPlayers.RemoveAt(indexA);
                // Jugador B
                int indexB = UnityEngine.Random.Range(0, validPlayers.Count);
                Player playerB = validPlayers[indexB];
                validPlayers.RemoveAt(indexB);

                // Registro de los que ya hicieron TP
                swappedPlayerIds.Add(playerA.PlayerId);
                swappedPlayerIds.Add(playerB.PlayerId);
                
                Vector3 posA = playerA.Position;
                Vector3 posB = playerB.Position;

                playerA.Position = posB;
                playerB.Position = posA;

                // Hint
                Hint swapHint = new Hint();
                swapHint.Alignment = HintAlignment.Center;
                swapHint.YCoordinate = 50; 
                swapHint.Text = "<color=yellow><b>¡Intercambio Espacial!</b></color>";
                swapHint.HideAfter(4f);

                PlayerDisplay displayA = PlayerDisplay.Get(playerA);
                if (displayA != null) displayA.AddHint(swapHint);

                PlayerDisplay displayB = PlayerDisplay.Get(playerB);
                if (displayB != null) displayB.AddHint(swapHint);
            }
            
            // Sonido cuando se hace TP
            Announcer.Message("pitch_1.5 .g4", "", false);
        }

        yield return Timing.WaitForSeconds(swapInterval);
        timePassed += swapInterval;
    }

    Announcer.Message(
        "pitch_0.2 .g4 .g4 pitch_1.0 spatial coordinates stabilized",
        "Coordenadas espaciales estabilizadas.",
        playBackground: true
    );
    }
}