using System;
using System.Configuration;

namespace vuuvv.core.route
{
    public class RouteSection : ConfigurationSection
    {
        public RouteSection()
            : base()
        {
        }

        [ConfigurationProperty("Rules")]
        public RuleElementCollection rules
        {
            get
            {
                return (RuleElementCollection)this["Rules"];
            }
            set
            {
                this["Rules"] = value;
            }
        }

        [ConfigurationProperty("Ext")]
        public string ext
        {
            get
            {
                return (string)this["Ext"];
            }
            set
            {
                this["Ext"] = value;
            }
        }
    }

    public class RuleElement : ConfigurationElement
    {
        public RuleElement()
            : base()
        {
        }

        [ConfigurationProperty("Pattern")]
        public string Pattern
        {
            get
            {
                return (string)this["Pattern"];
            }
            set
            {
                this["Pattern"] = value;
            }
        }

        [ConfigurationProperty("Handler")]
        public string Handler
        {
            get
            {
                return (string)this["Handler"];
            }
            set
            {
                this["Handler"] = value;
            }
        }

        [ConfigurationProperty("Name")]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }
            set
            {
                this["Name"] = value;
            }
        }
    }

    public class RuleElementCollection : ConfigurationElementCollection
    {
        RuleElementCollection()
        {
            RuleElement rule = (RuleElement)CreateNewElement();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Pattern;
        }

        new public RuleElement this[string name]
        {
            get
            {
                return (RuleElement)BaseGet(name);
            }
        }

        public int IndexOf(RuleElement rule)
        {
            return BaseIndexOf(rule);
        }
    }
}
