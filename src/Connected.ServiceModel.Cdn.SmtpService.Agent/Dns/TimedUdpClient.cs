using System.Net;
using System.Net.Sockets;

namespace Connected.ServiceModel.Cdn.SmtpService.Dns;

internal sealed class TimedUdpClient : UdpClient
{
	private Mutex? _mutexReturnReceive = new(false);
	private Mutex? _mutexErrorOccurs = new(false);
	private IPEndPoint? _remote;

	private IPEndPoint? Remote { get { return _remote; } set { _remote = value; } }
	private Thread? ReceiveThread { get; set; }
	private bool ErrorOccurs { get; set; }
	private byte[]? ReturnReceive { get; set; }
	public int Timeout { get; set; } = 2000;
	public new byte[]? Receive(ref IPEndPoint remote)
	{
		Remote = remote;

		ReceiveThread = new Thread(new ThreadStart(StartReceive));
		ReceiveThread.Start();

		Thread.Sleep(Timeout);

		_mutexErrorOccurs?.WaitOne();

		if (ErrorOccurs)
		{
			_mutexErrorOccurs?.ReleaseMutex();

			throw new Exception(SR.ConnectionTimeout);
		}
		else
			_mutexErrorOccurs?.ReleaseMutex();

		return ReturnReceive;
	}

	private void StartReceive()
	{
		_mutexErrorOccurs?.WaitOne();
		ErrorOccurs = true;
		_mutexErrorOccurs?.ReleaseMutex();

		try
		{
			byte[] ret = base.Receive(ref _remote);

			_mutexReturnReceive?.WaitOne();
			ReturnReceive = ret;
			_mutexReturnReceive?.ReleaseMutex();
			ErrorOccurs = false;
		}
		catch (SocketException) { }
		catch (ThreadAbortException) { }
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_mutexErrorOccurs = null;
		_mutexReturnReceive = null;
	}
}