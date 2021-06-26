using System;
using System.IO;
using Google.Protobuf;
using ImGuiNET;
using LotManager.Utilities;
using Practice;
using FSU = LotManager.Utilities.FileSystemUtilities;

namespace LotManager
{
    public class GuiWindow_CreateParkingLot : GuiWindow
    {
    
        #region Members

        private string _lotName = "";
        private int _size = 0;
        #endregion
        
        #region Constructor(s)
        //Probably none at this point, but this could change.
        #endregion

        #region Methods
        public override void DoImGui(ref bool show)
        {
            ShowLotCreator();
        }
        
        #region ImGui Helpers

        private void ShowLotCreator()
        {
            ImGui.Indent();
            ImGui.InputText($"File Name", ref _lotName, 128);
            ImGui.InputInt($"Number of Spaces", ref _size);

            if (!String.IsNullOrEmpty(_lotName) && _size != 0)
            {
                if (ImGui.Button("Create Lot!"))
                {
                    CreateLot();
                    _lotName = "";
                    _size = 0;
                }
            }
        }
        #endregion
        
        #region Functionality
        private void CreateLot()
        {
            var newLot = new ParkingLot();

            newLot.Size = _size;
            for (int i = 0; i < _size; i++)
            {
                var newSpot = new ParkingSpot();
                newSpot.Type = ParkingSpot.Types.VehicleType.Empty;
                newSpot.SpotNum = i;
                
                newLot.Spots.Add(newSpot);
            }

            var fullpath = Path.Combine(FileSystemConstants.AppPath, $"{_lotName}.lot");
            if (!FSU.DirectoryExists(FileSystemConstants.AppPath))
            {
                Directory.CreateDirectory(FileSystemConstants.AppPath);
            }

            using (var fstream = File.Create(fullpath))
            {
                try
                {
                    newLot.WriteTo(fstream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    fstream.Close();
                }
            }
        }
        #endregion
        
        #endregion
                
    }
}