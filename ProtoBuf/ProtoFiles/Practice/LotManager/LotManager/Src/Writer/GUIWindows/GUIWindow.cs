using ImGuiNET;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace LotManager
{
    public class GuiWindow
    {
        /// <summary>
        /// Will execute all ImGui commands for our GuiWindow class.
        /// This method is meant to be overridden by all inheriting classes.
        /// </summary>
        public virtual void DoImGui(ref bool show)
        {
            ImGui.ShowDemoWindow(ref show);
        }
    }
}