using System;
using System.Management;
namespace zedilabs.com
{
   public static class PrinterOffline
   {
      public static bool GetDymoPrinter()
      {
         // Set management scope
         ManagementScope scope = new ManagementScope(@"\root\cimv2");
         scope.Connect();

         // Select Printers from WMI Object Collections
         ManagementObjectSearcher searcher = new
         ManagementObjectSearcher("SELECT * FROM Win32_Printer");

         string printerName = "";
         foreach (ManagementObject printer in searcher.Get())
         {
            printerName = printer["Name"].ToString().ToLower();
            if (printerName.Contains(@"dymo"))
            {
               Console.WriteLine("Printer = " + printer["Name"]);
               if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
               {
                  // printer is offline by user
                  Console.WriteLine("Your Plug-N-Play printer is not connected.");
                  return (false);
               }
               else
               {
                  // printer is not offline
                  Console.WriteLine("Your Plug-N-Play printer is connected.");
                  return (true);
               }
            }
         }
         return (false);
      }
   }
}