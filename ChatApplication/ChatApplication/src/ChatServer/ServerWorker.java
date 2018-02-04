package ChatServer;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;
import java.net.SocketException;
import java.util.Date;
import java.util.HashSet;
import java.util.List;

import ChatClient.UserStatusListener;

public class ServerWorker extends Thread{
	private final  Socket clientSocket;
	private String login=null;
	private final Server server;
	private OutputStream outputStream;
	//private HashSet<String> topicSet=new HashSet<>();
	public ServerWorker(Server server,Socket clientSocket){
		this.server=server;
		this.clientSocket=clientSocket;
	}
	
	@Override
	public void run() {
		try {
			handleClientSocket();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	private void handleClientSocket() throws IOException, InterruptedException {
		InputStream inputStream=clientSocket.getInputStream();
		this.outputStream=clientSocket.getOutputStream();
		BufferedReader reader=new BufferedReader(new InputStreamReader(inputStream));				//bufferedReader,veri ak���n� okumak i�in kullan�l�r.
		String line;
		while((line=reader.readLine()) != null) {													//gelen veri varsa d�ng� �al���r
			String[] tokens =line.split(" ");														//hata olabilir..05.25
			if(tokens != null && tokens.length>0) {
				String cmd=tokens[0];
				if("logoff".equals(cmd) ||"quit".equalsIgnoreCase(cmd)) {
					handleLogoff();
					break;
				}else if("login".equalsIgnoreCase(cmd))  {
					handleLogin(outputStream, tokens);                                                   //handleLogin metoduna ... ve tokeni veriyoruz. (token=login user password)
				}else if("msg".equalsIgnoreCase(cmd)) {
					String[] tokensMsg=line.split(" " ,3);             								 //bo�luga g�re ay�rma yapt� fakat 3 par�aya b�ld�. son par�a gidecek mesaj --->  msg ali merhaba nasilsin
					handleMessage(tokensMsg);
				}/*else if("join".equalsIgnoreCase(cmd)) {
					handleJoin(tokens);	
				}else if("leave".equalsIgnoreCase(cmd)) {
					handleLeave(tokens);
				}*/else {
					String msg="unknown "+cmd+"\n";
					outputStream.write(msg.getBytes());
			}		
		}
			//String msg="You typed: "+line+"\n";
			//outputStream.write(msg.getBytes());
		}
		clientSocket.close();
		
	}
/*	private void handleLeave(String[] tokens) {
		if(tokens.length>1) {
			String topic=tokens[1];
			topicSet.remove(topic);
		} 
		
	}*/

/*	public boolean isMemberOfTopic(String topic) {
		return topicSet.contains(topic);
		
	}*/
/*	private void handleJoin(String[] tokens) {
		if(tokens.length>1) {
			String topic=tokens[1];
			topicSet.add(topic);
		}
			
	}*/

	//fornat : "msg" "login" msg...
	//format:"msg" "#topic" body...
	private void handleMessage(String[] tokens) throws IOException {
		String sendTo=tokens[1];
		String body=tokens[2];
		
		//boolean isTopic=sendTo.charAt(0)=='#';
		List<ServerWorker> workerList=server.getWorkerList();
		for(ServerWorker worker:workerList) {
			/*if(isTopic) {
				if(worker.isMemberOfTopic(sendTo)) {
					String outMsg="msg "+sendTo+":"+login+" "+body+"\n";
					worker.send(outMsg);
				}
			}else {*/
			
				if(sendTo.equalsIgnoreCase(worker.getLogin())) {     //sendTo ki�isi login yapan ki�ilerin i�inde ise ona mesaj� gonderir.
					String outMsg="msg "+login +" "+body+"\n";
					worker.send(outMsg);
				}
			//}
		}
	}

	private void handleLogoff() throws IOException {
		server.removeWorker(this); // herkes logoff yapm�ssa  exception olusuyor. engellemek i�in
		
		List<ServerWorker> workerList=server.getWorkerList();	
		//bu kullan�c� ��k�� yapt�g� i�in di�er kullanc��lara bildirim gidiyor.
		String offlineMsg="offline "+ login+"\n";
		for(ServerWorker worker:workerList) {				
			if(!login.equals(worker.getLogin())) {			//workerListteki kullan�c� login yapm�ssa mesaj gidiyor.
				worker.send(offlineMsg);
			}
		}
		
		clientSocket.close();
		
	}

	public String getLogin() {
		return login;
	}
	
	private void handleLogin(OutputStream outputStream, String[] tokens) throws IOException {
		if(tokens.length==3) {
			String login=tokens[1];
			String password=tokens[2];
			
			if((login.equals("a")&&password.equals("a")) || (login.equals("b")&&password.equals("b")) || (login.equals("c")&&password.equals("c")) || (login.equals("d")&&password.equals("d")) ) {
				String msg="okey login"+"\n";
				outputStream.write(msg.getBytes());                                              //outputStream ile "okey login" mesaj� yazd�r�l�r
				this.login=login;
				System.out.println("User logged succesfully: "+login);
				List<ServerWorker> workerList=server.getWorkerList();
			
				//giri� yapan kullan�c�n�n bilgileri diger online kullan�c�lara g�nderilir
				for(ServerWorker worker:workerList) {	
					if(worker.getLogin()!=null) {
						if(!login.equals(worker.getLogin())) {
							String msg2="online "+worker.getLogin()+"\n";		
							send(msg2);
						}
					}
					
					
				}
				
				//di�er online  kullan�c�lar�n bilgileri giri� yapan kullan�c�ya getirilir.
				String onlineMsg="online "+login+"\n";
				for(ServerWorker worker:workerList) {
					if(!login.equals(worker.getLogin())) {
						worker.send(onlineMsg);
					}
				}
			}
			else {
				String msg="error login\n";
				outputStream.write(msg.getBytes());
				System.err.println("Login failed for : "+login);
			}
		}
		
	}

	private void send(String onlineMsg) throws IOException {
		if(login!=null) {
			outputStream.write(onlineMsg.getBytes());
		}
	}
	
}
