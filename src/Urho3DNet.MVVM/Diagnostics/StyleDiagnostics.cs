using System.Collections.Generic;
using Urho3DNet.MVVM.Styling;

namespace Urho3DNet.MVVM.Diagnostics
{
    /// <summary>
    /// Contains information about style related diagnostics of a control.
    /// </summary>
    public class StyleDiagnostics
    {
        /// <summary>
        /// Currently applied styles.
        /// </summary>
        public IReadOnlyList<IStyleInstance> AppliedStyles { get; }

        public StyleDiagnostics(IReadOnlyList<IStyleInstance> appliedStyles)
        {
            AppliedStyles = appliedStyles;
        }
    }
}