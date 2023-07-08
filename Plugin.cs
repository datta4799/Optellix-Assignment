using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Optellix_Assignment
{
    
    /// <summary>
    /// Entry point for Revit
    /// </summary>
    public class Plugin : IExternalApplication
    {
        #region external application methods

        /// <summary>
        /// Calls when application shutdown
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        /// <summary>
        /// Calls when application starts
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string Path = Assembly.GetExecutingAssembly().Location;

            ///Create a button that executes CreatePolygonWall class's Execute method  
            if (panel.AddItem(new PushButtonData("Create Polygon Wall", "Polygon Wall", Path, "Optellix_Assignment.CreatePolygonWall"))
                is PushButton cpwbutton)
            {
                cpwbutton.ToolTip = "Create Polygon Wall";
            }
                
            panel.AddSeparator();

            if (panel.AddItem(new PushButtonData("Structured Layer Info", "Structured Layer Info", Path, "Optellix_Assignment.StructuredLayerInformation"))
                is PushButton slibutton)
            {
                slibutton.ToolTip = "Structured Layer Info";
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Method that creates RibbonPanel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public RibbonPanel RibbonPanel(UIControlledApplication app)
        {
            ///Tab Name
            string tab = "Optellix";

            RibbonPanel ribbonPanel = null;

            try
            {
                ///CreateTab
                app.CreateRibbonTab(tab);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error",ex.Message.ToString());
            }

            try
            {
                ///Create Panel Under the Tab
                RibbonPanel panel = app.CreateRibbonPanel(tab, "Assignments");
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message.ToString());
            }
            ///Get the Ribbon Panel with the Assigned name and return it.
            List<RibbonPanel> panels = app.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "Assignments"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;
        }

        #endregion
    }
}
