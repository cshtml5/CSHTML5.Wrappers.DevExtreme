using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Wrappers.DevExtreme.Switch
{
	public class Configuration
	{
        /// <summary>
        /// Contains the path to the "dx.all.js" file to use for the control.
        /// </summary>
        public string LocationOfDXAllJS { get; set; }

        /// <summary>
        /// Contains the path to the "jquery.min.js" file to use for the control.
        /// </summary>
        public string LocationOfJquery { get; set; }

        /// <summary>
        /// Contains the path to the "dx.common.css" file to use for the control.
        /// </summary>
        public string LocationOfDXCommonCSS { get; set; }

        /// <summary>
        /// Contains the path to the "dx."theme name".css" file to use for the control.
        /// </summary>
        public string LocationOfDXThemeCSS { get; set; }

        internal bool AreSourcesSet
        {
            get
            {
                return (LocationOfDXAllJS != null && LocationOfDXCommonCSS != null && LocationOfDXThemeCSS != null && LocationOfJquery != null);
            }
        }
    }
}
