
#region Namespaces
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using R11_FoundationPile.Library.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion


namespace R11_FoundationPile.Command
{
    [Transaction(TransactionMode.Manual)]
    public class FoundationPileCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // code
            List<Reference> references = null;
            try
            {
                references = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, new StructuralColumnSelectionFilter()).ToList();
                List<Element> columns = new List<Element>();
                foreach (var item in references)
                {
                    columns.Add(doc.GetElement(item));
                }
                columns = columns.Where(x => ErrorColumns.GetSectionStyle(doc, x) != ErrorColumns.SectionStyle.ORTHER).ToList();
                //FloorType family = new FilteredElementCollector(doc)
                //    .OfClass(typeof(FloorType))
                //    .Cast<FloorType>()
                //    .Where(x => x.IsFoundationSlab).FirstOrDefault();
                //List<Family> family = new FilteredElementCollector(doc)
                //.OfClass(typeof(Family))
                //    .Cast<Family>()
                //    .Where(x => x.FamilyCategory.Name.Equals("Structural Foundations"))
                //    .ToList();
                //System.Windows.Forms.MessageBox.Show("Test"+ family.Name);
                using (TransactionGroup transGr = new TransactionGroup(doc))
                {
                    transGr.Start("RAPI00TransGr");

                    FoundationPileViewModel viewModel = new FoundationPileViewModel(uidoc, doc, columns);
                    FoundationPileWindow window = new FoundationPileWindow(viewModel);
                    if (window.ShowDialog() == false) return Result.Cancelled;

                    transGr.Assimilate();
                }

                return Result.Succeeded;
            }
            catch (System.Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.Message);
                return Result.Cancelled;
            }
          

           

        }
    }
}
