package ChatClient;

import java.awt.BorderLayout;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.IOException;

import javax.swing.DefaultListModel;
import javax.swing.JFrame;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

public class UserListPane extends JPanel implements UserStatusListener{
	private final ChatClient client;
	private JList<String> userListUI;
	private DefaultListModel<String> userListModel;
	
	public UserListPane(ChatClient client) {
		this.client=client;
		this.client.addUserStatusListener(this);
		
		userListModel=new DefaultListModel<>();		
		userListUI=new JList<>(userListModel);
		setLayout(new BorderLayout());
		add(new JScrollPane(userListUI),BorderLayout.CENTER);
		
		userListUI.addMouseListener(new MouseAdapter() {
			@Override
			public void mouseClicked(MouseEvent e) {
				if(e.getClickCount()>1) {
					String login=userListUI.getSelectedValue();             //login'e mesaj at�lacak ki�i atand�
					MessagePane messagePane=new MessagePane(client,login);
					
					JFrame frame=new JFrame("Message : "+login);
					frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
					frame.setSize(500, 500);
					frame.getContentPane().add(messagePane,BorderLayout.CENTER);
					frame.setVisible(true);
					
				}
				
			}
		});
	}

	public static void main(String[] args) {
		ChatClient client=new ChatClient("localhost",8818);

		UserListPane userListPane=new UserListPane(client);                 //burda listener eklemesi laz�m
		JFrame frame=new JFrame("User List");
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setSize(400,600);
		
		frame.getContentPane().add(userListPane,BorderLayout.CENTER);
		frame.setVisible(true);
		
		if(client.connect()) {
			try {
				client.login("a","a");
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

	@Override
	public void online(String login) {
		userListModel.addElement(login);
		
	}

	@Override
	public void offline(String login) {
		userListModel.removeElement(login);
		
	}
}
