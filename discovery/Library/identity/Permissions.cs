using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.identity
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
        }
        public static class dataset
        {
            public const string View = "Permissions.dataset.View";
            public const string Create = "Permissions.dataset.Create";
            public const string Edit = "Permissions.dataset.Edit";
            public const string Delete = "Permissions.dataset.Delete";
        }
        public static class patterns
        {
            public const string View = "Permissions.patterns.View";
            public const string Create = "Permissions.patterns.Create";
            public const string Edit = "Permissions.patterns.Edit";
            public const string Delete = "Permissions.patterns.Delete";
        }
        public static class categories
        {
            public const string View = "Permissions.categories.View";
            public const string Create = "Permissions.categories.Create";
            public const string Edit = "Permissions.categories.Edit";
            public const string Delete = "Permissions.categories.Delete";
        }
        public static class analyze
        {
            public const string View = "Permissions.analyze.View";
            public const string Create = "Permissions.analyze.Create";
            public const string Edit = "Permissions.analyze.Edit";
            public const string Delete = "Permissions.analyze.Delete";
        }
        public static class import
        {
            public const string View = "Permissions.import.View";
            public const string Create = "Permissions.import.Create";
            public const string Edit = "Permissions.import.Edit";
            public const string Delete = "Permissions.import.Delete";
        }
        public static class scenario
        {
            public const string View = "Permissions.scenario.View";
            public const string Create = "Permissions.scenario.Create";
            public const string Edit = "Permissions.scenario.Edit";
            public const string Delete = "Permissions.scenario.Delete";
        }
    }
}
