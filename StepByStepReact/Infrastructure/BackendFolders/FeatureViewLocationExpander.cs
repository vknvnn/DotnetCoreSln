using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;

namespace StepByStepReact.Infrastructure.BackendFolders
{
    public class BackendViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {

        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            // {0} - Action Name
            // {1} - Controller Name
            // {2} - Area name

            //Backend
            yield return "/Backend/{1}/{0}.cshtml";
            yield return "/Backend/{1}/Views/{0}.cshtml";

            //Backend Areas
            yield return "/Backend/{2}/{1}/{0}.cshtml";
            yield return "/Backend/{2}/{1}/Views/{0}.cshtml";
            yield return "/Backend/{2}/Shared/{0}.cshtml";

            //Shared
            yield return "/Backend/Shared/{0}.cshtml";
        }
    }
}
