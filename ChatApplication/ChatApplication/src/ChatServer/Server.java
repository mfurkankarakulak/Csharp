package ChatServer;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

public class Server extends Thread{

	private final int serverPort;
	private ArrayList<ServerWorker> workerList=new ArrayList<>();           //Server'a kimler baglandý listesi

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
				Socket clientSocket=serverSocket.accept();                          //server'a gelen istek ile server-client arasýnda soket oluþturuldu.
				System.out.println("Accepted connection from "+clientSocket );
				ServerWorker worker=new ServerWorker(this,clientSocket);             //olusan soket ile worker nesnesi olusturuldu.(ServerWorker sýnýfý: server ile client arasýnda olusan soket sýnýfý)
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
