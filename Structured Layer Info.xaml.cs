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
        Document doc = null;
        public Structured_Layer_Info(Document doc)
        {
            InitializeComponent();
            this.doc = doc;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFamilyData();
        }
        private void DataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualParent<ScrollViewer>(sender as DependencyObject);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
                e.Handled = true;
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;

            return FindVisualParent<T>(parentObject);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<FamilyData> familyDataList = dataGrid.ItemsSource as List<FamilyData>;
            if (familyDataList != null)
            {
                ExportToJson(familyDataList);
            }

        }
        private void LoadFamilyData()
        {
            List<FamilyData> familyDataList = GetFamilyData();
            dataGrid.ItemsSource = familyDataList;
        }


        private List<FamilyData> GetFamilyData()
        {
            List<FamilyData> familyDataList = new List<FamilyData>();

            FilteredElementCollector fec = new FilteredElementCollector(doc, doc.ActiveView.Id).WhereElementIsNotElementType();
            List<Element> elementsindoc = fec.ToList();

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
                    double volume = ConvertToFeet(element.LookupParameter("Volume")?.AsDouble() ?? 0);
                    double area = ConvertToFeet(element.LookupParameter("Area")?.AsDouble() ?? 0);
                    Parameter thicknessParam = element.LookupParameter("Thickness");
                    if (thicknessParam != null && thicknessParam.HasValue)
                    {
                        familyData.Thickness = thicknessParam.AsDouble();
                    }
                    familyData.Volume += volume;
                    familyData.Area += area;

                    ICollection<ElementId> materialids = element.GetMaterialIds(false);

                    foreach (ElementId ids in materialids)
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













        public class FamilyData
        {
            public string Category { get; set; }
            public string Family { get; set; }
            public int Count { get; set; }

            [JsonProperty("Thickness (ft)", NullValueHandling = NullValueHandling.Ignore)]
            public double Thickness { get; set; }

            [JsonProperty("Volume (c.ft)", NullValueHandling = NullValueHandling.Ignore)]
            public double Volume { get; set; }

            [JsonProperty("Materials", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> Materials { get; set; }

            [JsonProperty("Material Thicknesses (ft)", NullValueHandling = NullValueHandling.Ignore)]
            public List<double> MaterialThicknesses { get; set; }

            [JsonProperty("Material Volumes (c.ft)", NullValueHandling = NullValueHandling.Ignore)]
            public List<double> MaterialVolumes { get; set; }

            [JsonProperty("Area (sq.ft)", NullValueHandling = NullValueHandling.Ignore)]
            public double Area { get; set; }
        }




        private double ConvertToFeet(double value)
        {
            ForgeTypeId feetTypeId = UnitTypeId.Feet;
            return UnitUtils.ConvertFromInternalUnits(value, feetTypeId);
        }


        private void ExportToJson(List<FamilyData> familyDataList)
        {
            string json = JsonConvert.SerializeObject(familyDataList, Formatting.Indented);
            string filePath = @"C:\Users\Asus\Desktop\Structured Layered Info.json";
            File.WriteAllText(filePath, json);
            MessageBox.Show("Structured Layered Data exported to JSON file.");
        }
        
    }



}

