#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
#endregion

namespace TabAndButton
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            /*
            // Create a custom ribbon tab
            String tabName = "This Tab Name";
            application.CreateRibbonTab(tabName);

            // Create two push buttons
            PushButtonData button1 = new PushButtonData("Button1", "My Button #1",
                @"C:\ExternalCommands.dll", "Revit.Test.Command1");
            PushButtonData button2 = new PushButtonData("Button2", "My Button #2",
                @"C:\ExternalCommands.dll", "Revit.Test.Command2");

            // Create a ribbon panel
            RibbonPanel m_projectPanel = application.CreateRibbonPanel(tabName, "This Panel Name");

            // Add the buttons to the panel
            List<RibbonItem> projectButtons = new List<RibbonItem>();
            projectButtons.AddRange(m_projectPanel.AddStackedItems(button1, button2));
            
            */

            RibbonPanel panel = ribbonPanel(a);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            //PushButton button = panel.AddItem(new PushButtonData("Button", "Test Button", thisAssemblyPath, "MyTest.Command")) as PushButton; // might need to be TabAndbutton.Command
            PushButton button = panel.AddItem(new PushButtonData("Button", "Test Button", thisAssemblyPath, "TabAndButton.Command")) as PushButton; // might need to be TabAndbutton.Command

            button.ToolTip = "this is a simple tooltip";
            var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "LACMA DOORS.PNG"); //need to load png

            Uri uriImage = new Uri(globePath);
            BitmapImage largeImage = new BitmapImage(uriImage);
            button.LargeImage = largeImage;

            


            a.ApplicationClosing += a_ApplicationClosing;

            // Set application idling
            a.Idling += a_Idling;

            return Result.Succeeded;
        }

        void a_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {

        }

        void a_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
        {
            throw new NotImplementedException();
        }


        public RibbonPanel ribbonPanel (UIControlledApplication a)
        {
            string tab = "My test tab";
            RibbonPanel ribbonPanel = null;
            try
            {
                
                //a.CreateRibbonPanel(tab);
                a.CreateRibbonTab(tab);
            }
            catch { }
            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "test");
            }
            catch { }

            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach(RibbonPanel p in panels)
            {
                if (p.Name == "test")
                {
                    ribbonPanel = p;
                }
            }
            return ribbonPanel;
        }




        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
