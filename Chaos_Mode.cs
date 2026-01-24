using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace Chaos_Mode
{
    public class Chaos_Mode : Plugin
    {
        public override string Name => "Chaos Mode";
        public override string Description => "Modo que implementa cosas que hacen que el server sea una locura";
        public override string Author => "AdrianoElAldeano";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);
        public EventHandler EventHandler = new EventHandler();
        public AnomalyHandler AnomalyHandler = new AnomalyHandler(); 
        public VampireHandler VampireHandler = new VampireHandler();
        
        public override void Enable()
        {
            CustomHandlersManager.RegisterEventsHandler(EventHandler);
            CustomHandlersManager.RegisterEventsHandler(AnomalyHandler);
            CustomHandlersManager.RegisterEventsHandler(VampireHandler);
        }

        public override void Disable()
        {
            CustomHandlersManager.UnregisterEventsHandler(EventHandler);
            CustomHandlersManager.UnregisterEventsHandler(AnomalyHandler);
            CustomHandlersManager.UnregisterEventsHandler(VampireHandler);
        }
    }
}