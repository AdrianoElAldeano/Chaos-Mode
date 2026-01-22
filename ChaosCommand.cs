using System;
using CommandSystem;

namespace Chaos_Mode;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ChaosCommand : ICommand
{
    public string Command => "chaos";
    
    public string[] Aliases => new[] { "cm" };
    
    public string Description => "Comandos para controlar el Chaos Mode (Pausar, Reanudar, Forzar Eventos).";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        sender.CheckPermission(PlayerPermissions.RoundEvents);
        
        if (AnomalyHandler.Instance == null)
        {
            response = "Error: El AnomalyHandler no está inicializado. ¿Ha empezado la ronda?";
            return false;
        }

        // Respuesta si solo pones "Chaos"
        if (arguments.Count == 0)
        {
            response = "Usa: chaos <pause | resume | evento>";
            return false;
        }

        // Code del pausado
        string action = arguments.At(0).ToLower();

        switch (action)
        {
            case "pause":
            case "stop":
            case "parar":
                AnomalyHandler.Instance.IsPaused = true;
                response = "Chaos Mode PAUSADO. No ocurrirán más eventos automáticos.";
                return true;
        // Code de reanudar
            case "resume":
            case "play":
            case "reanudar":
                AnomalyHandler.Instance.IsPaused = false;
                response = "Chaos Mode REANUDADO. La rotación de eventos continúa.";
                return true;
        // Code para forzar un evento
            case "evento":
            case "force":
            case "forzar":
                // Revisión de sí ha puesto el nombre del evento 
                if (arguments.Count < 2)
                {
                    response = "Debes especificar el evento. Ejemplo: chaos evento disco (Opciones: fantasma, velocidad, apagon, disco)";
                    return false;
                }

                string eventName = arguments.At(1).ToLower();
                return ForceSpecificEvent(eventName, out response);

            default:
                response = "Comando no reconocido. Usa: pause, resume, evento <nombre>.";
                return false;
        }
    }

    // Lógica para elegir qué evento forzar
    private bool ForceSpecificEvent(string name, out string response)
    {
        switch (name)
        {
            case "fantasma":
            case "ghost":
            case "0":
                AnomalyHandler.Instance.TriggerRandomAnomaly(0);
                response = "Forzando evento: FANTASMA";
                return true;

            case "velocidad":
            case "speed":
            case "1":
                AnomalyHandler.Instance.TriggerRandomAnomaly(1);
                response = "Forzando evento: VELOCIDAD";
                return true;

            case "apagon":
            case "blackout":
            case "2":
                AnomalyHandler.Instance.TriggerRandomAnomaly(2);
                response = "Forzando evento: APAGÓN";
                return true;

            case "disco":
            case "fiesta":
            case "luces":
            case "3":
                AnomalyHandler.Instance.TriggerRandomAnomaly(3);
                response = "Forzando evento: DISCO";
                return true;
            
            case "small":
            case "enano":
            case "4":
                AnomalyHandler.Instance.TriggerRandomAnomaly(4);
                response = "Forzando evento: ENANO";
                return true;
            
            case "gigant":
            case "gigante":
            case "5":
                AnomalyHandler.Instance.TriggerRandomAnomaly(5);
                response = "Forzando evento: GIGANTE";
                return true;
            
            case "door":
            case "puerta":
            case "6":
                AnomalyHandler.Instance.TriggerRandomAnomaly(6);
                response = "Forzando evento: PUERTAS LOCAS";
                return true;
                
            default:
                response = $"El evento '{name}' no existe. Intenta: fantasma, velocidad, apagon, disco, enano, gigante.";
                return false;
        }
    }
}