// Decompiled with JetBrains decompiler
// Type: TLTDatabaseEditor.DashboardModel
// Assembly: TLTDatabaseEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3AA8152-A60D-4A42-87BE-242C5FFCE9A0
// Assembly location: C:\Program Files (x86)\GizmoTron v1.5\TLTDatabaseEditor.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using TLTDatabaseEditor.Properties;

namespace TLTDatabaseEditor
{
    public class DashboardModel
    {
        public DatabaseConnectionManagerDataContext dc = new DatabaseConnectionManagerDataContext(Settings.Default.TLT_InventoryConnectionString);
        private IQueryable<int> _buildingId;

        public event DashboardModel.SqlExceptionDelegate sqlExceptionThrown;

        public List<GridListItemViewModel<ControlType>> GetRoomControlTypes(
          string roomNumber,
          string buildingName)
        {
            try
            {
                if (this.dc.DatabaseExists())
                {
                    List<GridListItemViewModel<ControlType>> roomControlTypes = new List<GridListItemViewModel<ControlType>>();
                    Table<ControlType> controlTypes = this.dc.ControlTypes;
                    IQueryable<int> _buildingId = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                    foreach (ControlType controlType in controlTypes)
                    {
                        int? nullable = this.dc.Classrooms.Where<Classroom>((Expression<Func<Classroom, bool>>)(x => x.BuildingID == _buildingId.First<int>() && x.RoomNumber == roomNumber)).Select<Classroom, int?>((Expression<Func<Classroom, int?>>)(x => x.ControlType)).First<int?>();
                        int controlTypeId = controlType.ControlTypeID;
                        bool flag = (nullable.GetValueOrDefault() == controlTypeId ? (nullable.HasValue ? 1 : 0) : 0) != 0;
                        roomControlTypes.Add(new GridListItemViewModel<ControlType>()
                        {
                            Data = controlType,
                            IsEnabled = flag
                        });
                    }
                    return roomControlTypes;
                }
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<GridListItemViewModel<ControlType>>)null;
        }

        public int? GetTypeIdToUpdate(List<GridListItemViewModel<TypeDesc>> types, string selectedType)
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return new int?(this.dc.TypeDescs.Where<TypeDesc>((Expression<Func<TypeDesc, bool>>)(x => x.Description == selectedType)).First<TypeDesc>().TypeDescID);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return new int?();
        }

