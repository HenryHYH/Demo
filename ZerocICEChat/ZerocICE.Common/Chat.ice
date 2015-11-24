module ZerocICE.Common
{
	interface Callback
	{
		void GetMessage(string content);
	};

	dictionary<string, Callback *> OnlineUsers;

	interface Chat
	{
		bool Register(string name);
		void SendMessage(string content);
		void Unregister();
		void SetCallback(Callback * cb);
	};
};