using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Optellix_Assignment
{
    /// <summary>
    /// Class Reads and Passes Document form Command Data to Window Class
    /// </summary>
    [Transaction(TransactionMode.ReadOnly)]
    internal class StructuredLayerInformation : IExternalCommand
    {
        #region
        /// <summary>
        /// Executes the external command.
        /// </summary>
        /// <param name="commandData">An ExternalCommandData object containing references to the application and current document.</param>
        /// <param name="message">A message that can be set to provide additional information.</param>
        /// <param name="elements">A set of elements that can be modified by the command.</param>
        /// <returns>A Result indicating the outcome of the command execution.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            ///WPF Class Receives the Current Document and Displays the Information.
            Structured_Layer_Info SLI = new Structured_Layer_Info(doc);
            SLI.Show();

            return Result.Succeeded;
        }
        #endregion
    }
}
