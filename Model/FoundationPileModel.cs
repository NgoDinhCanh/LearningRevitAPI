using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R11_FoundationPile
{
    public class FoundationPileModel  :BaseViewModel
    {
        #region Property
        private SettingModel _SettingModel;
        public SettingModel SettingModel { get => _SettingModel; set { _SettingModel = value; OnPropertyChanged(); } }
        private SelectedIndexModel _SelectedIndexModel;
        public SelectedIndexModel SelectedIndexModel { get => _SelectedIndexModel; set { _SelectedIndexModel = value; OnPropertyChanged(); } }
        #endregion
        public FoundationPileModel(Document  document, UnitProject unit)
        {
            GetSettingModel();
        }
        #region   Method
        private void GetSettingModel()
        {
            SettingModel = new SettingModel();
            SelectedIndexModel = new SelectedIndexModel();
        }
        #endregion

    }
}
