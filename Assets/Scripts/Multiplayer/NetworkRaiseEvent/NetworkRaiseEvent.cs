using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public static class NetworkRaiseEvent
{
    public static void RaiseEVT(byte Code, ReceiverGroup Options, object Data = null)
    {
        RaiseEventOptions receiverOPT = new()
        {
            Receivers = Options
        };

        if (Data == null)
        {
            Data = new object[]
            {

            };
        }

        PhotonNetwork.RaiseEvent(Code, Data, receiverOPT, SendOptions.SendReliable);
    }
}
