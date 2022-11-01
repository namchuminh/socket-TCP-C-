using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Bước 1: Khởi tạo địa chỉ IPEndpoint và port
                IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),8000);
                //Bước 2: Tạo socket TCP (server)
                Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                //Bước 3: Đăng ký IPEndpoint cho socket TCP
                serverSocket.Bind(s_iep);
                //Bước 4: Lắng nghe các kết nối tới TCP (server)
                Console.WriteLine("Doi ket noi tu Client");
                serverSocket.Listen(10);
                //Khi client kết nối tới thì chấp nhận kết nối của client đó và gán vào client Socket
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine("Ket noi client Thanh cong!" + clientSocket.RemoteEndPoint.ToString());
                //Bước 5: Trao đổi (nhận/gửi) dữ liệu giữa TCP server và client

                do
                {
                    //Nhận tin
                    byte[] dataReceive = new byte[1024];
                    clientSocket.Receive(dataReceive);
                    string messageReceive = ASCIIEncoding.ASCII.GetString(dataReceive);
                    Console.WriteLine("<client>: " + messageReceive);

                    //Gửi tin
                    string messageSend = Console.ReadLine(); //Nhập chuỗi cần gửi về client
                    byte[] dataSend = ASCIIEncoding.ASCII.GetBytes(messageSend);
                    clientSocket.Send(dataSend);

                    if (messageSend.ToLower() == "thoat" || messageReceive.ToLower() == "thoat")
                    {
                        break;
                    }

                } while (true);
                

                
                //Bước 6: Đóng kết nối
                serverSocket.Close();
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
