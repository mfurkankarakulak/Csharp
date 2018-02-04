package ChatServer;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class Server extends Thread{

	private final int serverPort;
	private ArrayList<ServerWorker> workerList=new ArrayList<>();           //Server'a kimler bagland� listesi

	public Server(int serverPort) {
		this.serverPort=serverPort;
	}
	public List<ServerWorker> getWorkerList(){
		return workerList;
		
	}
	@Override
	public void run() {
		try {
			ServerSocket serverSocket=new ServerSocket(serverPort);                 //serverSocket'e ait nesne olusturuluyor.
			while(true) {
				System.out.println("About to accept client connection");                 
				Socket clientSocket=serverSocket.accept();                          //server'a gelen istek ile server-client aras�nda soket olu�turuldu.
				System.out.println("Accepted connection from "+clientSocket );
				ServerWorker worker=new ServerWorker(this,clientSocket);             //olusan soket ile worker nesnesi olusturuldu.(ServerWorker s�n�f�: server ile client aras�nda olusan soket s�n�f�)
				workerList.add(worker);            //workerListesine son olusan soket eklendi
				worker.start();					
			}
		}catch (IOException e) {
					e.printStackTrace();
		}	
	}
	public void removeWorker(ServerWorker serverWorker) {
		workerList.remove(serverWorker);
		
	}
}
