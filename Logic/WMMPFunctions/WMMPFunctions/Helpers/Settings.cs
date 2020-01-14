using System;
using System.Collections.Generic;
using System.Text;

namespace WMMPFunctions
{
    public class Settings
    {
        public string Value(string name, bool isExpected = true, string defaultValue = null)
        {
            string result = Environment.GetEnvironmentVariable(name);
            bool isSuccess = (result != null);

            if (isSuccess)
            {
                return result;
            }
            else if (defaultValue != null)
            {
                return defaultValue;
            }
            else if (isExpected)
            {
                throw new Exception($"Expected setting '{name}' has no value");
            }
            else
            {
                throw new Exception($"Setting '{name}' has no valid value and no default value was specified");
            }
        }

        public bool BoolValue(string name, bool isExpected = true, bool? defaultValue = null)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (value != null)
            {
                if (value == "1" || value.ToLower() == "yes" || value.ToLower() == "true")
                {
                    return true;
                }
                if (value == "0" || value.ToLower() == "no" || value.ToLower() == "false")
                {
                    return false;
                }
            }
            bool isSuccess = bool.TryParse(value, out bool result);

            if (isSuccess)
            {
                return result;
            }
            else if (defaultValue != null)
            {
                return (bool)defaultValue;
            }
            else if (isExpected)
            {
                throw new Exception($"Expected setting '{name}' has no value");
            }
            else
            {
                throw new Exception($"Setting '{name}' has no valid value and no default value was specified");
            }
        }
    }
}