        public int? GetControlTypeToUpdate(
          List<GridListItemViewModel<ControlType>> types,
          string selectedType)
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return new int?(this.dc.ControlTypes.Where<ControlType>((Expression<Func<ControlType, bool>>)(x => x.Description == selectedType)).First<ControlType>().ControlTypeID);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return new int?();
        }

        public List<GridListItemViewModel<TypeDesc>> GetRoomDescTypes(
          string roomNumber,
          string buildingName)
        {
            try
            {
                if (this.dc.DatabaseExists())
                {
                    List<GridListItemViewModel<TypeDesc>> roomDescTypes = new List<GridListItemViewModel<TypeDesc>>();
                    Table<TypeDesc> typeDescs = this.dc.TypeDescs;
                    IQueryable<int> _buildingId = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                    foreach (TypeDesc typeDesc in typeDescs)
                    {
                        int? nullable = this.dc.Classrooms.Where<Classroom>((Expression<Func<Classroom, bool>>)(x => x.BuildingID == _buildingId.First<int>() && x.RoomNumber == roomNumber)).Select<Classroom, int?>((Expression<Func<Classroom, int?>>)(x => x.TypeID)).First<int?>();
                        int typeDescId = typeDesc.TypeDescID;
                        bool flag = (nullable.GetValueOrDefault() == typeDescId ? (nullable.HasValue ? 1 : 0) : 0) != 0;
                        roomDescTypes.Add(new GridListItemViewModel<TypeDesc>()
                        {
                            Data = typeDesc,
                            IsEnabled = flag
                        });
                    }
                    return roomDescTypes;
                }
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<GridListItemViewModel<TypeDesc>>)null;
        }

        public void UpdateControlType(int controlTypeID, string buildingName, string roomNumber)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                IQueryable<int> source = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                this.dc.ExecuteQuery<ClassroomQuery>("UPDATE Classroom SET ControlType = " + (object)controlTypeID + "WHERE BuildingID = " + (object)source.First<int>() + " AND RoomNumber = '" + roomNumber + "'");
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void SetRoomEnables(List<string> RoomEnables, string selectedBuilding)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                IQueryable<int> source = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == selectedBuilding)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                foreach (string roomEnable in RoomEnables)
                    this.dc.ExecuteQuery<ClassroomQuery>("UPDATE Classroom SET Enabled = 1 WHERE RoomNumber = '" + roomEnable + "' AND BuildingID = " + (object)source.First<int>());
                this.RefreshClassroomEntity();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void SetRoomDisables(List<string> RoomDisables, string selectedBuilding)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                IQueryable<int> source = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == selectedBuilding)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                foreach (string roomDisable in RoomDisables)
                    this.dc.ExecuteQuery<ClassroomQuery>("UPDATE Classroom SET Enabled = 0 WHERE RoomNumber = '" + roomDisable + "' AND BuildingID = " + (object)source.First<int>());
                this.RefreshClassroomEntity();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void SetBuildingEnables(List<string> BuildingEnables)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                foreach (string buildingEnable in BuildingEnables)
                {
                    string building = buildingEnable;
                    this.dc.ExecuteQuery<BuildingQuery>("UPDATE Building SET Enabled = 1 WHERE BuildingID = " + (object)this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == building)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID)).First<int>());
                }
                this.RefreshBuildingEntity();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void SetBuildingDisables(List<string> BuildingDisables)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                foreach (string buildingDisable in BuildingDisables)
                {
                    string building = buildingDisable;
                    this.dc.ExecuteQuery<BuildingQuery>("UPDATE Building SET Enabled = 0 WHERE BuildingID = " + (object)this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == building)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID)).First<int>());
                }
                this.RefreshBuildingEntity();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void UpdateType(int typeId, string buildingName, string roomNumber)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                IQueryable<int> source = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                this.dc.ExecuteQuery<ClassroomQuery>("UPDATE Classroom SET TypeID = " + (object)typeId + "WHERE BuildingID = " + (object)source.First<int>() + " AND RoomNumber = '" + roomNumber + "'");
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public List<string> GetRoomControlTypes()
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return this.dc.ControlTypes.Select<ControlType, string>((Expression<Func<ControlType, string>>)(x => x.Description)).ToList<string>();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<string>)null;
        }

        public List<string> GetRoomTypes()
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return this.dc.ExecuteQuery<TypeDesc>("SELECT * FROM TypeDesc").Select<TypeDesc, string>((Func<TypeDesc, string>)(x => x.Description)).ToList<string>();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<string>)null;
        }

        public void AddBuilding(string BuildingName, string BuildingCode, bool IsEnabled)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                this.dc.ExecuteQuery<BuildingQuery>("INSERT INTO Building VALUES ({0}, {1}, {2})", (object)BuildingName, (object)BuildingCode, (object)IsEnabled);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void AddClassroom(
          int BuildingID,
          string RoomNumber,
          string TypeDesc,
          string ControlType,
          int Capacity,
          bool IsEnabled)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                int num1 = this.dc.ControlTypes.Where<ControlType>((Expression<Func<ControlType, bool>>)(x => x.Description == ControlType)).Select<ControlType, int>((Expression<Func<ControlType, int>>)(x => x.ControlTypeID)).First<int>();
                int num2 = this.dc.TypeDescs.Where<TypeDesc>((Expression<Func<TypeDesc, bool>>)(x => x.Description == TypeDesc)).Select<TypeDesc, int>((Expression<Func<TypeDesc, int>>)(x => x.TypeDescID)).First<int>();
                this.dc.ExecuteQuery<ClassroomQuery>("INSERT INTO Classroom VALUES ({0}, {1}, {2}, {3}, {4}, {5})", (object)RoomNumber, (object)BuildingID, (object)num2, (object)Capacity, (object)num1, (object)IsEnabled);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void RemoveDescriptions(List<int> FeatureDescIDs, string RoomNumber, int BuildingID)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                foreach (int featureDescId in FeatureDescIDs)
                    this.dc.ExecuteQuery<ClassroomFeatureDescQuery>("DELETE FROM ClassroomFeatureDesc WHERE FeatureDescID = {0} AND RoomNumber = {1} AND BuildingID = {2}", (object)featureDescId, (object)RoomNumber, (object)BuildingID);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public void AddDescriptions(List<int> FeatureDescIDs, string RoomNumber, int BuildingID)
        {
            try
            {
                if (!this.dc.DatabaseExists())
                    return;
                foreach (int featureDescId in FeatureDescIDs)
                    this.dc.ExecuteQuery<ClassroomFeatureDescQuery>("INSERT INTO ClassroomFeatureDesc VALUES ({0}, {1}, {2})", (object)featureDescId, (object)RoomNumber, (object)BuildingID);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public List<int> GetFeatureDescs(List<string> features)
        {
            try
            {
                if (this.dc.DatabaseExists())
                {
                    List<int> featureDescs = new List<int>();
                    foreach (string feature1 in features)
                    {
                        string feature = feature1;
                        featureDescs.Add(this.dc.FeatureDescs.Where<FeatureDesc>((Expression<Func<FeatureDesc, bool>>)(x => x.Description == feature)).Select<FeatureDesc, int>((Expression<Func<FeatureDesc, int>>)(x => x.FeatureDescID)).First<int>());
                    }
                    return featureDescs;
                }
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<int>)null;
        }

        public List<Building> GetBuildingNames()
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return this.dc.Buildings.OrderBy<Building, string>((Expression<Func<Building, string>>)(x => x.BuildingName)).ToList<Building>();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<Building>)null;
        }

        public List<Classroom> GetClassroomNames(string buildingName)
        {
            try
            {
                if (this.dc.DatabaseExists())
                {
                    this._buildingId = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID));
                    return this.dc.Classrooms.Where<Classroom>((Expression<Func<Classroom, bool>>)(x => x.BuildingID == this._buildingId.First<int>())).Select<Classroom, Classroom>((Expression<Func<Classroom, Classroom>>)(x => x)).ToList<Classroom>();
                }
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<Classroom>)null;
        }

        public int? GetBuildingCodeForBuildingName(string buildingName)
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return new int?((this._buildingId = this.dc.Buildings.Where<Building>((Expression<Func<Building, bool>>)(x => x.BuildingName == buildingName)).Select<Building, int>((Expression<Func<Building, int>>)(x => x.BuildingID))).First<int>());
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return new int?();
        }

        private List<FeatureDesc> GetAllRoomFeatures()
        {
            try
            {
                if (this.dc.DatabaseExists())
                    return this.dc.FeatureDescs.ToList<FeatureDesc>();
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<FeatureDesc>)null;
        }

        private void RefreshClassroomEntity()
        {
            try
            {
                this.dc.Refresh(RefreshMode.OverwriteCurrentValues, (IEnumerable)this.dc.Classrooms);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        private void RefreshBuildingEntity()
        {
            try
            {
                this.dc.Refresh(RefreshMode.OverwriteCurrentValues, (IEnumerable)this.dc.Buildings);
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
        }

        public List<GridListItemViewModel<FeatureDesc>> GetRoomFeatures(
          string classroomNumber)
        {
            try
            {
                if (this.dc.DatabaseExists())
                {
                    List<GridListItemViewModel<FeatureDesc>> source1 = new List<GridListItemViewModel<FeatureDesc>>();
                    IQueryable<ClassroomFeatureDesc> source2 = this.dc.ClassroomFeatureDescs.Where<ClassroomFeatureDesc>((Expression<Func<ClassroomFeatureDesc, bool>>)(x => x.RoomNumber == classroomNumber && x.BuildingID == this._buildingId.First<int>()));
                    Expression<Func<ClassroomFeatureDesc, int>> selector = (Expression<Func<ClassroomFeatureDesc, int>>)(x => x.FeatureDescID);
                    foreach (int num in (IEnumerable<int>)source2.Select<ClassroomFeatureDesc, int>(selector))
                    {
                        int id = num;
                        IQueryable<FeatureDesc> source3 = this.dc.FeatureDescs.Where<FeatureDesc>((Expression<Func<FeatureDesc, bool>>)(x => x.FeatureDescID == id));
                        if (source3.Count<FeatureDesc>() > 0)
                            source1.Add(new GridListItemViewModel<FeatureDesc>()
                            {
                                Data = source3.First<FeatureDesc>(),
                                IsEnabled = true
                            });
                    }
                    foreach (FeatureDesc allRoomFeature in this.GetAllRoomFeatures())
                    {
                        if (!source1.Select<GridListItemViewModel<FeatureDesc>, string>((Func<GridListItemViewModel<FeatureDesc>, string>)(x => x.Data.Description)).Contains<string>(allRoomFeature.Description))
                            source1.Add(new GridListItemViewModel<FeatureDesc>()
                            {
                                Data = allRoomFeature,
                                IsEnabled = false
                            });
                    }
                    return source1;
                }
            }
            catch (SqlException ex)
            {
                this.sqlExceptionThrown(ex.Message);
                throw new Exception();
            }
            return (List<GridListItemViewModel<FeatureDesc>>)null;
        }

        public delegate void SqlExceptionDelegate(string message);
    }
}
