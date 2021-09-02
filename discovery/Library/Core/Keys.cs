using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    //Contains all the key names are using throughth the app
    public class Keys
    {
        //Pattern categories related keys
        public const string _STRUCTURAL = "Strucutural";
        public const string _BEHAVIORAL = "Behavioral";
        public const string _CREATIONAL = "Creational";
        public const string _CONCURRENCY = "Concurrency";

        //Scenrario Related keys
        public const string _AIBASED = "AIBased";
        public const string _CONVENTIONAL = "Conventional";
        public const string _FILE = "File";
        public const string _EMAIL = "Email";
        public const string _SERVICE = "Service";
        public const string _CREATED = "Created";
        public const string _DOWNLOADED = "Downloaded";
        public const string _IMPORTTED = "Importted";
        public const string _ANALYZED = "Anayzed";

        public const string _REMOTEADDRESS = "https://mail.python.org/pipermail/python-dev/";
        public const string _REMOTEURL = "remoteURL";
        public const string _TEMPDIRECTORY = "wwwroot/temp/";
        public const string _SCENARIODIRECTORY = "wwwroot/temp/scenarios";

        public const string _CURRENTSCENARIO = "currentScenario";
        public const string _CURRENTUSER = "currentUser";

        public const string _MSG = "Message";

        public const string _DATEREGEXPRESSION = "(?:\\d|\\d{2}) \\b(?:Jan(?:uary)?|Feb(?:ruary)?|Mar(?:ch)?|Apr(?:il)?|May?|Jun(?:e)?|Jul(?:y)?|Aug(?:ust)?|Sep(?:tember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?) (?:19[7-9]\\d|2\\d{3})(?=\\D|$)";
        public const string _MONTHREGEXPRESSION = "\\b(?:Jan(?:uary)?|Feb(?:ruary)?|Mar(?:ch)?|Apr(?:il)?|May?|Jun(?:e)?|Jul(?:y)?|Aug(?:ust)?|Sep(?:tember)?|Oct(?:ober)?|Nov(?:ember)?|Dec(?:ember)?)";

        public const string _TXT = ".txt";
        public const string _SCENARIORESET = "Reset";
        public const string _SCENARIOCREATE= "Create";
        public const string _SCENARIODELETED = "Delete";
        public const string _SCENARIODELETE = "Scenario has successfully been deleted";
        public const string _SCENARIORESETT = "Scenario has successfully been reset";
        public const string _SCENARIOSTOP = "Scenario has successfully Stopped";
        public const string _SCENARIOSTART = "Scneario has successfully Started";
        public const string _SCENARIODELETETITLE = "Delete Scenario";
        public const string _SCENARIORESETTITLE = "Reset Scenario";
        public const string _SCENARIODETAILSTITLE = "View Scenario Details";
        public const string _SCENARIOEDITTITLE = "Edit Scenario";
    }
}
