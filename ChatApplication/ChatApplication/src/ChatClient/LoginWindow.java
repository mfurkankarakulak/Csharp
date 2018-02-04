package ChatClient;

import java.awt.BorderLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;

public class LoginWindow extends JFrame{
	JTextField loginField=new JTextField();
	JPasswordField passwordField=new JPasswordField();
	JButton loginButton=new JButton("Login");
	private final ChatClient client;
	
	public LoginWindow() {
		super("login");              //neydi bu
		
		this.client=new ChatClient("localhost",8818);
		client.connect();
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		JPanel p=new JPanel();
		p.setLayout(new BoxLayout(p,BoxLayout.Y_AXIS));
		p.add(loginField);
		p.add(passwordField);
		p.add(loginButton);
		loginButton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent arg0) {
				doLogin();
			}
		});
		getContentPane().add(p, BorderLayout.CENTER);
		pack();
		setVisible(true);
	}
	protected void doLogin() {
		String login=loginField.getText();
		String password=passwordField.getText();
		
		try {
			if(client.login(login, password)) {
				//user list window bring
				UserListPane userListPane=new UserListPane(client);                 //burda listener eklemesi lazým
				JFrame frame=new JFrame("User List");
				frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
				frame.setSize(400,600);
				
				frame.getContentPane().add(userListPane,BorderLayout.CENTER);
				frame.setVisible(true);
				setVisible(false);
			}else {
				//show error message
				JOptionPane.showMessageDialog(this, "Invalid login/password");
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		
	}
	public static void main(String[] args) {
		LoginWindow loginWindow=new LoginWindow();
		loginWindow.setVisible(true);
	}
}
