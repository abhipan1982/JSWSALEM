using PE.Interfaces.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE.TcpProxy.Communication
{
  public static class ExternalAdapterHandler
  {
    public static bool SwithForMessages(DCExtCommonMessage message)
    {
      switch (message.TelegramID)
      {
        case (int)PE.Core.Constants.AuxEquipmentTelegrams.TelId_TestingTelegram_Eagle_1:
          {
            message.DeviceName = "Eagle Calibrator";
            Task.Factory.StartNew(() => TcpProxyApp.SendMessageWithOpenAndCloseConnection(message)).Wait();
            return true;
          }
        case (int)PE.Core.Constants.AuxEquipmentTelegrams.TelId_TestingTelegram_Eagle_2:
          {
            message.DeviceName = "Eagle Stelmor";
            Task.Factory.StartNew(() => TcpProxyApp.SendMessageWithOpenAndCloseConnection(message)).Wait();
            return true;
          }
        case (int)PE.Core.Constants.AuxEquipmentTelegrams.TelId_TestingTelegram_Panther:
          {
            message.DeviceName = "Panther";
            Task.Factory.StartNew(() => TcpProxyApp.SendMessageWithOpenAndCloseConnection(message)).Wait();
            return true;
          }
        default:
          return false;
      }
    }



    public static bool HandleSendMessage(DCExtCommonMessage message)
    {
      TcpProxyApp.SendMessage(message);
      return true;
    }    
  }
}
