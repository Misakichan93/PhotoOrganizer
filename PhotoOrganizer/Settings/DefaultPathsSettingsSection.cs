using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoOrganizer.Settings
{
    [ConfigurationCollection(typeof(DefaultPathsSettingsSection))]
    public sealed class DefaultPathsSettingsSection : ConfigurationSection
    {
        public  DefaultPathsSettingsSection()
        {
        }
    }
}
