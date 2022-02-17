using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R11_FoundationPile
{
    public class SettingModel : BaseViewModel
    {
        private List<string> _CategoryPiles;
        public List<string> CategoryPiles
        {
            get
            {
                if (_CategoryPiles == null)
                { _CategoryPiles = new List<string>() { "Structural Columns", "Structural Foundations" }; }
                return _CategoryPiles;
            }
            set
            {
                _CategoryPiles = value; 
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Family> _FamilyPiles;
        public ObservableCollection<Family> FamilyPiles
        {
            get => _FamilyPiles;
            set
            {
                _FamilyPiles = value; 
                OnPropertyChanged();
            }
        }
        private ObservableCollection<FamilySymbol> _FamilySymbolPile;
        public ObservableCollection<FamilySymbol> FamilySymbolPile
        {
            get => _FamilySymbolPile;
            set
            {
                _FamilySymbolPile = value;
                OnPropertyChanged();
            }
        }
        private double _DiameterPile;
        public double DiameterPile
        {
            get => _DiameterPile;
            set
            {
                _DiameterPile = value;
                OnPropertyChanged();
            }
        }
        public SettingModel()
        {

        }
        public void GetFamilyPile(Document doc ,string category)
        {
            FamilyPiles = new ObservableCollection<Family>(new FilteredElementCollector(doc).OfClass(typeof(Family))
                .Cast<Family>().Where(x => x.FamilyCategory.Name.Equals(category)).ToList());
        }
        public void GetAllFamilySymbol(Family family)
        {
            FamilySymbolPile = new ObservableCollection<FamilySymbol>();
            foreach (ElementId familySymbolId in family.GetFamilySymbolIds())
            {
                FamilySymbol familySymbol = family.Document.GetElement(familySymbolId) as FamilySymbol;
                FamilySymbolPile.Add(familySymbol);
            }
        }
        public void GetDiameterPile(Document doc ,FamilySymbol familySymbol)
        {
            if (familySymbol != null)
            {
                
                ElementType elementType = doc.GetElement(familySymbol.Id) as ElementType;
                Parameter bP = elementType.LookupParameter("b");
                Parameter hP = elementType.LookupParameter("h");
                if (bP == null || hP == null)
                {
                    Parameter dP = elementType.LookupParameter("Dp");
                    if (dP == null)
                    {
                        DiameterPile = 0;
                    }
                    else
                    {
                        DiameterPile = double.Parse(UnitFormatUtils.Format(doc.GetUnits(), SpecTypeId.Length, elementType.LookupParameter("b").AsDouble(), false));
                    }
                }
                else
                {
                    double b = double.Parse(UnitFormatUtils.Format(doc.GetUnits(), SpecTypeId.Length, bP.AsDouble(), false));
                    double h = double.Parse(UnitFormatUtils.Format(doc.GetUnits(), SpecTypeId.Length, hP.AsDouble(), false));
                    DiameterPile = Math.Max(b, h);
                }
            }
        }

    }
}
