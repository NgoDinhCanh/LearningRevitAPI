#region Namespaces
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using R11_FoundationPile.ViewModel;
#endregion

namespace R11_FoundationPile
{
    public class FoundationPileViewModel : BaseViewModel
    {
        #region property
        public UIDocument UiDoc;
        public Document Doc;
        private UnitProject _Unit;
        public UnitProject Unit { get { return _Unit; } set { _Unit = value; OnPropertyChanged(); } }
        private List<Element> _Columns;
        public List<Element> Columns { get => _Columns; set { _Columns = value; OnPropertyChanged(); } }
        private TransactionGroup _TransactionGroup;
        public TransactionGroup TransactionGroup { get => _TransactionGroup; set { _TransactionGroup = value; OnPropertyChanged(); } }
        private FoundationPileModel _FoundationPileModel;
        public FoundationPileModel FoundationPileModel { get => _FoundationPileModel; set { _FoundationPileModel = value; OnPropertyChanged(); } }
        #endregion

        #region Icommand
        public ICommand LoadWindowCommand { get; set; }
        public ICommand SelectionMenuCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand PreviewTextInputCommand { get; set; }
        #endregion
        #region Menu ViewModel
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel { get { return _selectedViewModel; } set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); } }
        private SettingViewModel _SettingViewModel;
        public SettingViewModel SettingViewModel { get => _SettingViewModel; set { _SettingViewModel = value; OnPropertyChanged(); } }
        
        #endregion
        public FoundationPileViewModel(UIDocument uiDoc, Document doc, List<Element> columns)
        {
            #region
            UiDoc = uiDoc;
            Doc = doc;
            Columns = columns;
            Unit = GetUnitProject();
            FoundationPileModel = new FoundationPileModel(Doc, Unit);
            SettingViewModel = new SettingViewModel(Doc,FoundationPileModel);
            SelectedViewModel = SettingViewModel;
            #endregion
            #region Load
            LoadWindowCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                DrawMenu(p);
            });
            SelectionMenuCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                switch (p.Menu.SelectedIndex)
                {
                    case 0:
                        SelectedViewModel = SettingViewModel;
                        break;
                    default:
                        break;
                }
            });
            CancelCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            #endregion
        }
        #region Get Property Method

        private UnitProject GetUnitProject()
        {
            UnitProject a = new UnitProject(1, "ft");
            ForgeTypeId forgeTypeId = Doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId();
            if (forgeTypeId == UnitTypeId.Centimeters)
            {
                a.UnitInt = 1; a.UnitName = "cm";
            }
            if (forgeTypeId == UnitTypeId.Decimeters)
            {
                a.UnitInt = 2; a.UnitName = "dm";
            }
            if (forgeTypeId == UnitTypeId.Feet)
            {
                a.UnitInt = 3; a.UnitName = "ft";
            }
            if (forgeTypeId == UnitTypeId.Inches)
            {
                a.UnitInt = 4; a.UnitName = "in";
            }
            if (forgeTypeId == UnitTypeId.Meters)
            {
                a.UnitInt = 5; a.UnitName = "m";
            }
            if (forgeTypeId == UnitTypeId.Millimeters)
            {
                a.UnitInt = 6; a.UnitName = "mm";
            }
            if (forgeTypeId == UnitTypeId.Inches)
            {
                a.UnitInt = 7; a.UnitName = "inUS";
            }
            if (forgeTypeId == UnitTypeId.FeetFractionalInches)
            {
                a.UnitInt = 8; a.UnitName = "ft-in";
            }
            if (forgeTypeId == UnitTypeId.FractionalInches)
            {
                a.UnitInt = 9; a.UnitName = "inch";
            }
            if (forgeTypeId == UnitTypeId.MetersCentimeters)
            {
                a.UnitInt = 10; a.UnitName = "m";
            }
            return a;
        }
        #endregion
        #region Draw Menu
        private void DrawMenu(FoundationPileWindow p)
        {
            DrawIcon.DrawSetting(p.SettingCanvas);
            DrawIcon.DrawGeometry(p.GeometryCanvas);
            DrawIcon.DrawBarsStirrups(p.ReinforcementCanvas, true);
            DrawIcon.DrawPileDetail(p.PileDetailCanvas);
        }
        #endregion
    }
}
