using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using Newtonsoft.Json.Linq;
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;  
using Newtonsoft.Json;
using AirControl;
using Communicator;
using Commons;


namespace Communicator
{
	/// <summary>
	/// Class to manage the TCP network function
	/// </summary>
	public class NetworkCommunicator : MonoBehaviour
	{
		
		#region public members
		#endregion

		#region private members 	
		/// <summary> 	
		/// TCPListener to listen for incomming TCP connection 	
		/// requests. 	
		/// </summary> 
		private TcpListener tcpListener; 
		/// <summary> 
		/// Background thread for TcpServer workload. 	
		/// </summary> 	
		private Thread tcpListenerThread;  	
		/// <summary> 	
		/// Create handle to connected tcp client. 	
		/// </summary> 	
		private TcpClient connectedTcpClient; 	
		// provide base input to access variables
		[Header("Airplane Input Required")]
		[Tooltip("Drag and drop here the  AC_BaseAirplane_Input.cs OR AC_XboxAirplane_Input.cs")]
		public AC_BaseAirplane_Input currentReadings;

		public InputHandle inputHandle;
		public OutputHandle outputHandle;
		bool isOutput;
		#endregion 	
		
		#region  Default Methods
		// Use this for initialization
		void Start () { 		
			// Start TcpServer background thread 		
			tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
			tcpListenerThread.IsBackground = true; 
			tcpListenerThread.Start(); 	
		}  	
		#endregion
		
		#region Custom Methods
		/// <summary>
		/// Runs in background TcpServerThread; Handles incomming TcpClient requests 
		/// </summary>
		public void ListenForIncommingRequests () { 		
			try { 			
				// Create listener on localhost port 8052. 			
				tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), 8053); 	
					
				tcpListener.Start();              
				Debug.Log("Server is listening");              
				Byte[] bytes = new Byte[1024];
				if(inputHandle)
				{
					while (true) { 				
							using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
								// Get a stream object for reading 	
											
								using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
									int length; 						
									// Read incomming stream into byte arrary.				
									while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) 
									{
										try
										{
											var incommingData = new byte[length]; 							
											Array.Copy(bytes, 0, incommingData, 0, length);  							
											// Convert byte array to string message. 							
											string clientMessage = Encoding.ASCII.GetString(incommingData); 
											clientMessage = clientMessage.Replace("}{", "} | {");
											string [] inputArray = clientMessage.Split('|');
											foreach(string eachInput in inputArray)
											{
												isOutput = false;
												try{
													var inputJson =  JObject.Parse(eachInput);
													inputHandle.ParseInput(inputJson);	
													isOutput = bool.Parse(inputJson["IsOutput"].ToString());
												}
												catch (SocketException e){
													Debug.LogError($"JsonReaderException : { e.Source}");
													isOutput = true;
												}
												catch (JsonReaderException e){
													Debug.LogError($"JsonReaderException : { e.Source}");
													isOutput = true;
												}	
												// once received the message, send message in return
												if(isOutput){

													string outputmsg = outputHandle.ParseOutput();
													SendMessage(outputmsg);
												}
												else{
													string logOutput = outputHandle.LogOutput();
													SendMessage(logOutput);
												}							
											}
										}
										catch(Exception ex)
										{
											Debug.LogWarning("Socket exception: " + ex.ToString());
											isOutput = true;
										}
										ResetThings();
										
									}					
								} 			
							}
								
						} 
				}
				else
				{
					Debug.Log("InputHandle is detached in from Network manager. Go to Unity Hierarchy, look at inspector, drag and drop InputHandle onto Network communicator");
				}		
			} 		
			catch (SocketException ex) { 			
				Debug.LogWarning("Socket exception: " + ex.ToString());
				// tcpListener.Stop();
				isOutput = true;
			}     
		}
		// /// <summary>
		// /// Reset things after the response is sent
		// /// </summary>
		public void ResetThings()
		{
			if(StaticOutputSchema.IfCollision)
			{	
				StaticOutputSchema.IfCollision = false;
				// Resetting max reward after each reload
				CommonFunctions.MaxR=0;
			}
			
		}

		/// <summary>
		/// Depricated
		/// Usage : UnityEvent m_MyEvent = new UnityEvent();
    	/// public NetworkCommunicator ns;
		/// m_MyEvent.AddListener(ns.MyAction);
        /// m_MyEvent.Invoke();
		/// </summary>
		public void MyAction()
		{
			string outputmsg = outputHandle.ParseOutput();
			Debug.Log("Event Tgriggered");
			SendMessage(outputmsg);
			Debug.Log(outputmsg);
		}

		/// <summary> 	
		/// Send message to client using socket connection. 	
		/// </summary> 	
		public new void SendMessage(String outStructSerialized) { 		
			if (connectedTcpClient == null) {  
				return;         
			}  		
			try { 					
				NetworkStream stream = connectedTcpClient.GetStream(); 	
				if (stream.CanWrite) {  
					// string serverMessage = "This is a message from your server."; 			
					// Convert string message to byte array.                 
					byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(outStructSerialized); 				
					// Write byte array to socketConnection stream.               
					stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);               
				}       
			} 		
			catch (SocketException socketException) {             
				Debug.LogWarning("Socket exception: " + socketException);         
			} 	
		} 
		#endregion
	}

}
