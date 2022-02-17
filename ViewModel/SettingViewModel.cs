using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Windows.Input;

namespace R11_FoundationPile.ViewModel
{
    public class SettingViewModel : BaseViewModel
    {
        public Document Doc;
        private FoundationPileModel _FoudationPileModel;
        public FoundationPileModel FoundationPileModel
        {
            get => _FoudationPileModel;
            set 
            {
                _FoudationPileModel = value;
                OnPropertyChanged();
            }
            
        }
        private string _SelectedCategoryPile;
        public string SelectedCategoryPile
        {
            get => _SelectedCategoryPile;
            set
            {
                _SelectedCategoryPile = value;
                OnPropertyChanged();
            }
        }
        private Family _SelectedPileFamily;
        public Family SelectedPileFamily
        {
            get => _SelectedPileFamily;
            set
            {
                _SelectedPileFamily = value;
                OnPropertyChanged();
            }
        }
        private FamilySymbol _SelectedPileFamilyType;
        public FamilySymbol SelectedPileFamilyType
        {
            get => _SelectedPileFamilyType;
            set
            {
                _SelectedPileFamilyType = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadSettingViewCommand { get; set; }
        public ICommand SelectionChangedCategoryCommand { get; set; }
        public ICommand SelectionChangedFamilyCommand { get; set; }
        public ICommand SelectionChangedFamilyTypeCommand { get; set; }

        public SettingViewModel(Document doc , FoundationPileModel foundationPileModel)
        {
            Doc = doc;
            FoundationPileModel = foundationPileModel;
            LoadSettingViewCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
             {
                 FoundationPileModel.SettingModel.GetFamilyPile(Doc, SelectedCategoryPile);
                 FoundationPileModel.SettingModel.GetAllFamilySymbol(SelectedPileFamily);
                 if (SelectedPileFamilyType != null)
                 {
                     FoundationPileModel.SettingModel.GetDiameterPile(Doc, SelectedPileFamilyType);
                 }
             });
            SelectionChangedCategoryCommand = new RelayCommand<FoundationPileWindow>((p) => { return true; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetFamilyPile(Doc, SelectedCategoryPile);
                FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamily = 0;
                
            });
            SelectionChangedFamilyCommand = new RelayCommand<FoundationPileWindow>((p) => { return SelectedPileFamily != null; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetAllFamilySymbol(SelectedPileFamily);
                FoundationPileModel.SelectedIndexModel.SelectedIndexPileFamilyType = 0;
                
            });
            SelectionChangedFamilyTypeCommand =new RelayCommand<FoundationPileWindow>((p) => { return SelectedPileFamilyType != null; }, (p) =>
            {
                FoundationPileModel.SettingModel.GetDiameterPile(Doc, SelectedPileFamilyType);
            });
        }



    }
}
