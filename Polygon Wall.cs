using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;


namespace Optellix_Assignment
{
    /// <summary>
    /// Entry Point for Form Class
    /// </summary>
    public partial class Polygon_Wall_Form : System.Windows.Forms.Form
    {
        #region Fields and Methods of this class

        ExternalCommandData commandData = null;
        public bool structuralwall;
        
        private ElementId walltypeid = null;
        /// <summary>
        /// wallType selected by the user from the List box will be passed into this Property
        /// and called in Execute Method
        /// </summary>
        public ElementId wallelementId { get { return walltypeid; } set { walltypeid = value; } }

        /// <summary>
        /// Constructor which takes CommandData as Parameter
        /// </summary>
        /// <param name="commandData">this is from the Exectue Method</param>
        public Polygon_Wall_Form(ExternalCommandData commandData)
        {
            InitializeComponent();
            this.commandData = commandData;
        }
        /// <summary>
        /// when Form is loded then it loads the items or tools written in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Polygon_Wall_Form_Load(object sender, EventArgs e)
        {
            if (WallsListBox.Items.Count == 0)
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                FilteredElementCollector fec = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls);
                FilteredElementCollector stackwall = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StackedWalls);
                List<WallType> wallTypes = fec.OfClass(typeof(WallType)).Cast<WallType>().ToList();
                List<WallType>stackwalllist = stackwall.OfClass(typeof(WallType)).Cast<WallType>().ToList();
                foreach (WallType wall in wallTypes)
                {
                    WallsListBox.Items.Add(wall.Name);
                }
                foreach (WallType wall in stackwalllist)
                {
                    WallsListBox.Items.Add(wall.Name);
                }
            }
        }
        /// <summary>
        /// Method for Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateWallBtn_Click(object sender, EventArgs e)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;
  
            string selectedwall = WallsListBox.SelectedItem.ToString();

            if (WallsListBox.SelectedItems.Count == 0)
            {
                TaskDialog.Show("Error", "Select a Wall");
            }
            /// Retrieve the wall type based on its name 
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(WallType));
            IEnumerable<WallType> wallTypes = collector.Cast<WallType>().Where(wt => wt.Name == selectedwall);

            if (wallTypes.Any())
            {
                ///Get the wall type ID
                wallelementId = wallTypes.First().Id;
                if (wallelementId == null)
                {
                    TaskDialog.Show("Error", "WallType Id Not Found");
                }
                else
                {
                    if (checkBoxSW.Checked)
                    {
                        structuralwall = true;
                    }
                    else 
                    { 
                        structuralwall = false; 
                    }
                    this.Close();
                }
            }
        }
        #endregion
    }
}
