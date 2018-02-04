package ChatClient;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;
import java.util.ArrayList;

public class ChatClient{
	private final String serverName;
	private final int serverPort;
	private Socket socket;
	private InputStream serverIn;
	private OutputStream serverOut;
	private BufferedReader bufferedIn;
	
	private ArrayList<UserStatusListener>userStatusListeners=new ArrayList<>();
	private ArrayList<MessageListener>messageListeners=new ArrayList<>();
	
	public ChatClient(String serverName, int serverPort) {
		this.serverName=serverName;
		this.serverPort=serverPort;
	}

	public static void main(String[] args) throws IOException {
		ChatClient client=new ChatClient("localhost",8818);
		client.addUserStatusListener(new UserStatusListener() {					//client'a 
			
			
			@Override
			public void online(String login) {
				
				System.out.println("ONLINE: "+login);             //birisi online olunca bildirim olarak gelecek.                 //serverdan gelen handlelogin mesajýnýn readerloop döngüsünde irdelenmesinden dolayý online mesajý basýyor
				
			}

			@Override
			public void offline(String login) {
				System.out.println("OFFLINE: "+login);             //handlelogout'un offline guest mesajýnýn irdelenmesi sonucu ekrana logoff basýyor.
				
			}
			
		});
		client.addMessageListener(new MessageListener() {

			@Override
			public void onMessage(String fromLogin, String msgBody) {
				
				System.out.println("Yeni mesajiniz var "+fromLogin+" kisisinden "+ "----->"+msgBody);
			}
		});
		
		
		if(!client.connect()) {
			System.out.println("Connection failed");
		}
		else {
			System.out.println("Connect successful");
			if(client.login("a","a")) {
				System.out.println("Login successful");
				//client.mesajGonder("guest","hello world");
			}else {
				System.err.println("Login failed");
			}
			//client.logoff();
		}
	}

	public void messageSend(String sendTo, String msgBody) throws IOException {
		String cmd="msg "+sendTo+" "+msgBody+"\n";
		serverOut.write(cmd.getBytes());
		
	}

	public boolean login(String login, String password) throws IOException {
		String cmd="login "+login+ " " +password+ "\n" ;
		serverOut.write(cmd.getBytes());
		//serverOut.flush();
		String response=bufferedIn.readLine();                             //gelen veriyi okuyor okey login
		System.out.println("Response Line:"+response);
		
		if("okey login".equalsIgnoreCase(response)) {
			startMessageReader();
			return true;
		}else {
			return false;
		}
	}
/*	public void logoff() throws IOException{
		String cmd="logoff";
		serverOut.write(cmd.getBytes());
	}*/
	
	private void startMessageReader() {
		Thread t=new Thread() {
			@Override
			public void run() {
				readMessageLoop();
			}
		
		};
		t.start();
	}
	private void readMessageLoop() {
		String line;
		try {
			while((line=bufferedIn.readLine()) !=null) {
				String[] tokens =line.split(" ");
				if(tokens != null && tokens.length>0) {
					String cmd=tokens[0];
					if("online".equalsIgnoreCase(cmd)) {                       //kullanýcý giriþ yaptýktan sonra eger login yazarsa handleLogin metodu çalýsýr.
						handleOnline(tokens);
					}else if("offline".equalsIgnoreCase(cmd)) {
						handleOffline(tokens);
					}else if("msg".equalsIgnoreCase(cmd)) {
						String[] tokensMsg=line.split(" ", 3); 
						handleMessage(tokensMsg);
						
					}
					
				}
			}
		} catch (IOException e) {
			e.printStackTrace();
			try {
				socket.close();
			} catch (IOException e1) {
				e1.printStackTrace();
			}
		}
 
	}	
		
	

	private void handleMessage(String[] tokensMsg) {
		String login=tokensMsg[1];
		String msgBody=tokensMsg[2];
		
		for(MessageListener listener:messageListeners) {
			listener.onMessage(login, msgBody);
		}
		
	}
		
	

	private void handleOffline(String[] tokens) {
		String login=tokens[1];
		for(UserStatusListener listener: userStatusListeners) {
			listener.offline(login);
		}
		
	}

	private void handleOnline(String[] tokens) {
		String login=tokens[1];
		for(UserStatusListener listener: userStatusListeners) {
			listener.online(login);
		}
		
	}

	public boolean connect() {
		try {
			this.socket=new Socket(serverName,serverPort);
			System.out.println("Client port is "+socket.getLocalPort());
			this.serverOut=socket.getOutputStream();
			this.serverIn=socket.getInputStream();
			this.bufferedIn=new BufferedReader(new InputStreamReader(serverIn));
			return true;
		} catch (IOException e) {
			e.printStackTrace();
		}
		return false;
		
	}
	public void addUserStatusListener(UserStatusListener listener) {
		userStatusListeners.add(listener);
	}

 	//public void removeUserStatusListener(UserStatusListener listener) {
	//	userStatusListeners.remove(listener);	
	//}
	public void addMessageListener(MessageListener listener) {
		messageListeners.add(listener);
	}
	//public void removeMessageListener(MessageListener listener) {
	//	messageListeners.remove(listener);
	//}

	
}
