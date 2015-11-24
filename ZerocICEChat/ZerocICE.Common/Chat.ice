module Common
{
       interface ICallback
       {
              void GetMessage(string content);
       };

       dictionary<string, ICallback *> OnlineUsers;

       interface IChat
       {
              bool Register(string name);
              void SendMessage(string content);
              void Unregister();
              void SetICallback(ICallback * cb);
       };
};