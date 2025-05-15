using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoBrew
{
    class LanguageManager
    {
        // Default language is English
        private static string currentLanguage = "en-US";

        //get or set the current language
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                currentLanguage = value;
                ApplyLanguage();
            }
        }

        
        public static void ApplyLanguage()
        {
            //apply the language to the form
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(currentLanguage);
        }

        //change language and refresh the current form
        public static void ChangeLanguage(string languageCode, Form currentForm)
        {
            //new language
            CurrentLanguage = languageCode;
            //rfresh the form
            RefreshForm(currentForm);
        }

        //refresh form after language change
        public static void RefreshForm(Form form)
        {
            //keep the current form's location and size
            var location = form.Location;
            var size = form.Size;

            //clear all controls
            form.Controls.Clear();

            //initialize the form again (reload)
            form.GetType().GetMethod("InitializeComponent", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(form, null);

            //restore the form's location and size
            form.Location = location;
            form.Size = size;
        }
    }
}
