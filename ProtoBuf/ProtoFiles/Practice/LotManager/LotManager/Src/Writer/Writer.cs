// Bryan is responsible for this monstrosity.
// this is where all of our mainline function calls for creating and managing a
// virtual, fake parking lot.

using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using LotManager.Utilities;
using Google.Protobuf;
using Google.Protobuf.Collections;
using ImGuiNET;
using Veldrid;
using Practice;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Vulkan.Xlib;

namespace LotManager
{
    public static partial class Writer
    {
        
        #region members
        private static Sdl2Window _window;
        private static GraphicsDevice _gd;
        private static CommandList _cl;

        private static ImGuiRenderer _guiRenderer;
        private static InputSnapshot _snapshot;

        private static bool _showAboutWindow = false;
        private static bool _showDemoWindow = true;
        private static bool _showMainWindow = true;

        private static bool _dockSpaceInitialized = false;

        private static readonly GuiWindow_CreateParkingLot _createLotWindow = new GuiWindow_CreateParkingLot();
        private static readonly GuiWindow_ModifyLot _modifyLot = null;

        private static uint _dockID_Root, _dockID_Right, _dockID_Left;
        #endregion
        
        /// <summary>
        /// Main realtime loop 
        /// </summary>
        /// <returns></returns>
        public static int MainLoop()
        {
            #region Graphics startup boilerplate

            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 960, 540, WindowState.Normal, "Parking Lot Manager"),
                out _window,
                out _gd);
            
            _cl = _gd.ResourceFactory.CreateCommandList();

            _window.Resizable = false;
            
            _guiRenderer = new ImGuiRenderer(_gd,
                _gd.MainSwapchain.Framebuffer.OutputDescription,
                _window.Width,
                _window.Height);

            ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            
            //ImGui.GetIO().ConfigViewportsNoAutoMerge = true;
            

            _window.Resized += () =>
            {
                _guiRenderer.WindowResized(_window.Width, _window.Height);
                _gd.MainSwapchain.Resize((uint) _window.Width, (uint) _window.Height);
            };
            #endregion
            uint dockspaceID = 0;
            
            #region main loop
            while (_showMainWindow && _window.Exists)
            {
                #region update input and renderer states
                _snapshot = _window.PumpEvents();
                _guiRenderer.Update(1f/120f, _snapshot);
                #endregion
                
                ImGui.SetNextWindowPos(Vector2.Zero);
                ImGui.SetNextWindowSize(new Vector2(_window.Width, _window.Height));
                
                if(ImGui.Begin("LotManager", ref _showMainWindow));
                {
                    if (ImGui.CollapsingHeader("Create Lot"))
                    {
                        bool dummy = true;
                        _createLotWindow.DoImGui(ref dummy);
                        ImGui.Unindent();
                    }

                    if (ImGui.CollapsingHeader("Modify Lot"))
                    {
                        bool dummy = true;
                        GuiWindow_ModifyLot.ShowAvailableLots();
                    }
                    
                }
                ImGui.End();
                #region ImGui Commands
                #endregion

                #region execute graphics commandlist.
                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(0, 0, 0.2f, 1f));
                _guiRenderer.Render(_gd, _cl);
                
                _cl.End();
                _gd.SubmitCommands(_cl);


                /*if ((ImGui.GetIO().ConfigFlags & ImGuiConfigFlags.DockingEnable) != 0)
                {
                    #region multi-viewport test

                    ImGui.UpdatePlatformWindows();
                    ImGui.RenderPlatformWindowsDefault();

                    #endregion
                }*/
                
