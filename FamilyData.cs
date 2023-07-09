using Newtonsoft.Json;
using System.Collections.Generic;

namespace Optellix_Assignment
{
    /// <summary>
    /// This Class Represents the data for a specific family in the structured layer information.
    /// </summary>
    public class FamilyData
    {
        #region FamilyData Class Members

        /// <summary>
        /// Gets or sets the category of the family.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the name of the family.
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets the count of instances for the family.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the family in feet (optional).
        /// </summary>
        [JsonProperty("Thickness (ft)", NullValueHandling = NullValueHandling.Ignore)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the volume of the family in cubic feet (optional).
        /// </summary>
        [JsonProperty("Volume (c.ft)", NullValueHandling = NullValueHandling.Ignore)]
        public double Volume { get; set; }

        /// <summary>
        /// Gets or sets the list of materials for the family (optional).
        /// </summary>
        [JsonProperty("Materials", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Materials { get; set; }

        /// <summary>
        /// Gets or sets the list of material thicknesses in feet (optional).
        /// </summary>
        [JsonProperty("Material Thicknesses (ft)", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> MaterialThicknesses { get; set; }

        /// <summary>
        /// Gets or sets the list of material volumes in cubic feet (optional).
        /// </summary>
        [JsonProperty("Material Volumes (c.ft)", NullValueHandling = NullValueHandling.Ignore)]
        public List<double> MaterialVolumes { get; set; }

        /// <summary>
        /// Gets or sets the area of the family in square feet (optional).
        /// </summary>
        [JsonProperty("Area (sq.ft)", NullValueHandling = NullValueHandling.Ignore)]
        public double Area { get; set; }

        #endregion
    }
}
