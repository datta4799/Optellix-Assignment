using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.PointClouds;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Optellix_Assignment
{
    /// <summary>
    /// Entry Point for creating polygonwall
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class CreatePolygonWall : IExternalCommand
    {
        #region Create Polygon Wall Methods

        /// <summary>
        /// Method returns the Path
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            string Path = Assembly.GetExecutingAssembly().Location;
            return Path;
        }
        /// <summary>
        /// Executes the command logic to create a polygon wall with specified parameters.
        /// </summary>
        /// <param name="commandData">The external command data.</param>
        /// <param name="message">A message returned by the command.</param>
        /// <param name="elements">The elements affected by the command.</param>
        /// <returns>The result of the command execution.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;
            
            
            using (Transaction trans = new Transaction(doc,"create Wall"))
            {
                try
                {
                    trans.Start();

                    // Get the active view
                    View activeView = doc.ActiveView;
                    Level level = activeView.GenLevel;
                    ElementId levelId = level.Id;

                    // Prompt the user to select the center point of the circle
                    XYZ centerPoint = GetSelectedPoint(uiDoc);

                    // Prompt the user to select the radius point of the circle
                    XYZ radiusPoint = GetSelectedPoint(uiDoc);

                    // Calculate the radius length
                    double radius = centerPoint.DistanceTo(radiusPoint);

                    // Create the circle
                    Plane plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, centerPoint);

                    // Divide the circle into five equal parts
                    int divisionCount = 5;
                    double divisionAngle = 2 * Math.PI / divisionCount;

                    // Store the points in a list
                    List<XYZ> pentagonPoints = new List<XYZ>();

                    // Calculate points along the circle's circumference
                    for (int i = 0; i < divisionCount; i++)
                    {
                        double angle = divisionAngle * i;
                        double x = centerPoint.X + radius * Math.Cos(angle);
                        double y = centerPoint.Y + radius * Math.Sin(angle);
                        XYZ pointOnCircle = new XYZ(x, y, centerPoint.Z);
                        pentagonPoints.Add(pointOnCircle);
                    }

                    // Create lines between the points to form a pentagon
                    for (int i = 0; i < divisionCount; i++)
                    {
                        XYZ startPoint = pentagonPoints[i];
                        XYZ endPoint = pentagonPoints[(i + 1) % divisionCount];

                        // Create a new line element
                        Line line = Line.CreateBound(startPoint, endPoint);

                        Wall.Create(doc, line, levelId, true);
                    }

                    trans.Commit();
                }
                catch (Exception ex) { TaskDialog.Show("Error", ex.Message.ToString()); }
                
            }

            return Result.Succeeded;
        }
        /// <summary>
        /// returns the selection of points in the Uidocument
        /// </summary>
        /// <param name="uidoc">The Current Uidocument</param>
        /// <returns></returns>
        private XYZ GetSelectedPoint(UIDocument uidoc)
        {
            // Get the selected point
            XYZ selectedPoint = uidoc.Selection.PickPoint();

            return selectedPoint;
        }

        #endregion
    }
}