                _gd.SwapBuffers(_gd.MainSwapchain);
                #endregion
            }
            #endregion
            return 0;
        }
        
        /// <summary>
        /// This method will be responsible for generating a user-specified parking lot.
        /// The program will prompt its user for input such as parking lot size, name, etc.. For the sake of
        /// simplicity, lot size will be defined by a single dimension.
        ///
        /// Parking lot files will be stored in the user's "MyDocuments" folder.
        /// </summary>
        public static void GenerateParkingLot()
        {
            bool ValidNumberInput = false;
            Console.WriteLine("---====GENERATING PARKING LOT====---");
            string PromptForLotName = "Choose Name For Parking Lot: ";
            string PromptForLotSize = "Specify Number of spots in Parking Lot: ";

            string LotName = string.Empty;

            int LotSize = 0;

            Console.Write(PromptForLotName);
            LotName = Console.ReadLine();

            while (!ValidNumberInput)
            {
                Console.Write(PromptForLotSize);
                var input = Console.ReadLine();
                ValidNumberInput = int.TryParse(input, out LotSize);

                if (!ValidNumberInput)
                {
                    Console.WriteLine(
                        $"The given input '{input}' is not a valid numerical input! We will try this again...");
                }
            }

            var newLot = new ParkingLot();
            newLot.Size = LotSize;

            for (int i = 0; i < LotSize; i++)
            {
                var newSpot = new ParkingSpot();
                newSpot.Type = ParkingSpot.Types.VehicleType.Empty;
                newSpot.SpotNum = i;
                newLot.Spots.Add(newSpot);
            }

            var fullpath = Path.Combine(FileSystemConstants.AppPath, $"{LotName}.dat");

            using (var fstream = File.Create(fullpath))
            {
                newLot.WriteTo(fstream);
                fstream.Close();
            }
        }
        
        
        /// <summary>
        /// This method is responsible for adding a vehicle to a specified parking lot. 
        /// </summary>
        public static void AddVehicle()
        {
            Console.WriteLine("---====ADD VEHICLE TO PARKING LOT====---\n");
            var AllFiles = Directory.GetFiles(FileSystemConstants.AppPath);
            var builder = new StringBuilder();

            #region build file display string

            for (int i = 0; i < AllFiles.Length; i++)
            {
                var tokens = AllFiles[i].Split('.')[0].Split('\\');
                string lotName = tokens[tokens.Length - 1]; //<- could have used index-from-end expression '^' but 
                builder.Append($"[{i}] {lotName}\n");       // wanted the option to backport to a dotnetframework version
            }                                               // of c#, since this is supposed to be a practice, and all.

            #endregion

            bool ValidNumber = false;

            int lotSelection = -1;

            while (!ValidNumber)
            {
                Console.WriteLine(builder.ToString());
                Console.Write("Choose parking lot by number: ");

                var input = Console.ReadLine();

                ValidNumber = int.TryParse(input, out lotSelection);

                if (!ValidNumber)
                {
                    Console.WriteLine(
                        $"The given input '{input}' is not a valid numerical input! We will try this again...");
                }
                else if (lotSelection < 0 || lotSelection >= AllFiles.Length)
                {
                    Console.WriteLine(
                        $"Lot option number {lotSelection} is not within a valid range of selectable parking lots. We will try this again...");
                    ValidNumber = false;
                }
            }

            ParkingLot newLot = null;

            var fullpath = Path.Combine(FileSystemConstants.AppPath, AllFiles[lotSelection]);

            using (var fstream = File.OpenRead(fullpath))
            {
                newLot = ParkingLot.Parser.ParseFrom(fstream);
            }

            var spots = newLot.Spots;

            var name = AllFiles[lotSelection].Split('.')[0];

            if (CountEmpty(spots) == 0)
            {
                Console.WriteLine($"Parking lot {name} is full! Can't add any more cars.");
                return;
            }

            ValidNumber = false;

            StringBuilder lotEntryBuilder = new StringBuilder();

            for (int i = 0; i < spots.Count; i++)
            {
                lotEntryBuilder.Append($"[{i}] {spots[i].Type.ToString()}\n");
            }

            int spotSelection = -1;
            
            while (!ValidNumber)
            {
                Console.WriteLine(lotEntryBuilder.ToString());
                Console.Write("Choose a spot to fill: ");

                var input = Console.ReadLine();
                ValidNumber = int.TryParse(input, out spotSelection);
                if (spotSelection < 0 || spotSelection >= spots.Count)
                {
                    ValidNumber = false;
                }
            }

            var vType = new List<ParkingSpot.Types.VehicleType>(new []
            {
                ParkingSpot.Types.VehicleType.Car,
                ParkingSpot.Types.VehicleType.Moto,
                ParkingSpot.Types.VehicleType.Truck,
                ParkingSpot.Types.VehicleType.Van
            });

            StringBuilder typeEntryBuilder = new StringBuilder();

            for (int i = 0; i < vType.Count; i++)
            {
                typeEntryBuilder.Append($"[{i}]{vType[i].ToString()}\n");
            }

            ValidNumber = false;

            int typeSelection = -1;

            while (!ValidNumber)
            {
                Console.WriteLine(typeEntryBuilder.ToString());
                Console.Write("Which car type? ");

                var input = Console.ReadLine();
                ValidNumber = int.TryParse(input, out typeSelection);
                if (typeSelection < 0 || typeSelection >= vType.Count)
                {
                    ValidNumber = false;
                }
            }

            spots[spotSelection].Type = vType[typeSelection];

            using (var fstream = File.Create(fullpath))
            {
                newLot.WriteTo(fstream);
                fstream.Close();
            }
            
            Console.WriteLine("Done!");
        }

        /// <summary>
        /// This method is responsible for removing a vehicle to a specified parking lot.
        /// </summary>
        public static void RemoveVehicle()
        {
            
        }

        /// <summary>
        /// crawls through a given set of parking spots and counts the empty ones.
        /// </summary>
        /// <param name="spots">set of parking spots to crawl through</param>
        /// <returns>
        /// [<b>int</b>] Total count of empty parking spots.
        /// </returns>
        private static int CountEmpty(RepeatedField<ParkingSpot> spots)
        {
            int count = 0;
            
            foreach (var spot in spots)
            {
                if (spot.Type == ParkingSpot.Types.VehicleType.Empty) count++;
            }

            return count;
        }
        
    }
}