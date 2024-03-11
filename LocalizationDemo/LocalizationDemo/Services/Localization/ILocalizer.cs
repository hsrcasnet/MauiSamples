using System;
using System.Globalization;

namespace LocalizationDemo.Services.Localization
{
    /// <summary>
    /// Source: https://github.com/thomasgalliker/CrossPlatformLibrary/blob/master/CrossPlatformLibrary.Shared/Localization/ILocalizer.cs
    /// </summary>
    public interface ILocalizer
    {
        /// <summary>
        ///     This method must evaluate platform-specific locale settings
        ///     and convert them (when necessary) to a valid .NET locale.
        /// </summary>
        CultureInfo GetCurrentCulture();

        /// <summary>
        ///     CurrentCulture and CurrentUICulture must be set in the platform project,
        ///     because the Thread object can't be accessed in a PCL.
        /// </summary>
        void SetCultureInfo(CultureInfo cultureInfo);

        /// <summary>
        ///     Event is raised when the current culture info has changed.
        /// </summary>
        event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;
    }
}