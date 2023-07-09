using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace Optellix_Assignment
{
    /// <summary>
    /// Entry Point for creating polygonwall
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    internal class CreatePolygonWall : IExternalCommand
    {
        #region Create Polygon Wall Methods

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
                      
            using(Transaction trans = new Transaction(doc,"Initialize the Form"))
            {
                trans.Start();
                Polygon_Wall_Form Pwform = new Polygon_Wall_Form(commandData);
                Pwform.ShowDialog();
                ElementId wallTypeId = Pwform.wallelementId;
                bool StructuralWall = Pwform.structuralwall;
                Pwform.Dispose();
                {
                    if(wallTypeId != null) 
                    {
                        try
                        {
                            View activeView = doc.ActiveView;
                            Level level = activeView.GenLevel;
                            ElementId levelId = level.Id;

                            ///Prompt the user to select the center point of the circle
                            XYZ centerPoint = GetSelectedPoint(uiDoc);

                            ///Prompt the user to select the radius point of the circle
                            XYZ radiusPoint = GetSelectedPoint(uiDoc);

                            ///Calculate the radius length
                            double radius = centerPoint.DistanceTo(radiusPoint);

                            ///Create the circle
                            Plane plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, centerPoint);
                            activeView.Dispose();
                            plane.Dispose();
                            level.Dispose();

                            ///Divide the circle into five equal parts
                            int divisionCount = 5;
                            double divisionAngle = 2 * Math.PI / divisionCount;

                            double angleOrientationOffset = Math.PI / 2; /// 90 degrees
                            double baseAngle = Math.Atan2(radiusPoint.Y - centerPoint.Y, radiusPoint.X - centerPoint.X) + angleOrientationOffset;

                            ///Store the points in a list
                            List<XYZ> pentagonPoints = new List<XYZ>();

                            ///Calculate points along the circle's circumference
                            for (int i = 0; i < divisionCount; i++)
                            {
                                double angle = baseAngle + divisionAngle * i;

                                double x = centerPoint.X + radius * Math.Cos(angle);
                                double y = centerPoint.Y + radius * Math.Sin(angle);
                                XYZ pointOnCircle = new XYZ(x, y, centerPoint.Z);
                                pentagonPoints.Add(pointOnCircle);
                            }
                            
                            for (int i = 0; i < divisionCount; i++)
                            {
                                XYZ startPoint = pentagonPoints[i];
                                XYZ endPoint = pentagonPoints[(i + 1) % divisionCount];

                                ///Create a new line element
                                Line line = Line.CreateBound(startPoint, endPoint);
                                Wall.Create(doc, line, wallTypeId, levelId, 7, 0, false, StructuralWall);
                            }
                            
                        }
                        catch (Exception ex) { TaskDialog.Show("Error", ex.Message.ToString()); }
                    }
                    
                    trans.Commit();
                }
            }
            return Result.Succeeded;
        }
        /// <summary>
        /// returns the selection of points in the Uidocument
        /// </summary>
        /// <param name="uidoc">The Current Uidocument</param>
        /// <returns>XYZ point from the selection</returns>
        private XYZ GetSelectedPoint(UIDocument uidoc)
        {
            /// Get the selected point
            XYZ selectedPoint = uidoc.Selection.PickPoint();
            return selectedPoint;
        }
        #endregion
    }
}
