using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R11_FoundationPile
{
    public class SelectedIndexModel    :BaseViewModel
    {
        private int _SelectedIndexPileCategory;
        public int SelectedIndexPileCategory
        { 
            get => _SelectedIndexPileCategory; 
            set
            {
                _SelectedIndexPileCategory = value; 
                OnPropertyChanged(); 
            } 
        }
        private int _SelectedIndexPileFamily;
        public int SelectedIndexPileFamily
        { 
            get => _SelectedIndexPileFamily;
            set 
            { 
                _SelectedIndexPileFamily = value; 
                OnPropertyChanged(); 
            } 
        }
        private int _SelectedIndexPileFamilyType;
        public int SelectedIndexPileFamilyType
        {
            get => _SelectedIndexPileFamilyType;
            set
            {
                _SelectedIndexPileFamilyType = value;
                OnPropertyChanged();
            }
        }
        public SelectedIndexModel()
        {
            SelectedIndexPileCategory = 0;
            SelectedIndexPileFamily = 0;
            SelectedIndexPileFamilyType = 0;
        }
    }
}
