using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Autodesk.Revit.DB;

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
        private void LoadFamilyData()
        {
            List<FamilyData> familyDataList = GetFamilyData();
            dataGrid.ItemsSource = familyDataList;
        }


        private List<FamilyData> GetFamilyData()
        {
            List<FamilyData> familyDataList = new List<FamilyData>();

            // Retrieve all elements in the document
            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            IList<Element> elements = collector.WhereElementIsNotElementType().ToElements();

            // Group the elements by category and family name
            var groupedElements = elements
                .Where(element => element.Category != null)
                .GroupBy(element => element.Category.Name)
                .Select(group => new
                {
                    Category = group.Key,
                    Families = group.GroupBy(element => element.Name)
                        .Select(famGroup => new FamilyData
                        {
                            Category = group.Key,
                            Family = famGroup.Key,
                            Count = famGroup.Count(),
                            Volume = famGroup.Where(element => element.LookupParameter("Volume") != null).Sum(element => ConvertToFeet(element.LookupParameter("Volume").AsDouble()))
                        })
                });

            // Add the grouped elements to the familyDataList
            foreach (var group in groupedElements)
            {
                familyDataList.AddRange(group.Families);
            }

            return familyDataList;
        }




        public class FamilyData
        {
            public string Category { get; set; }
            public string Family { get; set; }
            public int Count { get; set; }
            public double Thickness { get; set; }
            public double Volume { get; set; }
            public List<string> Materials { get; set; }
            public List<double> MaterialThicknesses { get; set; }
            public List<double> MaterialVolumes { get; set; }
            public double Area { get; set; }
        }


        private double ConvertToFeet(double value)
        {

            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_DECIMAL_FEET);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }



}

