using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;

namespace GooEyeTesting
{
    public class Program : Overlay
    {
        //Crosshair Elements
        bool enableOverlay = false;
        bool enableCrosshair = false;
        bool enableCircle = false;
        //Crosshair Variables
        int crosshairWidth = 8;
        int crosshairHeight = 8;
        Vector4 crosshairColor = new Vector4(1, 1, 1, 1);
        int radius = 5;
        Vector4 circleColor = new Vector4(1, 1, 1, 1);
        //Positions
        Vector2 screenSize = new Vector2(1920, 1080);
        Vector2 drawPosition = new Vector2(1920 / 2, 1080 / 2 + 60);
        Vector2 draw1 = new Vector2(1920 / 2, 1080 / 2 + 60);
        Vector2 draw2 = new Vector2(1920 / 2, 1080 / 2 + 60);
        Vector2 draw3 = new Vector2(1920 / 2, 1080 / 2 + 8 + 60);
        Vector2 draw4 = new Vector2(1920 / 2, 1080 / 2 - 8 + 60);

        protected override void Render()
        {
            DrawMenu();
            DrawOverlay();
        }

        void DrawMenu()
        {
            ImGui.Begin("Crozz");
            ImGui.Checkbox("Enable Overlay", ref enableOverlay);
            ImGui.Checkbox("Crosshair", ref enableCrosshair);
            ImGui.SliderInt("Crosshair Width", ref crosshairWidth, 0, 100);
            ImGui.SliderInt("Crosshair Height", ref crosshairHeight, 0, 100);
            ImGui.ColorEdit4("Crosshair Color", ref crosshairColor);
            ImGui.Checkbox("Circle", ref enableCircle);
            ImGui.SliderInt("Radius", ref radius, 0, 100);
            ImGui.ColorEdit4("Circle Color", ref circleColor);
            ImGui.Text("If the crosshair doesnt show up go to your games video settings\nand turn windowed fullscreen on");
            ImGui.End();
        }

        void DrawOverlay()
        {
            if (enableOverlay)
            {
                ImGui.SetNextWindowSize(screenSize);
                ImGui.SetNextWindowPos(new Vector2(0, 0));
                ImGui.Begin("Overlay", ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoInputs | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);
                ImDrawListPtr drawList = ImGui.GetWindowDrawList();

                draw1.X = drawPosition.X + crosshairWidth;
                draw2.X = drawPosition.X - crosshairWidth;
                draw3.Y = drawPosition.Y + crosshairHeight;
                draw4.Y = drawPosition.Y - crosshairHeight;
                if (enableCircle)
                {
                    drawList.AddCircle(drawPosition, radius, ImGui.ColorConvertFloat4ToU32(circleColor));
                }
                if (enableCrosshair)
                {
                    drawList.AddLine(draw1, draw2, ImGui.ColorConvertFloat4ToU32(crosshairColor));
                    drawList.AddLine(draw3, draw4, ImGui.ColorConvertFloat4ToU32(crosshairColor));
                }
            }
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Start().Wait();
        }
    }
}