using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            
            return Result.Succeeded;
        }
        #endregion 
    }
}
