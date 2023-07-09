using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Autodesk.Revit.DB;
using System.IO;

namespace Optellix_Assignment
{
    /// <summary>
    /// Interaction logic for Structured_Layer_Info.xaml
    /// </summary>
    public partial class Structured_Layer_Info : Window
    {
        #region Class Members
        
        Document doc = null;

        /// <summary>
        /// Parameterized Constructor of this Class which takes Document as Parameter
        /// </summary>
        /// <param name="doc">This Parameter is Passed From the Class which is inherited
        /// by the IExternalCommand Interface</param>
        public Structured_Layer_Info(Document doc)
        {
            InitializeComponent();
            this.doc = doc;
        }
        /// <summary>
        /// When WPF UI is Loaded this Method is Called.
        /// </summary>
        /// <param name="sender">This Parameter represents the object that raises the event</param>
        /// <param name="e">This parameter contains information about the event that occurred</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFamilyData();
        }
        /// <summary>
        /// This Method is Invoked When an Interaction Occured to DataGrid
        /// </summary>
        /// <param name="sender">This Parameter represents the object that raises the event</param>
        /// <param name="e">Provides Data for Mouse Button Related Events</param>
        private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// This Method is Invoked When Mouse Wheel is Scrolled Up and DownWards
        /// </summary>
        /// <param name="sender">This Parameter represents the object that raises the event</param>
        /// <param name="e">Provides Data for Mouse Button Related Events</param>
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualParent<ScrollViewer>(sender as DependencyObject);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }
        /// <summary>
        /// Finds the parent control of a specific type in the visual tree hierarchy.
        /// </summary>
        /// <typeparam name="T">The type of the parent control to find.</typeparam>
        /// <param name="child">The child control whose parent needs to be found.</param>
        /// <returns>The first parent control of the specified type, or null if not found.</returns>
        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            ///Traverses up the visual tree hierarchy to find the first parent control of type T.
            ///Returns null if no parent control of the specified type is found.

            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;

            return FindVisualParent<T>(parentObject);
        }
        /// <summary>
        /// This Method is Invoked When Button Click Event is Raised
        /// </summary>
        /// <param name="sender">This Parameter represents the object that raises the event</param>
        /// <param name="e">This parameter contains information about the event that occurred</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<FamilyData> familyDataList = dataGrid.ItemsSource as List<FamilyData>;
            if (familyDataList != null)
            {
                ///Shows folder selection dialog
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string folderPath = dialog.SelectedPath;

                    ///Shows save file dialog to get the JSON file name
                    var saveDialog = new Microsoft.Win32.SaveFileDialog();
                    saveDialog.Filter = "JSON Files (*.json)|*.json";
                    saveDialog.DefaultExt = ".json";
                    saveDialog.InitialDirectory = folderPath;
                    saveDialog.FileName = "Structured Layered Info";

                    if (saveDialog.ShowDialog() == true)
                    {
                        string filePath = saveDialog.FileName;

                        ///Exports the JSON file
                        ExportToJson(familyDataList, filePath);

                        MessageBox.Show("Structured Layered Data Exported to JSON file.");
                    }
                }
            }

        }
        /// <summary>
        /// This Method Loads the Family Data from the Current Document
        /// </summary>
        private void LoadFamilyData()
        {
            List<FamilyData> familyDataList = GetFamilyData();
            dataGrid.ItemsSource = familyDataList;
        }

        /// <summary>
        /// This Method Gets the Family Data into List.
        /// </summary>
        /// <returns>A list of FamilyData objects representing the collected family data.</returns>
        private List<FamilyData> GetFamilyData()
        {
            List<FamilyData> familyDataList = new List<FamilyData>();

            ///Filters Elements from the Current Document are Collected
            FilteredElementCollector fec = new FilteredElementCollector(doc, doc.ActiveView.Id).WhereElementIsNotElementType();

            ///Collects the Elements from the Collector
            List<Element> elementsindoc = fec.ToList();

            /// Retrieves family data by grouping elements based on their names and collecting relevant information.
            var groupedFamilies = elementsindoc
                .Where(element => element.Category != null)
                .GroupBy(element => element.Name)
                .Select(group => new
                {
                    Category = group.First().Category?.Name,
                    Family = group.Key,
                    Elements = group.ToList()
                });

            foreach (var groupedFamily in groupedFamilies)
            {
                FamilyData familyData = new FamilyData
                {
                    Category = groupedFamily.Category,
                    Family = groupedFamily.Family,
                    Count = groupedFamily.Elements.Count,
                    Thickness = 0,
                    Volume = 0,
                    Materials = new List<string>(),
                    MaterialThicknesses = new List<double>(),
                    MaterialVolumes = new List<double>(),
                    Area = 0
                };

                foreach (Element element in groupedFamily.Elements)
                {
                    ///Retrieves the following Data from the element Prameters
                    double volume = ConvertToFeet(element.LookupParameter("Volume")?.AsDouble() ?? 0);
                    double area = ConvertToFeet(element.LookupParameter("Area")?.AsDouble() ?? 0);
                    Parameter thicknessParam = element.LookupParameter("Thickness");
                    if (thicknessParam != null && thicknessParam.HasValue)
                    {
                        familyData.Thickness = thicknessParam.AsDouble();
                    }
                    familyData.Volume += volume;
                    familyData.Area += area;

                    ///Collects all the MaterialId's Which Excluding Non Paint Materials.
                    ICollection<ElementId> materialIds = element.GetMaterialIds(false);

                    ///Gets the Material as Element from the MaterialId in the ElementId Icollection
                    foreach (ElementId ids in materialIds)
                    {
                        Element material = doc.GetElement(ids);
                        double materialArea = ConvertToFeet(element.GetMaterialArea(ids, false));
                        double materialVolume = ConvertToFeet(element.GetMaterialVolume(ids));

                        familyData.Materials.Add(material.Name);
                        familyData.MaterialThicknesses.Add(materialArea != 0 ? materialVolume / materialArea : 0);
                        familyData.MaterialVolumes.Add(materialVolume);
                    }
                }

                familyDataList.Add(familyData);
            }

            return familyDataList;
        }
        /// <summary>
        /// Converts a value from internal units to feet.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value in feet.</returns>
        private double ConvertToFeet(double value)
        {
            ForgeTypeId feetTypeId = UnitTypeId.Feet;
            return UnitUtils.ConvertFromInternalUnits(value, feetTypeId);
        }
        /// <summary>
        /// Exports the family data to a JSON file.
        /// </summary>
        /// <param name="familyDataList">The list of FamilyData objects to export.</param>
        /// <param name="filePath">The file path of the JSON file.</param>
        private void ExportToJson(List<FamilyData> familyDataList, string filePath)
        {
            string json = JsonConvert.SerializeObject(familyDataList, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        #endregion
    }
}

