using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Google.Protobuf;
using ImGuiNET;
using LotManager.Utilities;
using Practice;
using FSU = LotManager.Utilities.FileSystemUtilities;

namespace LotManager
{
    public class GuiWindow_ModifyLot : GuiWindow
    {

        #region Members
        
        /// <summary>
        /// deserialized parking lot instance to work on/modify.
        /// </summary>
        private ParkingLot _myLot;

        /// <summary>
        /// full path to parking lot file.
        /// </summary>
        private string LotPath = "";

        /// <summary>
        /// Name of parking lot, extracted from filepath.
        /// </summary>
        private string LotName = "";
        
        /// <summary>
        /// private accessor for the <b>names</b> of all possible vehicle types
        /// in a parking spot.
        /// </summary>
        private string[] _vTypes = Enum.GetNames(typeof(ParkingSpot.Types.VehicleType));
        
        /// <summary>
        /// private accessor for the <b>values</b> of all possible vehicle types
        /// in a parking spot.
        /// </summary>
        private ParkingSpot.Types.VehicleType[] _vValues = (ParkingSpot.Types.VehicleType[]) Enum.GetValues(typeof(ParkingSpot.Types.VehicleType));
        
        #endregion

        
        #region Constructor(s)
        
        /// <summary>
        /// We need a constructor for this ImGUI "window."
        /// It needs to handle reading our parking lot in from the file system
        /// and getting it ready to do work on. If, for any reason during the
        /// File I/O, deserialization process, we encounter an exception, we'll share the
        /// exception with the console and then continue to allow our app to run.
        /// </summary>
        /// <param name="lotPath">
        /// full filepath to where the parking lot data lives
        /// on the computer.
        /// </param>
        public GuiWindow_ModifyLot(string lotPath)
        {
            LotPath = lotPath;
            
            LotName = lotPath.Split('\\')[^1].Split('.')[0];

            using (var fstream = File.OpenRead(LotPath))
            {
                try
                {
                    _myLot = ParkingLot.Parser.ParseFrom(fstream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    LotPath = "";
                    LotName = "";
                }
                finally
                {
                    fstream.Close();
                }
            }

        }
        #endregion

        #region Methods
        public override void DoImGui(ref bool show)
        {
            if (_myLot != null)
            {
                if (_myLot.IsInitialized())
                {
                    ShowLot();
                }
            }
        }


        /// <summary>
        /// quick method to display our modifiable parking lot
        /// members.
        /// </summary>
        private void ShowLot()
        {
            ImGui.Text($"Modify {LotName}: ");
            {
                ImGui.Indent();
                if (ImGui.CollapsingHeader("Spots"))
                {
                    ImGui.Indent();

                    for(int i = 0; i < _myLot.Spots.Count; i++)
                    {
                        var spot = _myLot.Spots[i];
                        if (ImGui.CollapsingHeader($"Spot {i}: "))
                        {
                            DisplaySpot(spot);
                        }
                    }
                
                    ImGui.Unindent();
                }
                ImGui.Unindent();
            }
        }

        /// <summary>
        /// Displays the modifiable information for a given parking spot.
        /// </summary>
        /// <param name="spot">
        /// Parking spot to display.
        /// </param>
        private void DisplaySpot(ParkingSpot spot)
        {
            ImGui.Indent();
            var lookup = MakeTypeLookup();

            int itype = 0;
            for (itype = 0; itype < _vTypes.Length; itype++)
            {
                if (!String.Equals(_vTypes[itype], spot.Type.ToString())) continue;
                break;
            }
            ImGui.ListBox($"Car in Spot {spot.SpotNum}", ref itype, _vTypes, _vTypes.Length);
            spot.Type = lookup[_vTypes[itype]];
            ImGui.Unindent();
        }

        private Dictionary<string, ParkingSpot.Types.VehicleType> MakeTypeLookup()
        {
            Dictionary<string, ParkingSpot.Types.VehicleType> _typeLookup =
                new Dictionary<string, ParkingSpot.Types.VehicleType>();

            for (int i = 0; i < _vValues.Length; i++)
            {
                _typeLookup.Add(Enum.GetName(typeof(ParkingSpot.Types.VehicleType), _vValues[i]), _vValues[i]);
            }

            return _typeLookup;
        }

        /// <summary>
        /// quick method to communicate through ImGUI that there was a problem loading this parking lot file.
        /// </summary>
        private void ShowError()
        {
            ImGui.Begin("ERROR LOADING LOT");
            ImGui.TextColored(new Vector4(1.0f, 0.0f, 0.0f, 1.0f), $"Parking lot was unable to be loaded/parsed!");
            ImGui.End();
        }

        private static bool _editingLot = false;
        public static void ShowAvailableLots()
        {
            var allFiles = Directory.GetFiles(FileSystemConstants.AppPath);

            foreach (var filepath in allFiles)
            {
                ImGui.Indent();
                var lotName = filepath.Split('\\')[^1].Split('.')[0];
                if (ImGui.CollapsingHeader($"{lotName}"))
                {
                    var LotModifier = new GuiWindow_ModifyLot(filepath);

                    bool dummy = true;
                    LotModifier.DoImGui(ref dummy);
                }
                ImGui.Unindent();
            }
        }
        #endregion
    }
}