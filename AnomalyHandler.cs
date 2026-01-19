using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using MEC;
using UnityEngine;

namespace Chaos_Mode;

public class AnomalyHandler : CustomEventsHandler
{
    private CoroutineHandle _anomalyCoroutine;

    public override void OnServerRoundStarted()
    
    {
        LabApi.Features.Console.Logger.Info("Los eventos anomalos se estan iniciando.");
        
        Timing.KillCoroutines(_anomalyCoroutine);
        
        _anomalyCoroutine = Timing.RunCoroutine(AnomalyCycle());
    }

    public override void OnServerRoundRestarted()
    {
        Timing.KillCoroutines(_anomalyCoroutine);

        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Config tiempo anomalias
    private IEnumerator<float> AnomalyCycle()
    {
        // Espera entre 3 y 5 minutos para el primer evento
        float firstWait = UnityEngine.Random.Range(180f, 300f);

        yield return Timing.WaitForSeconds(firstWait);

        while (true)
        {
            TriggerRandomAnomaly();

            // Espera de 4 a 7 minutos para la siguiente anomalia (después de la primera anomalia)
            float nextWait = UnityEngine.Random.Range(240f, 420f);

            yield return Timing.WaitForSeconds(nextWait);
        }
    }

// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Selector de Anomalías 
    private void TriggerRandomAnomaly()
    {
        // Cambiar numeros entre parentesis dependiendo de los eventos que tenga, ej: 10 eventos = (0, 10)
        int randomPick = UnityEngine.Random.Range(0, 4);

        switch (randomPick)
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
        float timePassed = 0f;

        while (timePassed < duration)
        {
            foreach (Room room in Room.List)
            {
                room.LightController.FlickerLights(5.0f);
            }
        }
        yield return Timing.WaitForSeconds(1f);
        timePassed += 1f;
        
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
}